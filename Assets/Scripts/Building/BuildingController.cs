using UnityEngine;
using System.Collections;

public class BuildingController : MonoBehaviour
{
	public int numCol = 5;
	public int numRow = 3;
	public int numInputs = 4;
	public int numOutputs = 2;
	public int consoleW = 0;

	public GameObject Grid;
	public GameObject Inputs;
	public GameObject Outputs;

	public int level = 0;
	public int buildingStyle;
	public int experience = 0;

	// stats
	int power;
	int defense;
	//etc.

	private int w = 0; // w of cells
	private int h = 0; // h of cells

	public TextAsset feedback;

	// Use this for initialization
	void Start ()
	{
		Grid = new GameObject("Grid");
		Grid.transform.parent = this.transform;
		Inputs = new GameObject("Inputs");
		Inputs.transform.parent = this.transform;
		Outputs = new GameObject("Outputs");
		Outputs.transform.parent = this.transform;

		w = (Screen.width-consoleW)/(numCol+2); // 2 for input and output
		h = Screen.height/Mathf.Max(numRow, numInputs, numOutputs);
		for(int i = 0; i < numRow; i++)
		{
			GameObject row = new GameObject("R"+i);
			for(int j = 0; j < numCol; j++)
			{
				makeCell(row, "C"+(j+1), "None", new Vector2(j+1, i)); // col=x, row=y
			}
			row.transform.parent = Grid.transform;
		}

		for(int i = 0; i < numInputs; i++)
		{
			makeCell(Inputs, "I"+i, "Input", new Vector2(0, i)); // col=x, row=y
		}

		for(int i = 0; i < numOutputs; i++)
		{
			makeCell(Outputs, "O"+i, "Output", new Vector2(numCol+1, i)); // col=x, row=y
		}
	}

	// makes a tagged cell for the parent
	void makeCell(GameObject par, string name, string tag, Vector2 loc)
	{
		GameObject temp = new GameObject(name);
		temp.AddComponent<DataCell>();
		DataCell info = temp.GetComponent<DataCell>();
		info.makeOutline(tag);
		info.location = loc;
		info.size = new Vector2(w, h);
		temp.transform.parent = par.transform;
	}
}
