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

	// Use this for initialization
	void Start () { }
	
	// Update is called once per frame
	void Update ()
    {
    }

    void SetStep(Step a)
    {
        currentStep = a;
        this.transform.position = a.position;
        answer = a.answer.ToString();
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
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == answer)
        {
            Debug.Log("correct answer");
            // assign entered gate to locked gate
            col.gameObject.transform.position = this.transform.position;
            col.gameObject.GetComponent<ClickAndDrag>().isDraggable = false;
            foreach (Vector3 p in currentStep.inputPositions)
            {
               // Debug.Log("has inputs: " + this.transform.position + " and " + p);
                col.gameObject.AddComponent<OurLineRenderer>();
                col.gameObject.SendMessage("SetStart", this.transform.position); // previous
                col.gameObject.SendMessage("SetEnd", p); // current
            }
            col.gameObject.SendMessage("Respawn");

            GameObject.Find("PedagogicalModule").SendMessage("NextStep");
        }
        else
        {
            Debug.Log("wrong answer");
            col.gameObject.SendMessage("Return");
        }
    }
}
