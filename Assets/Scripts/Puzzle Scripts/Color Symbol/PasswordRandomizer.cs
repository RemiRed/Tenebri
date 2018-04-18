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

	bool started = false;   //Temp varible needed for testing

    void Update()
    {

        //Temp function neeced for testing
        if (isServer && Input.GetButtonDown("Jump") && !started)
        {

            started = true;
            StartPuzzle();
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
        print("StartPuzzle");
		passwordManager = GameObject.FindGameObjectWithTag ("PasswordManager").GetComponent<Password> ();
        print(isServer);
		//if (isServer) {
            print("is server");
			RpcStartPuzzleClues (passwordLengt);

			for (int i = 0; i < unsetPasswordButtons.Count; i++) {

				int _randomButton = Random.Range (0, unsetPasswordButtons.Count-i);	
				int _randomSymbol = Random.Range (0, symbols.Count-i);
				int _randomColor = Random.Range (0, symbolColors.Count-i);

				if (i < passwordLengt) {
				
					if (i < symbolColors.Count-i) {

						RpcSetRandomPuzzleClues (_randomSymbol, _randomColor);
					
					} else {

						Debug.LogWarning ("Not enough colors to assign clues for the whole password");
					}
				}
				RpcSetRandomPassword (i+1,_randomButton,_randomSymbol);
			}
		//}
	}
		
	[ClientRpc]
	void RpcSetRandomPassword(int _index,int _randomButton, int _randomSymbol){

		passwordManager.passwordButtons.Add (unsetPasswordButtons [_randomButton]);	//Not nessesary, but useful for debuging purposes.

		if (_index <= passwordLengt) {

			unsetPasswordButtons [_randomButton].SetPasswordButton (_index, symbols [_randomSymbol]);

		} else {

			unsetPasswordButtons [_randomButton].SetPasswordButton (0, symbols [_randomSymbol]);
		}
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