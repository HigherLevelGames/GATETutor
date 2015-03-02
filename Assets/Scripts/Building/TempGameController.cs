using UnityEngine;
using System.Collections;

//
// Temporary Game Controller
// 
// Will be using mostly the 3D Tower Defense Controller instead
// For now, temporarily implement the 2D controller part for the Building Controller
// Each Building has its own Building Controller
// Game Controller manages the buildings, and different phases for the game
//
// May want to reuse some code in PedagogicalModule.cs (especially for the Console)
// Basically, this will be the centerpoint where DataCell.cs can send its events to
// this class should have event handlers for all possible actions
// Pedagogical Tasks are synonymous to game mode phases (every phase requires player to perform some action)
//

public class TempGameController : MonoBehaviour {

	public int numBits; // 0's and 1's used as currency to purchase building upgrades
	// may want to use two different currency systems (positive & negative?)

	// number of gates user has to use
	public int numOR;
	public int numAND;
	public int numNOT;
	public int numNOR;
	public int numNAND;
	public int numXOR;


	// determines what to show user on screen (3D mode, 2D mode, etc.)
	// name partly misleading... i.e. we'll want different game modes (tutorial, puzzle, etc.)
	enum gameMode
	{
		mainTowerDefense, // 3D Mode
		buildingManager // 2D Mode
	}
	gameMode curMode = gameMode.buildingManager;

	bool isActive = false; // determines whether or not enemies will be active while in 2D mode
	enum phase2D
	{
		mainState,
		inputState,
		outputState
	}
	phase2D curPhase = phase2D.mainState;

	// pretty sure Tower Defense assets will already have this in place
	int phase3D; // enum see below
	int numWave; // current wave, i.e. level of difficulty. Helps determine win state
	int timer; // time until next wave

	public GameObject curBuilding; // the current building to manage

	// Use this for initialization
	void Start () { }
	
	// Update is called once per frame
	void Update () { }

	#region phase2D Main State Events
	void ShowCellInfo(GameObject cell)
	{
		// Action: Click existing cells to get cell feedback
		// display info on console
		// could probably reuse RegisterInput() below
	}

	void DeleteCell(GameObject cell)
	{
		// Action: Drag existing cells into trash bin to delete cell
		// increase number of available gates, and other game information accordingly
	}

	void PlaceGate(GameObject cell)
	{
		// Action: Click and drag gates, and release to place gate onto cells (done!)
		// change cell tag accordingly to gate's tag (almost done...)
		// save cell information
		// determine possible inputs from cell location
		// highlight accordingly by calling events on BuildingController.cs
		// change to input state
	}
	#endregion

	// Event used for both input and output states
	void ExitCell(GameObject cell) // argument probably not needed
	{
		// Action: click on non-input to exit and go back to main state
	}

	#region phase2D Input State Events
	int inputToSet = 0; // 0 = primary, 1 = secondary
	// function could probably be used as generic, DataCell was clicked function, and just register events based on curPhase
	void RegisterInput(GameObject cell)
	{
		// Action: click on two inputs for cell
		// update cell info accordingly
		// See DataCell.cs OnMouseUpAsButton()

		// if chose an input that was already chosen, deselect input

		// if finished choosing two inputs
		// determine possible outputs from cell location
		// highlight accordingly by calling events on BuildingController.cs
		// change to output state
	}
	#endregion

	#region phase2D Output State Events
	// Could reuse RegisterInput(GameObject cell)
	// Action: click on output for cell
	// update cell info accordingly
	// make all cells active again by \
	// highlight accordingly by calling events on BuildingController.cs
	// change to main state
	#endregion
}
