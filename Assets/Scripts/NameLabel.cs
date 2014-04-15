using UnityEngine;
using System.Collections;

public class NameLabel : MonoBehaviour
{
    string[] tags = { "And", "Nand", "Nor", "Not", "Or" };
    public GUISkin skin;
    public Font font;
    float width = 40;
    float height = 20;
    //float width = 50;
    //float height = 20;

    // Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.skin.box = skin.box;
        GUI.skin.box.fontSize = 15;
        //GUI.contentColor = Color.black;
        //GUI.contentColor = Color.white;
        GUI.backgroundColor = Color.black;
        foreach (string t in tags)
        {
            GameObject[] gates = GameObject.FindGameObjectsWithTag(t);
            foreach (GameObject g in gates)
            {
                Vector3 center = Camera.main.WorldToScreenPoint(g.transform.position);
                if (t == "Not" || t == "Nand") center.x -= 20;
                if (t == "Nor") center.x -= 15;
                center.y = Screen.height - center.y;
                //GUI.Label(new Rect(center.x - width / 2.0f, center.y - height / 2.0f, width, height), t);
                GUI.Box(new Rect(center.x-width/2.0f, center.y-height/2.0f,width,height), t);
            }
        }
    }
}
