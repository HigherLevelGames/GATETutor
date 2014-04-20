using UnityEngine;
using System.Collections;

[RequireComponent (typeof(GUITexture))]
public class Interface : MonoBehaviour
{
    public Texture2D texture;
    public Texture2D glowTexture;
    private bool isOn = false;
    
	// Use this for initialization
	void Start ()
    {
        if (texture != null)
        {
            this.guiTexture.texture = texture;
        }
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
}
