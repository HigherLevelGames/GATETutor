using UnityEngine;
using System.Collections;

// Reused code from StepAnalyzer.cs
public class DataCell : MonoBehaviour
{
	// http://answers.unity3d.com/questions/36678/world-space-to-pixel-space-accuracy.html
	private float pixelRatio
	{
		get
		{
			return (Camera.main.orthographicSize * 2) / Camera.main.pixelHeight;
		}
	}

	public GameObject Inputs;
	public GameObject Output;
	public Vector2 size // changes based on numCol, max(numRow,numInputs,numOutputs) in BuildingController.cs
	{
		get
		{
			BoxCollider2D bc = this.gameObject.GetComponent<BoxCollider2D>();
			if(bc == null)
			{
				this.gameObject.AddComponent<BoxCollider2D>();
				bc = this.gameObject.GetComponent<BoxCollider2D>();
			}
			return bc.size;
		}
		set
		{
			BoxCollider2D bc = this.gameObject.GetComponent<BoxCollider2D>();
			if(bc == null)
			{
				this.gameObject.AddComponent<BoxCollider2D>();
				bc = this.gameObject.GetComponent<BoxCollider2D>();
			}
			bc.isTrigger = true;
			bc.size = value * pixelRatio;
			bc.center = new Vector2(bc.size.x*0.5f, -bc.size.y*0.5f);
			this.transform.position = Camera.main.ScreenToWorldPoint(new Vector2(loc.x*value.x, Screen.height-loc.y*value.y));
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
			this.transform.position = Camera.main.ScreenToWorldPoint(new Vector2(loc.x*size.x, Screen.height-loc.y*size.y));
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

	GameObject toRegister;
	void TouchUpEventHandler()
	{
		if(toRegister == null || this.tag != "None")
		{
			return;
		}
		
		// assign entered gate to locked gate
		toRegister.transform.position = this.transform.position + new Vector3(size.x*0.5f, -size.y*0.5f, 1);
		toRegister.GetComponent<ClickAndDrag>().isDraggable = false;
		toRegister.SendMessage("Respawn");
		
		// Tell the Game Controller (Pedagogical Module)
		// to go to the next phase (next step or task) (i.e. input lines)
		//GameObject.Find("GameController").SendMessage("NextStep");
		Debug.Log("Placed gate");
		
		toRegister = null;
	}

	// once gate enters the trigger
	// analyzes whether it is wrong or correct
	void OnTriggerEnter2D(Collider2D col)
	{
		toRegister = col.gameObject;
	}

	void OnTriggerExit2D(Collider2D col)
	{
		toRegister = null;
	}

	// OnGUI() draws a green square outline
	void OnGUI()
	{
		Vector3 pt = Camera.main.WorldToScreenPoint(this.transform.position);
		pt.y = Screen.height-pt.y; // screen to gui space
		GUI.DrawTexture(new Rect(pt.x, pt.y, size.x/pixelRatio, size.y/pixelRatio), wireTexture);
	}
}
