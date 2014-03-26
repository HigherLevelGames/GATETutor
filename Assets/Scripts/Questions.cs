using UnityEngine;
using System.Collections;

public class Questions : MonoBehaviour
{
    public GUISkin skin;
    public Question[] questions;
    private Question currentQuestion;
    private int questionNum;

	// Use this for initialization
	void Start ()
    {
        questionNum = 0;
        currentQuestion = questions[questionNum];
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    void OnGUI()
    {
        GUI.skin = skin;
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height * 0.2f), currentQuestion.question);

        currentQuestion.Draw();

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
    }

    public void GetQuestion(int num)
    {
        if (num >= 0 && num < questions.Length) // bounds
        {
            questionNum = num;
            currentQuestion = questions[questionNum];
        }
    }

    public void GetPrevQuestion()
    {
        if (questionNum > 0) // to avoid underflow
        {
            questionNum--;
            currentQuestion = questions[questionNum];
        }
    }

    public void GetNextQuestion()
    {
        if (questionNum < questions.Length - 1) // to avoid overflow
        {
            questionNum++;
            currentQuestion = questions[questionNum];
        }
    }

    public void GetRandomQuestion()
    {
		if (questions.Length < 2) // needs at least two elements in order to function properly
		{
			return;
		}

		int num = 0;
		do
		{
			num = Random.Range (0, questions.Length - 1); // min(inclusive), max(inclusive)
		} while(questionNum == num); // until different question number

		questionNum = num;
        currentQuestion = questions[questionNum];
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "OR")
        {

        }
        LineRenderer l = this.GetComponent<LineRenderer>();
        l.SetPosition(0,new Vector3(0,0,0));
        l.SetPosition(1, new Vector3(0, 0, 0));
        l.Set
    }

    [System.Serializable]
    public class Question
    {
        public string question;
        public string KC;

        public void AnswerCorrect()
        {
            PlayerPrefs.SetInt(KC, PlayerPrefs.GetInt(KC) + 1);
        }

        public void AnswerWrong()
        {
            PlayerPrefs.SetInt(KC, PlayerPrefs.GetInt(KC) - 1);
        }

        // called OnGUI
        // Draws whatever the questions should draw
        public void Draw()
        {
        }
    }
}
