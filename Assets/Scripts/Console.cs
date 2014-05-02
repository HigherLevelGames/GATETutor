using UnityEngine;
using System.Collections;

public class Console : MonoBehaviour
{
    public GUISkin skin;
    public Font font;
    private string title = "";
    private Rect rctWindow = new Rect(Screen.width * 0.1f, Screen.height * 0.7f, Screen.width * 0.5f, Screen.height * 0.25f);
    private Vector2 scrollPosition = Vector2.zero;
    private ArrayList consoleLines = new ArrayList();
    public ArrayList StepAttemptHistory = new ArrayList();

    void AddLine(string line)
    {
        StepAttemptHistory.Add(line);
        consoleLines.Add(PlayerPrefs.GetString("Username") + "@GATETutor:~$ " + line);
        scrollPosition.y = Mathf.Infinity; // scroll to bottom
    }

    void ClearConsole()
    {
        StepAttemptHistory.Add("\t");
        consoleLines.Clear();
    }

    void SetTitle(string t)
    {
        title = t; // "Current Task: " + currentTask.question;
    }

    void OnGUI()
    {
        if (skin != null) GUI.skin = skin;
        if (font != null) GUI.skin.font = font;
        // draw a window
        GUI.skin.label.fontSize = 20;
        rctWindow = GUI.Window(0, rctWindow, winFunc, title);
    }

    void OnDestroy()
    {
        string hello = "";
        foreach (string step in StepAttemptHistory)
        {
            hello += step;
            if(step != "\t")
            {
                hello += "\n";
            }
        }
        PlayerPrefs.SetString("StepHistory", hello);
    }

    // the actual window
    void winFunc(int id)
    {
        // dimension variables
        float sideBuffer = 13;
        float topBuffer = 26;
        float bottomBuffer = 14;
        float winWidth = rctWindow.width - sideBuffer * 2;
        float vScrollWidth = 18;
        float consoleWidth = winWidth - vScrollWidth;
        float consoleHeight = 0;
        for (int i = 0; i < consoleLines.Count; i++)
        {
            consoleHeight += GUI.skin.GetStyle("Label").CalcHeight(new GUIContent((string)consoleLines[i]), consoleWidth);
        } 
        Rect scrollRect = new Rect(sideBuffer, topBuffer, winWidth, rctWindow.height - topBuffer - bottomBuffer);
        
        // console background
        Vector2 temp = new Vector2(Input.mousePosition.x,Screen.height - Input.mousePosition.y);
        GUI.backgroundColor = Color.black;
        GUI.Box(scrollRect, "");
        GUI.backgroundColor = Color.white; // white again for vertical scroll bar

        // begin scroll view
        scrollPosition = GUI.BeginScrollView(
                scrollRect, // scroll rectangle
                scrollPosition,
                new Rect(0, 0, consoleWidth, consoleHeight) // view inside scroll rectangle
                );

        // write each line (as a GUI.Label) into the scrollable console
        float offset = 0;
        foreach (string l in consoleLines)
        {
            float lineHeight = GUI.skin.GetStyle("Label").CalcHeight(new GUIContent(l), consoleWidth);
            Color c = Color.white;
            if (l.Contains("Incorrect") && l.Contains("Correct"))
            {
                c = Color.cyan;
            }
            else if (l.Contains("Incorrect"))
            {
                c = Color.red;
            }
            else if (l.Contains("Correct") || l.Contains("Complete"))
            {
                c = Color.green;
            }
            else if (l.Contains("Hint"))
            {
                c = Color.yellow;
            }
            GUI.skin.label.alignment = TextAnchor.MiddleLeft;
            GUI.skin.label.normal.textColor = c;
            GUI.Label(new Rect(0+5, offset, consoleWidth, lineHeight), l);
            offset += lineHeight;
        }

        // end scroll view and make window draggable
        GUI.EndScrollView();
        //GUI.DragWindow();
    }
}