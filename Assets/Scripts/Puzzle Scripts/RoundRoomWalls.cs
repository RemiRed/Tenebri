using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RoundRoomWalls : RoomVariables
{
	
	[SerializeField]
	List<GameObject> walls = new List<GameObject>();
	[SerializeField]
	List<GameObject> buttons = new List<GameObject>();	//Change these from 'GameObject' to 'RoundDoors'

	List<RoundDoors> usedButtons = new List<RoundDoors>();
	List<int> mazeSymbolMaterialIndex = new List<int>();
	public List<int> theseButtonsIndex = new List<int>();

	public Material defaultButtonMaterial;

	[SerializeField]
	int numberOfButtons, curButtonNumber;

	bool foundPath = true;

	void Update()
	{
		//Debug Stuff
		if (isServer && Input.GetKeyDown(KeyCode.G))
		{
			pairedRoom.GetComponent<RoundRoomManager> ().CmdGetWallSymbols ();
			RandomSymbols();
			pairedRoom.GetComponent<RoundMazeMapRoom>().RpcMapButtons();
		}
	}

	void RandomSymbols()
	{
		//Resets to default
		RpcCloseWalls();
		CloseWalls();

		curButtonNumber = 0;	//<<-- How can this set 'curButtonNumber' on Client if run on server? 
		int tempLayer = 0;
		int _curSymbolIndex = 0;
		bool firstLayer = false;

		//Creates list of references to the Index of maze symbol materials to be used
		foreach (GameObject _symbol in pairedRoom.GetComponent<RoundRoomManager>().wallSymbols) {

			mazeSymbolMaterialIndex.Add (_curSymbolIndex);
			_curSymbolIndex++;
		}
		//Sets Maze buttons
		for (int i = 0; i < numberOfButtons; i++)
		{
			//Generates list of possible buttons to be selected
			List<RoundDoors> tempButtons = new List<RoundDoors>();
			foreach (GameObject _button in buttons)
			{
				//Conditions for buttons to be added to list of possible buttons to be selected
				if (!usedButtons.Contains(_button.GetComponent<RoundDoors>()) &&
					(firstLayer == false || (firstLayer == true && _button.GetComponent<RoundDoors>().layer != 1)) &&
					_button.GetComponent<RoundDoors>().layer != tempLayer)
				{
					tempButtons.Add(_button.GetComponent<RoundDoors>());
				}
			}
			//Generates random values for button variables
			int randomButtonInt = Random.Range(0, tempButtons.Count);
			int randomSymbol = Random.Range (0, mazeSymbolMaterialIndex.Count);
			//Calls Client to Find path from these button variables
			RpcFindPath(tempButtons[randomButtonInt].gameObject, true, mazeSymbolMaterialIndex[randomSymbol]);
			//Adjusts varables for next loop
			mazeSymbolMaterialIndex.RemoveAt (randomSymbol);
			tempLayer = tempButtons[randomButtonInt].layer;
			if (tempLayer == 1) firstLayer = true;
			usedButtons.Add(tempButtons[randomButtonInt]);
		}
		//Opens the rooms that has not been entered
		foreach (GameObject _button in buttons)
		{
			RpcFindPath(_button, false,0);
		}
	}

	[ClientRpc]
	void RpcFindPath(GameObject _button, bool _ifButton, int _randomSymbol)
	{
		if (_ifButton) {

			curButtonNumber++;

			//Sets first button to be the correct matching button
			if (curButtonNumber == 1) {

				_button.GetComponent<Renderer> ().material = pairedRoom.GetComponent<RoundRoomManager> ().wallSymbols [_randomSymbol].GetComponent<Renderer> ().material;

			} else {

				_button.GetComponent<Renderer> ().material = pairedRoom.GetComponent<RoundRoomManager> ().wallSymbols [_randomSymbol].GetComponent<Renderer> ().material;

				//Changes color to a non-matching color
				List<Color> _symbolColors = new List<Color> ();
				foreach (Color _color in pairedRoom.GetComponent<RoundRoomManager>().symbolColors) {

					if (_color != _button.GetComponent<Renderer> ().material.color) {

						_symbolColors.Add (_color);
					}
				}
				_button.GetComponent<Renderer> ().material.color = _symbolColors [Random.Range (0, _symbolColors.Count)];
			}
			//Adds button locations to map room
			theseButtonsIndex.Add (_button.GetComponent<RoundDoors> ().buttonNumber);

			//Looks for & opens path. If path fails, marks as 'False' to be Re-Randomized 
			if (!_button.GetComponent<RoundDoors> ().FindPath (_button.GetComponent<RoundDoors> ())) {
				foundPath = false;
			}
			//If Path failed; Re-Randomize everything
			if (curButtonNumber >= numberOfButtons && !foundPath)
			{
				CmdReRandomizeEverything();
			}
		}
		else if (!_button.GetComponent<RoundDoors>().entered)	//Opens remaining unopened rooms
		{
			_button.GetComponent<RoundDoors>().FindPath(_button.GetComponent<RoundDoors>());
		}
	}

	[Command]
	//Starts over if fails to find a working path
	void CmdReRandomizeEverything()
	{
		RandomSymbols();
		pairedRoom.GetComponent<RoundMazeMapRoom>().RpcMapButtons();
	}
	//Resets Everything to default
	[ClientRpc]
	void RpcCloseWalls()
	{
		CloseWalls();
	}
	void CloseWalls()
	{
		foreach (GameObject bwa in walls)
		{
			bwa.SetActive(true);
		}
		foreach (GameObject bwu in buttons)
		{
			bwu.GetComponent<RoundDoors>().entered = false;
			bwu.GetComponent<RoundDoors>().enteredNow = false;
		}
		foreach (GameObject bwe /*named after my gf Ivvie*/ in buttons) 
		{
			bwe.GetComponent<Renderer> ().material = defaultButtonMaterial;
		}
		usedButtons.Clear();
		mazeSymbolMaterialIndex.Clear();
		theseButtonsIndex.Clear();
		foundPath = true;
	}
}