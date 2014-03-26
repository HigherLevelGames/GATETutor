using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour
{
    private int scene = 0;
    // 0 = title
    // 1 = instructions

    public Rect PlayButton = new Rect(0f, 0f, 50f, 20f);
    public Rect InstructButton = new Rect(0f, 60f, 50f, 20f);
    public Rect BackButton = new Rect(0f, 60f, 50f, 20f);
    public string GameTitle = "GATE Tutor";
    public Font titleFont;

    private string titleText = "GATE Tutor";
    private Font defaultFont;

    public GUISkin skin;

    // Use this for initialization
    void Start()
    {
        if (skin != null)
        {
            defaultFont = skin.font;
        }
        PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        GUI.skin = skin;
        if (titleFont != null)
        {
            //GUI.contentColor = Color.green;
            GUI.skin.font = titleFont;
        }//*/
        GUI.Label(new Rect(Screen.width * 0.20f, Screen.height * 0.1f, Screen.width * 0.6f, Screen.height * 0.2f), titleText);
        //GUI.contentColor = Color.cyan;
        GUI.skin.font = defaultFont;

        Rect tempBox;
        switch (scene)
        {
        case 0:
            tempBox = new Rect(PlayButton.x / 100.0f * Screen.width,
                    PlayButton.y / 100.0f * Screen.height,
                    PlayButton.width / 100.0f * Screen.width,
                    PlayButton.height / 100.0f * Screen.height
                    );
            if (GUI.Button(tempBox, "Play"))
            {
                Application.LoadLevel("MainGame");
            }
            tempBox = new Rect(InstructButton.x / 100.0f * Screen.width,
                    InstructButton.y / 100.0f * Screen.height,
                    InstructButton.width / 100.0f * Screen.width,
                    InstructButton.height / 100.0f * Screen.height
                    );
            if (GUI.Button(tempBox, "Instructions"))
            {
                titleText = "Instructions";
                scene = 1;
            }
            break;
        case 1: // Instructions
            tempBox = new Rect(BackButton.x / 100.0f * Screen.width,
                    BackButton.y / 100.0f * Screen.height,
                    BackButton.width / 100.0f * Screen.width,
                    BackButton.height / 100.0f * Screen.height
                    );
            if (GUI.Button(tempBox, "Back"))
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
}
