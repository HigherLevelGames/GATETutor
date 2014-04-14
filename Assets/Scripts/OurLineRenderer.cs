using UnityEngine;
using System.Collections;

public class OurLineRenderer : MonoBehaviour
{
    public Vector2 point1 = new Vector2(0, 0);
    public Vector2 point2 = new Vector2(20, 20);
    public int lineWidth = 5;
    private Texture2D blankTexture;

	// Use this for initialization
	void Start () {
        blankTexture = new Texture2D(1, 1);
        blankTexture.SetPixel(0, 0, Color.white);
        blankTexture.Apply();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SetStart(Vector3 p)
    {
        Vector3 temp = Camera.main.WorldToScreenPoint(p);
        point1 = new Vector2(temp.x, Screen.height - temp.y);
    }

    void SetEnd(Vector3 p)
    {
        Vector3 temp = Camera.main.WorldToScreenPoint(p);
        point2 = new Vector2(temp.x, Screen.height - temp.y);
    }

    void OnGUI()
    {
        // first horizontal line
        float mid = (point1.x + point2.x) / 2.0f;
        //Debug.Log(mid);
        Rect line = new Rect(point1.x, point1.y, mid - point1.x, lineWidth);
        GUI.DrawTexture(line, blankTexture);

        // vertical
        line = new Rect(mid - lineWidth/2, Mathf.Min(point1.y, point2.y) + lineWidth/2, lineWidth, Mathf.Abs(point2.y - point1.y));
        GUI.DrawTexture(line, blankTexture);

        // second horizontal
        line = new Rect(mid, point2.y, point2.x - mid, lineWidth);
        GUI.DrawTexture(line, blankTexture);
    }
}
