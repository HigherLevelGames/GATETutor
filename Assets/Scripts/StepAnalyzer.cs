using UnityEngine;
using System.Collections;

// Step Analyzer

// green trigger box for user to drag gate onto
public class StepAnalyzer : MonoBehaviour
{
    private GameObject lockedGate;
    public string answer; // and, or, not, nand, or nor
    public Step currentStep;

	// Use this for initialization
	void Start () { }
	
	// Update is called once per frame
	void Update ()
    {
        if (lockedGate != null)
        {
            // lock the gate to the answer box's position
            lockedGate.transform.position = this.transform.position;
        }
	}

    void SetStep(Step a)
    {
        this.transform.position = a.position;
        answer = a.answer.ToString();
        /*switch (a.answer)
        {
            case Step.AnswerOption.And:
                answer = "And";
                break;
            case Step.AnswerOption.Nand:
                answer = "Nand";
                break;
            case Step.AnswerOption.Nor:
                answer = "Nor";
                break;
            case Step.AnswerOption.Not:
                answer = "Not";
                break;
            case Step.AnswerOption.Or:
                answer = "Or";
                break;
        }//*/
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
            lockedGate = col.gameObject;
        }
        else
        {
            Debug.Log("wrong answer");
        }
    }
}
