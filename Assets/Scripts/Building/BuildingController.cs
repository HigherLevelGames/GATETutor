using UnityEngine;
using System.Collections;

public class BuildingController : MonoBehaviour
{
	// TODO: if these values change, change DataCell.cs size accordingly
	public int numCol = 5;
	public int numRow = 3;
	public int numInputs = 4;
	public int numOutputs = 2;
	public int consoleW = 0;

	public int level = 0;
	public int buildingStyle;
	public int experience = 0;
	
	//public TextAsset feedback; // See Console.cs
	public GameObject Console;

	// stats
	int power;
	int defense;
	int rof; // speed, i.e. rate of fire
	//etc.

	// children of this GameObject (no need to show in inspector)
	private GameObject Grid; 
	private GameObject Inputs;
	private GameObject Outputs;

	private int w // cell width
	{
		get {
			return (Screen.width-consoleW)/(numCol+2); // 2 to account for input and output columns
		}
	}
	private int h // cell height
	{
		get {
			return Screen.height/Mathf.Max(numRow, numInputs, numOutputs);
		}
	}

	// Use this for initialization
	void Start ()
	{
		Grid = new GameObject("Grid");
		Grid.transform.parent = this.transform;
		Inputs = new GameObject("Inputs");
		Inputs.transform.parent = this.transform;
		Outputs = new GameObject("Outputs");
		Outputs.transform.parent = this.transform;

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
		temp.AddComponent<BoxCollider2D>(); // TODO: Switch over to BoxCollider2D for both Gates and cells
		DataCell info = temp.GetComponent<DataCell>();
		info.makeOutline(tag);
		info.location = loc;
		info.size = new Vector2(w, h);
		temp.transform.parent = par.transform;
	}

	// TODO: When player chooses to manage a building, allow GameController access to its BuildingController
	// Called by GameController whenever User finishes the steps in a task, i.e. phase
	void SetPhase(int phase)
	{
		switch(phase)
		{
		case 0: // main state, dragging gates to cells
			this.BroadcastMessage("Highlight", SendMessageOptions.DontRequireReceiver);
			break;
		case 1: // input state, choosing gate inputs
			// get saved cell which received the gate
			this.BroadcastMessage("Dehighlight", SendMessageOptions.DontRequireReceiver);
			// figure out list of possible cells to highlight (input cells)
			// highlight possible cells
			/*foreach(GameObject cell in possibleCells)
			{
				cell.SendMessage("Highlight", SendMessageOptions.DontRequireReceiver);
			}*/
			break;
		case 2: // output state, choosing gate outputs
			// get saved cell which received the gate
			this.BroadcastMessage("Dehighlight", SendMessageOptions.DontRequireReceiver);
			// figure out list of possible cells to highlight (output cells)
			// highlight possible cells
			/*foreach(GameObject cell in possibleCells)
			{
				cell.SendMessage("Highlight", SendMessageOptions.DontRequireReceiver);
			}*/
			break;
		default:
			Debug.Log("Warning: Unknown phase attempt");
			break;
		}

	}

	/*
	// shortcuts to broadcast message to all cells to go to different phase2D states
	void ActivateCells() { }
	void DeactivateCells() { }
	/**/
}
