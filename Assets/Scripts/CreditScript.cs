using UnityEngine;
using System.Collections;

public class CreditScript : MonoBehaviour
{
    public int buffer = 10;
    public float creditSpeed = 50.0f;
    public TextAsset credits;

    public Rect RestartButton = new Rect(0f, 60f, 50f, 20f);

    public GUISkin skin;
    public Font titleFont;
    private Font defaultFont;
    private string titleText = "CREDITS";    

    private float amount; // credit offset amount
    private int repeatNum = 0;
    private string creditText = "";
    private int creditLength = 0;
    private float startTime = 0.0f;

	// Use this for initialization
	void Start ()
    {
        if (skin != null)
        {
            defaultFont = skin.font;
        }
        creditText = credits.text;
        startTime = Time.time;
    }
	
	// Update is called once per frame
	void Update ()
    {
        amount = (Time.time-startTime)*creditSpeed - (float)((creditLength+Screen.height)*repeatNum);
		if(amount >= creditLength+Screen.height)
		{
			amount = 0;
			repeatNum++;
		}
    }

    void OnGUI()
    {
        GUI.skin = skin;

        // Draw the "Credits" Label
        if (titleFont != null)
        {
            GUI.skin.font = titleFont;
        }
        GUI.contentColor = Color.black;
        GUI.Label(new Rect(Screen.width * 0.2f, Screen.height * 0.1f, Screen.width * 0.6f, Screen.height * 0.1f), titleText);
        GUI.skin.font = defaultFont;

        GUILayout.BeginArea(new Rect(0, Screen.height*0.2f, Screen.width, Screen.height * 0.8f));

        // Draw the Scrolling Box
        GUI.skin.box.alignment = TextAnchor.MiddleCenter;
        //Vector2 sizeOfBox = GUI.skin.GetStyle("Box").CalcSize(new GUIContent(creditText));
        Vector2 sizeOfBox = GUI.skin.GetStyle("label").CalcSize(new GUIContent(creditText));
        creditLength = (int)(sizeOfBox.y + 2.0f * buffer);
        GUI.Box(new Rect((Screen.width - sizeOfBox.x) / 2.0f - buffer, Screen.height - amount, sizeOfBox.x + 2.0f * buffer, creditLength), creditText);

        GUILayout.EndArea();

        // Replay Button
        if (GUI.Button(adjRect(RestartButton), "Replay?"))
        {
            Application.LoadLevel("Title");
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