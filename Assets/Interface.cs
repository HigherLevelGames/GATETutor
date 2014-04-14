using UnityEngine;
using System.Collections;

[RequireComponent (typeof(GUITexture))]
public class Interface : MonoBehaviour
{
    public Texture2D texture;
    public Texture2D glowTexture;
    private bool isOn = false;
    public Rect canvasRect = new Rect(0, 0, 80, 80);
    private Texture2D wireTexture;
    
	// Use this for initialization
	void Start ()
    {
        if (texture != null)
        {
            this.guiTexture.texture = texture;
        }
        wireTexture = new Texture2D(256, 256);
        for (int i = 0; i < 256; i++)
        {
            for (int j = 0; j < 256; j++)
            {
                wireTexture.SetPixel(i, j, Color.clear);
                if (i == 0 || j == 0 || i == 255 || j == 255)
                {
                    wireTexture.SetPixel(i, j, Color.green);
                }
            }
        }
        wireTexture.Apply();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Toggle();
        }
	}

    void OnGUI()
    {
        GUI.DrawTexture(screenAdjRect(adjRect(canvasRect)), wireTexture);
    }

    void Toggle()
    {
        isOn = !isOn;
        if (isOn)
        {
            TurnOn();
        }
        else
        {
            TurnOff();
        }
    }

    void TurnOn()
    {
        if (glowTexture != null)
        {
            this.guiTexture.texture = glowTexture;
        }
    }

    void TurnOff()
    {
        if (texture != null)
        {
            this.guiTexture.texture = texture;
        }
    }

    public Rect getScreenRect()
    {
        return screenAdjRect(canvasRect);
    }

    Rect screenAdjRect(Rect r)
    {
        return new Rect(
                r.x,
                Screen.height - r.y - r.height,
                r.width,
                r.height);
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
