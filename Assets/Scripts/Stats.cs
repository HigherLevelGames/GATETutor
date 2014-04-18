using UnityEngine;
using System.Collections;

// Assessor (i.e. Unity's PlayerPrefs)
// For a step-based tutoring system,
// the assessor needs to receive all the steps
// that the student did, and for each step,
// the step analyzer needs to send the assessor:
// 1. A step history e.g., whether the student got the step right
//    on the first attempt, asked for a hint, gave up, etc.
// 2. The knowledge components exercised by the step
// The assessor maintains the learner model,
// which has a database of all knowledge components
// and perhaps much other information about the student/learner.
// Each knowledge component has a number,
// category or some other value that represents the learner's
// current competence on that knowledge component.
// The assessor's job is to update the measures of competence
// based on the input it receives from the step analyzer.


public class Stats : MonoBehaviour
{
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnGUI()
    {
        string text = "Performance";

        /*
        //add performance text here
        
        foreach(string KC in KnowledgeComponents)
        {
            text += KC + ": " + PlayerPrefs.GetInt(KC);
        }
        //*/

        GUI.Label(new Rect(0, 0, Screen.width, Screen.height*0.1f), text);
        //GUI.Box(new Rect(0,0,Screen.width, Screen.height),text);

        if (GUI.Button(new Rect(Screen.width * 0.75f, Screen.height * 0.8f, Screen.width * 0.25f, Screen.height * 0.1f), "Restart"))
        {
            Application.LoadLevel("Title");
        }
    }
}
