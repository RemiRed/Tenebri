using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manages the information provided for the players in the 'ColorSymbols' pussle that allows players to match colors & symbols to figure out the password

public class ColorSymbolPasswordClues : PuzzleClues {

	public bool numbers; 					//Sets if this is clues with numbers or symbols. (True if with numbers)

	public GameObject clueObject;			//The object that displays the clue that will be instantiated in the scene
	public List<Transform> clueLocations;	//List of possible locations where the clue objects will be instansiated
	public List<Material> symbolNumbers;	//List of materials for the numbers

	int curClueNumber = 0;					//Default index value selecting number materials

	//override method inherited from 'PuzzleClues'
	public override void SetPuzzleClues(Material _symbolMaterial, Color _symbolColor){

		//Selects random location and instansiates the clue object
		int _randomLocation = Random.Range (0, clueLocations.Count);
		GameObject _curClue = Instantiate (clueObject, clueLocations [_randomLocation].position, clueLocations [_randomLocation].rotation,gameObject.transform);

		if (numbers) {

			//Assigns the current number material and increases current number
			_curClue.GetComponent<Renderer> ().material = symbolNumbers [curClueNumber];
			curClueNumber++;

		} else {

			//Assigns a symbol material with values inherited from the symbol material used in the password
			_curClue.GetComponent<Renderer> ().material = new Material (_symbolMaterial);
		}
		//Adjusts color of material to link the clues
		_curClue.GetComponent<Renderer> ().material.color = _symbolColor;
		//Removes possible location from list so clues can't be instansiated on each other
		clueLocations.RemoveAt (_randomLocation);
	}
}