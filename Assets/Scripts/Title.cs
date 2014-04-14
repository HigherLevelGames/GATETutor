using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour
{
    // Title Vars
    public string GameTitle = "GATE Tutor";
    public Font titleFont;
    public GUISkin titleSkin;

    // Button Vars
    public Font buttonFont;
    public GUISkin buttonSkin;
    public Rect PlayButton = new Rect(0f, 0f, 50f, 20f);
    public Rect InstructButton = new Rect(0f, 60f, 50f, 20f);
    public Rect BackButton = new Rect(0f, 60f, 50f, 20f);

    // Private Vars
    private string titleText = "";
    private int scene = 0;
    // 0 = title
    // 1 = instructions

    // Use this for initialization
    void Start()
    {
        titleText = GameTitle;
        PlayerPrefs.DeleteAll();
    }

    void OnGUI()
    {
        GUI.skin = titleSkin;
        GUI.skin.font = titleFont;
        GUI.skin.box.alignment = TextAnchor.MiddleCenter;
        GUI.contentColor = Color.black;
        GUI.Box(new Rect(Screen.width * 0.20f, Screen.height * 0.1f, Screen.width * 0.6f, Screen.height * 0.2f), titleText);

        GUI.skin = buttonSkin;
        GUI.skin.font = buttonFont;
        GUI.contentColor = Color.black;

        switch (scene)
        {
        case 0:
            if (GUI.Button(adjRect(PlayButton), "Play"))
            {
                Application.LoadLevel("MainGame");
            }
            if (GUI.Button(adjRect(InstructButton), "Instructions"))
            {
                titleText = "Instructions";
                scene = 1;
            }
            break;
        case 1: // Instructions
            if (GUI.Button(adjRect(BackButton), "Back"))
            {
                titleText = GameTitle;
                scene = 0;
            }
            string instructText = "How To Play: kfjsdiofwsdmxcvhuis";
            GUI.Label(new Rect(0f, Screen.height * 0.8f, Screen.width, Screen.height * 0.2f), instructText);
            break;
        default:
            break;
        }
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
}
