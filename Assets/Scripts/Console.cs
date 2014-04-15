using UnityEngine;
using System.Collections;

public class Console : MonoBehaviour
{
    public GUISkin skin;
    public Font font;
    private string title = "";
    private Rect rctWindow = new Rect(0, 0, Screen.width * 0.5f, Screen.height * 0.25f);
    private Vector2 scrollPosition = Vector2.zero;
    private ArrayList consoleLines = new ArrayList();

    void AddLine(string line)
    {
        consoleLines.Add(PlayerPrefs.GetString("Username") + "@GATETutor:~$ " + line);
        scrollPosition.y = Mathf.Infinity; // scroll to bottom
    }

    void ClearConsole()
    {
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
        rctWindow = GUI.Window(0, rctWindow, winFunc, title);
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
        float consoleHeight = consoleLines.Count * GUI.skin.GetStyle("Label").CalcHeight(new GUIContent("Hello World"), consoleWidth);
        Rect scrollRect = new Rect(sideBuffer, topBuffer, winWidth, rctWindow.height - topBuffer - bottomBuffer);
        
        // console background
        Vector2 temp = new Vector2(Input.mousePosition.x,Screen.height - Input.mousePosition.y);
        GUI.backgroundColor = (rctWindow.Contains(temp)) ? Color.black : Color.black * 0.8f;
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
            GUI.skin.label.normal.textColor = (l.Contains("Incorrect")) ? Color.red : Color.green;
            GUI.Label(new Rect(0+5, offset, consoleWidth, lineHeight), l);
            offset += lineHeight;
        }

        // end scroll view and make window draggable
        GUI.EndScrollView();
        GUI.DragWindow();
    }
}