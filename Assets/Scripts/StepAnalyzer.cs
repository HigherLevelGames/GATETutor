using UnityEngine;
using System.Collections;

// Step Analyzer

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
