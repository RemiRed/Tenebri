using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PasswordRandomizer : NetworkBehaviour {

	//Variables needed for password randomization & assignment
	public Password passwordManager;
	public List<PasswordButton> unsetPasswordButtons;
	public int passwordLengt;

	//Variables needed for password clues randomization & assignment
	public PuzzleClues P1Clues, P2Clues;
	public List<Color> symbolColors;
	public List<Material> symbols;

<<<<<<< HEAD
=======

>>>>>>> Daniel
	bool started = false;	//Temp varible needed for testing

	void Update(){

		//Temp function neeced for testing
		if (isServer && Input.GetButtonDown("Jump") && !started) {

			started = true;
			Debug.Log ("Starting the puzzle");
			StartPuzzle ();
		}
	}

	// Use this for initialization
	void Start () {

		//Makes sure password lenght can't be to set to longer than the number of availible buttons.
		if (passwordLengt > unsetPasswordButtons.Count) {

			passwordLengt = unsetPasswordButtons.Count;
			Debug.LogWarning ("Password set to too long. Password length adjusted to " + unsetPasswordButtons.Count);
		}

		//StartPuzzle ();
	}

	//Starts the puzzle by randomizing the password and assigns values to clues on server to send to clients. 
	public void StartPuzzle(){

		passwordManager = GameObject.FindGameObjectWithTag ("PasswordManager").GetComponent<Password> ();

		if (isServer) {

			RpcStartPuzzleClues (passwordLengt);

			int randomButtonRange = unsetPasswordButtons.Count;
			int _randomButton;

			int randomSymbolRange = symbols.Count;
			int _randomSymbol;

			int randomColorRange = symbolColors.Count;
			int _randomColor;

			for (int i = 1; i <= unsetPasswordButtons.Count; i++) {

				_randomButton = Random.Range (0, randomButtonRange);	
				_randomSymbol = Random.Range (0, randomSymbolRange);
				_randomColor = Random.Range (0, randomColorRange);

				if (i <= passwordLengt) {
				
					if (i <= symbolColors.Count) {

						RpcSetRandomPuzzleClues (_randomSymbol, _randomColor);
						randomColorRange--;
					
					} else {

						Debug.LogWarning ("Not enough colors to assign clues for the whole password");
					}
				}

				RpcSetRandomPassword (i,_randomButton,_randomSymbol);

				randomButtonRange--;
				randomSymbolRange--;
			}
		}
	}
		
	[ClientRpc]
	void RpcSetRandomPassword(int _index,int _randomButton, int _randomSymbol){

		passwordManager.passwordButtons.Add (unsetPasswordButtons [_randomButton]);	//Not nessesary, but useful for debuging purposes.

		unsetPasswordButtons [_randomButton].SetPasswordButton (_index, symbols [_randomSymbol]);

		unsetPasswordButtons.RemoveAt (_randomButton);
		symbols.RemoveAt (_randomSymbol);
	}

	[ClientRpc]
	void RpcSetRandomPuzzleClues(int _randomSymbol, int _randomColor){

		P1Clues.SetPuzzleClues(symbols[_randomSymbol], symbolColors[_randomColor]);
		P2Clues.SetPuzzleClues(symbols[_randomSymbol], symbolColors[_randomColor]);

		symbolColors.RemoveAt (_randomColor);
	}

	[ClientRpc]
	void RpcStartPuzzleClues(int _passwordLength){

		P1Clues = GameObject.FindGameObjectWithTag ("P1").GetComponent<PuzzleClues> ();
		P2Clues = GameObject.FindGameObjectWithTag ("P2").GetComponent<PuzzleClues> ();
		passwordManager.SetPasswordLength (passwordLengt);
	}
}