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
    public GUISkin skin;
    private string[] history;
    private Vector2[] scrollPositions;

	// Use this for initialization
	void Start ()
    {
        history = PlayerPrefs.GetString("StepHistory").Split('\t');
        scrollPositions = new Vector2[history.Length-1];
        for (int i = 0; i < history.Length - 1; i++)
        {
            int numCorrect = PlayerPrefs.GetInt("c" + i);
            int numIncorrect = PlayerPrefs.GetInt("i" + i);
            Debug.Log(numCorrect + ", " + numIncorrect);
            // pie chart set data
        }
	}
	
	// Update is called once per frame
	void Update () {
	}
    
    void OnGUI()
    {
        GUI.skin = skin;
        string text = "Performance";

        /*
        //add performance text here
        
        foreach(string KC in KnowledgeComponents)
        {
            text += KC + ": " + PlayerPrefs.GetInt(KC);
        }
        //*/

        float width = Screen.width/(history.Length-1);
        for (int i = 0; i < history.Length - 1; i++)
        {
            Rect scrollRect = new Rect(0 + i * width, Screen.height * 0.5f, width, Screen.height * 0.3f);
            string step = history[i];
            float height = GUI.skin.box.CalcHeight(new GUIContent(step), width);

            // begin scroll view
            scrollPositions[i] = GUI.BeginScrollView(
                    scrollRect, // scroll rectangle
                    scrollPositions[i],
                    new Rect(0, 0, width, height) // view inside scroll rectangle
                    );
            GUI.Box(new Rect(0, 0, width, height), step);

            // end scroll view and make window draggable
            GUI.EndScrollView();
        }

        GUI.Label(new Rect(0, 0, Screen.width, Screen.height*0.1f), text);
        //GUI.Box(new Rect(0,0,Screen.width, Screen.height),text);

        if (GUI.Button(new Rect(Screen.width * 0.75f, Screen.height * 0.8f, Screen.width * 0.25f, Screen.height * 0.1f), "Restart"))
        {
            Application.LoadLevel("Title");
        }
    }
}
