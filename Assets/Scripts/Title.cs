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
    public Rect QuitButton = new Rect(0f, 60f, 50f, 20f);

    // Username Input Field Vars
    public Rect UsernameField = new Rect(0f, 80f, 50f, 20f);
    public Rect PasswordField = new Rect(0f, 80f, 50f, 20f);
    private string userName = "User";
    private string passWord = "Password";

    // Private Vars
    private string titleText = "";
    private int scene = 0;
    // 0 = title
    // 1 = instructions
    private bool startGame = false;

    // Use this for initialization
    void Start()
    {
        titleText = GameTitle;
        userName = "User" + Random.Range(0, 9999).ToString("0000");
        PlayerPrefs.DeleteAll();
    }

    void Update()
    {
        if (startGame && !audio.isPlaying)
        {
            Application.LoadLevel("MainGame");
        }
    }

    void OnGUI()
    {
        GUI.skin = titleSkin;
        GUI.skin.font = titleFont;
        GUI.skin.box.alignment = TextAnchor.MiddleCenter;
        GUI.contentColor = Color.black;
        GUI.skin.box.fontSize = 50;
        GUI.Box(new Rect(Screen.width * 0.20f, Screen.height * 0.1f, Screen.width * 0.6f, Screen.height * 0.2f), titleText);

        GUI.skin = buttonSkin;
        GUI.skin.font = buttonFont;
        GUI.contentColor = Color.black;

        switch (scene)
        {
        case 0:
            // User Name Field
            Rect temp = adjRect(UsernameField);
            temp.width /= 3.0f;
            GUI.TextArea(temp, "User Name: ");
            temp.x += temp.width;
            temp.width *= 2.0f;
            userName = GUI.TextField(temp, userName);

            // Password Field
            temp = adjRect(PasswordField);
            temp.width /= 3.0f;
            GUI.TextArea(temp, "Password: ");
            temp.x += temp.width;
            temp.width *= 2.0f;
            passWord = GUI.PasswordField(temp, passWord, '*'/*'☺'*/);

            // Buttons
            if (GUI.Button(adjRect(PlayButton), "Play"))
            {
                audio.Play();
                PlayerPrefs.SetString("Username", userName);
                startGame = true;
            }
            if (GUI.Button(adjRect(InstructButton), "Instructions"))
            {
                audio.Play();
                titleText = "Instructions";
                scene = 1;
            }
            if (GUI.Button(adjRect(QuitButton), "Quit"))
            {
                Application.Quit();
            }
            break;
        case 1: // Instructions
            if (GUI.Button(adjRect(BackButton), "Back"))
            {
                audio.Play();
                titleText = GameTitle;
                scene = 0;
            }
            string instructText = "Click and drag logic gates into the green boxes in order to draw the logic gate diagrams as specified by the command prompt's title.";
            GUI.skin = titleSkin;
            GUI.skin.box.fontSize = 20;
            GUI.skin.box.wordWrap = true;
            GUI.Box(new Rect(Screen.width * 0.2f, Screen.height * 0.4f, Screen.width*0.6f, Screen.height * 0.2f), instructText);
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
