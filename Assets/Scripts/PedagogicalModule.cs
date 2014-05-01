using UnityEngine;
using System.Collections;

// Pedagogical module
// Generate feedback and hints.

// It inputs the students steps,
// which have been analyzed by the Step Analyzer,
// and outputs feedback and hints on the steps
// to the student interface, which presents them.

// If the student requests help,
// then it takes that as an input as well.

// With process tutoring, the pedagogical module
// receives steps as the student generates them,
// and it reacts to each step when it receives it.


// Task Selector:
// 1. decide when a student has finished a module and may go on to another one.
// 2. pick or recommend a task for the student to do as part of their work within a module.
// Questions: http://sandbox.mc.edu/~bennet/cs110/boolalg/gate.html


/* Order of Operations: (Boolean Algebra)
1. Parenthesis ( ) 
2. NOT ~ 
3. AND z 
4. OR +
//*/

public class PedagogicalModule : MonoBehaviour
{
    public GUISkin skinButton;
    private Rect nextButton = new Rect(80, 80, 15, 5);
    private Rect quitButton = new Rect(80, 85, 15, 5);
    public Font buttonFont;
    public Task[] tasks;
    public StepAnalyzer analyzer;

    public GameObject spark;
    public GameObject spark2;
    public AudioClip finishedTaskSFX; // sound that plays when lights turn on
    public AudioClip btnSFX; // sound that plays when going to a new task
    public AudioClip correctSFX; // sound that plays when a step is entered correctly
    public AudioClip wrongSFX; // sound that plays when a step is entered incoreectly

    private Task currentTask;
    private int taskNum;
    private bool showNext = false;
    private int numWrong = 0;
    private bool isMute = false;
	// Use this for initialization
	void Start ()
    {
        taskNum = 0;
        currentTask = tasks[taskNum];
        this.SendMessage("AddLine", currentTask.question);
        SetStep();
	}

    void OnGUI()
    {
        GUI.skin = skinButton;
        GUI.skin.font = buttonFont;
        isMute = GUI.Toggle(adjRect(new Rect(80, 75, 15, 5)), isMute, "Mute");
        if (isMute)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
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
            GUI.backgroundColor = Color.white;
            if (GUI.Button(adjRect(nextButton), "Next Problem"))
            {
                GameObject.Find("Sci-Fi Interface").SendMessage("TurnOff");
                this.SendMessage("ClearConsole");
                audio.clip = btnSFX;
                audio.Play();
                NextTask();
                showNext = false;
                analyzer.showMe = true;
            }
        }

        if (GUI.Button(adjRect(quitButton), "Quit"))
        {
            Application.Quit();
            //Application.LoadLevel("End");
        }

        /*
        if(GUI.Button(hint))
            give hint GUI.Box
        //*/
    }

    // returns Rectangle adjusted to screen size
    Rect adjRect(Rect r)
    {
        return new Rect(
                r.x * Screen.width / 100.0f,
                r.y * Screen.height / 100.0f,
                r.width * Screen.width / 100.0f,
                r.height * Screen.height / 100.0f);
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
            this.SendMessage("AddLine", currentTask.question);
        }
        else
        {
            // allow enough time to play the SFX for turning off the interface
            Invoke("GoToNextLevel", btnSFX.length);
        }
        SetStep();
    }

    public void GoToNextLevel()
    {
        Application.LoadLevel("End");
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

    // called by step analyzer to inform pedagogical module of a correct step
    public void NextStep()
    {
        numWrong = 0;
        PlayerPrefs.SetInt("Correct", PlayerPrefs.GetInt("Correct") + 1);
        currentTask.stepNum++;
        
        if (currentTask.answeredQuestion) // finished task
        {
            analyzer.showMe = false;
            this.SendMessage("AddLine", "Complete!");
            GameObject.Find("Sci-Fi Interface").SendMessage("TurnOn");
            audio.clip = finishedTaskSFX;
            audio.Play();
            showNext = true; // to show the next button and give student a chance to review his/her work
            int numCorrect = PlayerPrefs.GetInt("Correct");
            int numIncorrect = PlayerPrefs.GetInt("Incorrect");
            this.SendMessage("AddLine", "Number of Correct: " + numCorrect + " Number of Incorrect: " + numIncorrect);
            PlayerPrefs.SetInt("c" + taskNum, numCorrect);
            PlayerPrefs.SetInt("i" + taskNum, numIncorrect);
            PlayerPrefs.DeleteKey("Correct");
            PlayerPrefs.DeleteKey("Incorrect");
        }
        else // correct step
        {
            this.SendMessage("AddLine", "Correct!");
            audio.clip = correctSFX;
            audio.Play();
            SetStep();
        }
    }

    // called by step analyzer to inform pedagogical module of an incorrect step
    public void IncorrectStep(string answered)
    {
        PlayerPrefs.SetInt("Incorrect", PlayerPrefs.GetInt("Incorrect") + 1);
        // play sound
        audio.clip = wrongSFX;
        audio.Play();

        // create particle
        GameObject temp = (GameObject)Instantiate(spark, analyzer.transform.position, Quaternion.identity);
        Destroy(temp, 0.7f);
        GameObject temp2 = (GameObject)Instantiate(spark2, analyzer.transform.position, Quaternion.identity);
        Destroy(temp2, 0.5f);

        this.SendMessage("AddLine", "Incorrect: You answered: " + answered);
        numWrong++;
        if (numWrong >= 2)
        {
            // show hint on console
            this.SendMessage("AddLine", "Hint: " + currentTask.currentStep.hint);
        }
    }

    public void GiveHint()
    {
        this.SendMessage("AddLine", "Hint: " + currentTask.currentStep.hint);
    }

    public void SetStep()
    {
        // step analyzer needs new step
        analyzer.SendMessage("SetStep", currentTask.currentStep);

        // title on console
        this.SendMessage("SetTitle", "Current Task: " + currentTask.question);
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