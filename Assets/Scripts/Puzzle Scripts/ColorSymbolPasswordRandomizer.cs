using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ColorSymbolPasswordRandomizer : NetworkBehaviour {

	public Password passwordManager;
	public List<PasswordButton> unsetPasswordButtons;
	public int passwordLengt;

	public PasswordClues P1Clues, P2Clues;
	public List<Color> symbolColors;
	public List<Material> symbols;

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
		
		//StartPuzzle ();
	}

	public void StartPuzzle(){

		passwordManager = GameObject.FindGameObjectWithTag ("PasswordManager").GetComponent<Password> ();

		if (isServer) {

			int randomButtonRange = unsetPasswordButtons.Count;
			int _randomButton;

			int randomSymbolRange = symbols.Count;
			int _randomSymbol;

			int randomColorRange = symbolColors.Count;
			int _randomColor;

			for (int i = 1; i <= passwordLengt; i++) {

				_randomButton = Random.Range (0, randomButtonRange);	
				_randomSymbol = Random.Range (0, randomSymbolRange);
				_randomColor = Random.Range (0, randomColorRange);

				RpcSetRandomPassword (i,_randomButton,_randomSymbol,_randomColor);

				randomButtonRange--;
				randomSymbolRange--;
				randomColorRange--;
			}

			for (int j = 0; j < randomButtonRange; j++) {

				_randomSymbol = Random.Range (0, randomSymbolRange);
				RpcAddRestOfPasswordButtons (j, _randomSymbol);
				randomSymbolRange--;
			}
		}
	}
		
	[ClientRpc]
	void RpcSetRandomPassword(int _index,int _randomButton, int _randomSymbol, int _randomColor){

		passwordManager.passwordButtons.Add (unsetPasswordButtons [_randomButton]);
		passwordManager.passwordButtons [_index - 1].SetPasswordButton (_index, symbols[_randomSymbol]);
		unsetPasswordButtons.RemoveAt (_randomButton);

		P1Clues = GameObject.FindGameObjectWithTag ("P1").GetComponent<PasswordClues> ();
		P2Clues = GameObject.FindGameObjectWithTag ("P2").GetComponent<PasswordClues> ();

		P1Clues.SetClues(symbols[_randomSymbol], symbolColors[_randomColor]);
		P2Clues.SetClues(symbols[_randomSymbol], symbolColors[_randomColor]);

		symbols.RemoveAt (_randomSymbol);
		symbolColors.RemoveAt (_randomColor);

		passwordManager.passwordLength = passwordLengt; //Its unnessesary to have this variable update every time, but it also doesn't do any harm and was easy this way.
	}

	[ClientRpc]
	void RpcAddRestOfPasswordButtons(int _index, int _randomSymbol){

		unsetPasswordButtons [_index].buttonOrderID = 0;
		passwordManager.passwordButtons.Add (unsetPasswordButtons [_index]);

		unsetPasswordButtons [_index].GetComponent<Renderer> ().material = symbols [_randomSymbol];
		symbols.RemoveAt (_randomSymbol);
	}
}