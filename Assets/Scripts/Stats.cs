using UnityEngine;
using System.Collections;

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

        GUI.Box(new Rect(0,0,Screen.width, Screen.height),text);

        if (GUI.Button(new Rect(Screen.width * 0.75f, Screen.height * 0.8f, Screen.width * 0.25f, Screen.height * 0.1f), "Restart"))
        {
            Application.LoadLevel("Title");
        }
    }
}
