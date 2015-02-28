using UnityEngine;
using System.Collections;

// Reused code from StepAnalyzer.cs
public class DataCell : MonoBehaviour
{
	public GameObject Inputs;
	public GameObject Output;
	public Vector2 size // changes based on numCol, max(numRow,numInputs,numOutputs) in BuildingController.cs
	{
		get
		{
			return (Vector2)this.transform.localScale;
		}
		set
		{
			this.transform.localScale = value;
			this.transform.position = new Vector2(loc.x*value.x, loc.y*value.y);
		}
	}
	private Vector2 loc = Vector2.zero;
	public Vector2 location // col=x, row=y
	{
		get
		{
			return loc;
		}
		set
		{
			loc = value;
			this.transform.position = new Vector2(loc.x*size.x, loc.y*size.y);
		}
	}
	public bool available;
	public bool isOn;

	private Texture2D wireTexture;

	public void makeOutline(string tag)
	{
		this.tag = tag;

		int resolution = 256;
		int lineThickness = 5;
		wireTexture = new Texture2D(resolution, resolution);
		for (int i = 0; i < resolution; i++)
		{
			for (int j = 0; j < resolution; j++)
			{
				wireTexture.SetPixel(i, j, Color.clear);
				if (i < lineThickness || j < lineThickness || i > (resolution - 1) - lineThickness || j > (resolution-1) - lineThickness)
				{
					if(tag == "Input")
					{
						wireTexture.SetPixel(i, j, Color.blue);
					}
					else if(tag == "Output")
					{
						wireTexture.SetPixel(i, j, Color.red);
					}
					else
					{
						wireTexture.SetPixel(i, j, Color.green);
					}
				}
			}
		}
		wireTexture.Apply();
	}

	// OnGUI() draws a green square outline
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(this.transform.position.x, this.transform.position.y, size.x, size.y), wireTexture);
	}
}
