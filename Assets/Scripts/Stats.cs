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
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height * 0.1f), "Performance");
        
        // populate the information
        ArrayList selectionItems = new ArrayList();
        for (int i = 0; i < history.Length - 1; i++)
        {
            selectionItems.Add("Task #" + (i+1) + ":");
            selectionItems.Add(history[i].Split('\n')[0]);
            
            int numCorrect = PlayerPrefs.GetInt("c" + i);
            selectionItems.Add("Number of Correct: " + numCorrect);
            
            int numIncorrect = PlayerPrefs.GetInt("i" + i);
            selectionItems.Add("Number of Wrong: " + numIncorrect);
            
            selectionItems.Add("Percentage: " + (((float)numCorrect / (float)(numCorrect + numIncorrect)) * 100) + "%");
            selectionItems.Add("Show Steps");
        }
        string[] items = (string[])selectionItems.ToArray(typeof(string));

        // TODO: GUI.BeginScrollView() if we ever decide to add more than four tasks
        // most likely by creating TaskSelector.cs which imports tasks from a text file
        // and chooses tasks accordingly to student's current level of knowledge
        // when asked for by the pedagogical model during gameplay...

        // GUI.SelectionGrid since unity doesn't have its own GUI.Table
        GUI.skin.button.wordWrap = true;
        GUI.skin.button.fixedHeight = 50;
        int selected = -1;
        Rect bgRect = new Rect(0, Screen.height * 0.1f, Screen.width, Screen.height * 0.35f);
        //GUI.Box(bgRect,"");
        selected = GUI.SelectionGrid(bgRect, selected, items, 6, GUI.skin.button);

        // manage selected button from SelectionGrid
        if (selected % 6 == 5) // last column
        {
            if (showFeedback && curFeedback == (selected / 6)) // if already showing feedback
            {
                showFeedback = false;
            }
            else // not showing feedback
            {
                curFeedback = selected / 6;
                showFeedback = true;
                feedbackText = history[curFeedback];
                windowTitle = history[curFeedback].Split('\n')[0];
            }
        }
        else if (selected != -1) // clicked on any of the other columns
        {
            showFeedback = false;
        }

        // feedback shown in a GUI.Window
        if (showFeedback)
        {
            windowRect = GUI.Window(0, windowRect, windowFunc, windowTitle);
        }

        // Restart button
        if (GUI.Button(new Rect(Screen.width * 0.75f, Screen.height * 0.8f, Screen.width * 0.25f, Screen.height * 0.1f), "Restart"))
        {
            Application.LoadLevel("Title");
        }
    }

    string windowTitle = "";
    Rect windowRect = new Rect(0,0,Screen.width * 0.5f,Screen.height * 0.5f);
    bool showFeedback = false;
    string feedbackText = "";
    int curFeedback = -1;
    Vector2 scrollPosition = Vector2.zero;
    void windowFunc(int id)
    {
        // dimension variables
        float sideBuffer = 13;
        float topBuffer = 26;
        float bottomBuffer = 14;
        float winWidth = windowRect.width - sideBuffer * 2;
        float vScrollWidth = 18;
        float width = winWidth - vScrollWidth;
        float height = GUI.skin.box.CalcHeight(new GUIContent(feedbackText), width);
        Rect scrollRect = new Rect(sideBuffer, topBuffer, winWidth, windowRect.height - topBuffer - bottomBuffer);

        // begin scroll view
        scrollPosition = GUI.BeginScrollView(
                scrollRect, // scroll rectangle
                scrollPosition,
                new Rect(0, 0, width, height) // view inside scroll rectangle
                );
        GUI.Box(new Rect(0, 0, width, height), feedbackText);

        // end scroll view and make window draggable
        GUI.EndScrollView();
        GUI.DragWindow();
    }
}
