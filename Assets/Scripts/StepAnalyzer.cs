using UnityEngine;
using System.Collections;

// Step Analyzer
// 1. When given a step from the user interface,
//    it must classify the steps as unrecognized, correct, incorrect, etc.
//    and send this classification to the pedagogical module,
//    which decides what to do about each step.

// 2. If the tutoring system has an assessor,
//    and the student step(s) are recognized,
//    then the step analyzer must decide what knowledge components
//    each step addresses and tell the assessor.

// 3. If the student or the pedagogical modules
//    asks for suggestions of steps the student should do next,
//    the step analyzer must report some. (most likely, won't be implemented)


// Step loop: Student Interface > Step Analyzer > Pedagogical Module
// Task loop: Student Interface > Step Analyzer > Assessor (w/ learner model) >Task Selector


// green trigger box for user to drag gate onto
public class StepAnalyzer : MonoBehaviour
{
    public string answer; // and, or, not, nand, or nor
    public Step currentStep;
    private Texture2D wireTexture;
    public float width = 100;

	// Use this for initialization
	void Start ()
    {
        int resolution = 256;
        int lineThickness = 5;
        wireTexture = new Texture2D(resolution, resolution);
        for (int i = 0; i < resolution; i++)
        {
            for (int j = 0; j < resolution; j++)
            {
                wireTexture.SetPixel(i, j, Color.clear);
                if (i < lineThickness || j < lineThickness || i > (resolution - 1) - lineThickness || j > (resolution-1) - lineThickness)
                {
                    wireTexture.SetPixel(i, j, Color.green);
                }
            }
        }
        wireTexture.Apply();
    }

    // OnGUI() draws a green square outline
    void OnGUI()
    {
        Vector3 pt = Camera.main.WorldToScreenPoint(this.transform.position);
        GUI.DrawTexture(new Rect(pt.x - width / 2.0f, Screen.height - pt.y - width / 2.0f, width, width), wireTexture);
    }

    void SetStep(Step a)
    {
        // set the current step and answer
        currentStep = a;
        answer = a.answer.ToString();

        // get the position where the player should click and drag their gate onto
        Vector2 pos = adjPoint(a.Position2D);

        // convert the position to the screen position, camera is 10 units away from zero
        float distanceFromCamera = 10;
        Vector3 screenPos = new Vector3(pos.x, Screen.height - pos.y, distanceFromCamera);

        // get the screen position into world coordinates
        this.transform.position = Camera.main.ScreenToWorldPoint(screenPos);        
    }

    public void AnswerCorrect()
    {
        //PlayerPrefs.SetInt(KC, PlayerPrefs.GetInt(KC) + 1);
    }

    public void AnswerWrong()
    {
        //PlayerPrefs.SetInt(KC, PlayerPrefs.GetInt(KC) - 1);
    }

    // once gate enters the trigger
    // analyzes whether it is wrong or correct
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == answer)
        {
            Debug.Log("correct answer");
            // assign entered gate to locked gate
            col.gameObject.transform.position = this.transform.position;
            col.gameObject.GetComponent<ClickAndDrag>().isDraggable = false;

            // Create input lines that are created for locked gates
            foreach (Line l in currentStep.inputLines)
            {
                GameObject obj = new GameObject();
                obj.tag = "ToClean";
                obj.AddComponent<OurLineRenderer>();
                obj.SendMessage("SetStart1", adjPoint(l.begin)); // previous
                obj.SendMessage("SetEnd1", adjPoint(l.end)); // current
            }
            col.gameObject.SendMessage("Respawn");

            // Tell the pedagogical module that we're ready for the next step (or task)
            GameObject.Find("PedagogicalModule").SendMessage("NextStep");
        }
        else
        {
            Debug.Log("wrong answer");
            col.gameObject.SendMessage("Return");
            GameObject.Find("PedagogicalModule").SendMessage("IncorrectStep", col.gameObject.tag);
        }
    }

    Vector2 adjPoint(Vector2 l)
    {
        // The point stored in step is based on a 25x20 grid
        // the canvas takes up 75% of the screen
        Vector2 pt = new Vector2(l.x*(Screen.width*0.75f)/25.0f, l.y*(Screen.height*0.75f)/20.0f);
        return pt;
    }
}
