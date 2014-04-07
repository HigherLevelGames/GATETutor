using UnityEngine;
using System.Collections;

// Pedagogical module

public class PedagogicalModule : MonoBehaviour
{
    public GUISkin skinBox;
    public GUISkin skinButton;
    public Font questionFont;
    public Font buttonFont;
    public Task[] tasks;
    public StepAnalyzer analyzer;
    private Task currentTask;
    private int taskNum;
    private bool showNext = false;

	// Use this for initialization
	void Start ()
    {
        taskNum = 0;
        currentTask = tasks[taskNum];
        SetStep();
	}

    void winFunc(int id)
    {

    }

    void OnGUI()
    {
        GUI.skin = skinBox;
        GUI.skin.font = questionFont;
        GUI.contentColor = Color.black;
        GUI.skin.box.alignment = TextAnchor.MiddleCenter;
        GUI.skin.box.fontSize = 50;
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height * 0.2f), currentTask.question);
        
        //GUI.Window(0, new Rect(0, 0, Screen.width, Screen.height * 0.2f), winFunc, currentTask.question);
        //GUI.Label(new Rect(0, 0, Screen.width, Screen.height * 0.2f), currentTask.question);

        GUI.skin = skinButton;
        GUI.skin.font = buttonFont;

        /*
        if (GUI.Button(new Rect(0, Screen.height*0.8f, Screen.width * 0.25f, Screen.height*0.1f), "Prev"))
        {
            GetPrevQuestion();
        }

        if (GUI.Button(new Rect(Screen.width*0.25f, Screen.height * 0.8f, Screen.width * 0.25f, Screen.height * 0.1f), "Next"))
        {
            GetNextQuestion();
        }

        if (GUI.Button(new Rect(Screen.width*0.5f, Screen.height * 0.8f, Screen.width * 0.25f, Screen.height * 0.1f), "Random"))
        {
            GetRandomQuestion();
        }

        if (GUI.Button(new Rect(Screen.width * 0.75f, Screen.height * 0.8f, Screen.width * 0.25f, Screen.height * 0.1f), "Quit"))
        {
            Application.LoadLevel("End");
        }//*/

        if (showNext)
        {
            if (GUI.Button(new Rect(Screen.width * 0.75f, Screen.height * 0.8f, Screen.width * 0.25f, Screen.height * 0.1f), "Next Problem"))
            {
                NextTask();
                showNext = false;
            }
        }

        /*
        if(GUI.Button(hint))
            give hint GUI.Box
        //*/
    }

    public void GetQuestion(int num)
    {
        if (num >= 0 && num < tasks.Length) // bounds
        {
            taskNum = num;
            currentTask = tasks[taskNum];
        }
        SetStep();
    }

    public void GetPrevQuestion()
    {
        if (taskNum > 0) // to avoid underflow
        {
            taskNum--;
            currentTask = tasks[taskNum];
        }
        SetStep();
    }

    public void GetNextQuestion()
    {
        if (taskNum < tasks.Length - 1) // to avoid overflow
        {
            taskNum++;
            currentTask = tasks[taskNum];
        }
        SetStep();
    }

    public void GetRandomQuestion()
    {
		if (tasks.Length < 2) // needs at least two elements in order to function properly
		{
			return;
		}

		int num = 0;
		do
		{
			num = Random.Range (0, tasks.Length - 1); // min(inclusive), max(inclusive)
		} while(taskNum == num); // until different question number

		taskNum = num;
        currentTask = tasks[taskNum];
        SetStep();
    }

    public void NextTask()
    {
        GameObject[] toClean = GameObject.FindGameObjectsWithTag("ToClean");
        foreach (GameObject o in toClean)
        {
            Destroy(o);
        }

        //GetQuestion(7); // if need specific
        //GetPrevQuestion(); // if need easier
        //GetRandomQuestion(); // for uncertainty
        GetNextQuestion(); // if need harder
        SetStep();
    }

    /* Order of Operations: (Boolean Algebra)
    1. Parenthesis ( ) 
    2. NOT ~ 
    3. AND z 
    4. OR +
    //*/

    public void NextStep()
    {
        currentTask.stepNum++;
        if (currentTask.answeredQuestion)
        {
            showNext = true;
        }
        else
        {
            SetStep();
        }
    }

    public void SetStep()
    {
        analyzer.SendMessage("SetStep", currentTask.currentStep);
    }

    [System.Serializable]
    public class Task // steps
    {
        public string question;
        public string KC;
        public bool answeredQuestion = false;

        public Step[] steps; // question/task has multiple steps
        public Step currentStep
        {
            get
            {
                return steps[stepNum];
            }
        }

        private int num = 0;
        public int stepNum
        {
            get
            {
                return num;
            }
            set
            {
                num = Mathf.Clamp(value, 0, steps.Length - 1);
                if (value == steps.Length)
                {
                    answeredQuestion = true;
                }
            }
        }
    }
}