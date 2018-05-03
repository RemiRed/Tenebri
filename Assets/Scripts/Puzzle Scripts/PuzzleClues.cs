using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Template for scripts handling clues or hints for each puzzle.
//Meant to contain virtual methods to be overriden by inheriting scripts

public class PuzzleClues : MonoBehaviour {

	//Used by: 'ColorSymbolPasswordClues'
	public virtual void SetPuzzleClues(Material _material, Color _color){

	}
}
