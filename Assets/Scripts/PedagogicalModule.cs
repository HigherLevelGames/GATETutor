using UnityEngine;
using System.Collections;

// Pedagogical module

public class PedagogicalModule : MonoBehaviour
{
    public GUISkin skin;
    public Task[] tasks;
    public StepAnalyzer analyzer;
    private Task currentTask;
    private int taskNum;

	// Use this for initialization
	void Start ()
    {
        taskNum = 0;
        currentTask = tasks[taskNum];
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    void OnGUI()
    {
        GUI.skin = skin;
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height * 0.2f), currentTask.question);

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

    public void SetStep()
    {
        analyzer.SendMessage("SetStep", currentTask.currentStep);
    }

    [System.Serializable]
    public class Task // steps
    {
        public string question;
        public string KC;

        public Step[] steps; // question/task has multiple steps
        public Step currentStep;
        public int stepNum = 0;
    }
}