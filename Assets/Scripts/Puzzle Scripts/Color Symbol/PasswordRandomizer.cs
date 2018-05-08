using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//Randomizes a password and assigns matching correct information to clues that can be used by the players to figure out the password.

public class PasswordRandomizer : NetworkBehaviour
{
    //Variables needed for password randomization & assignment
    public Password passwordManager;							
    public List<PasswordButton> unsetPasswordButtons;
    public int passwordLength;

    //Variables needed for password clues randomization & assignment
    public PuzzleClues P1Clues, P2Clues;
    public List<Color> symbolColors;
    public List<Material> symbols;

    // Use this for initialization
    void Start()
    {
        //Makes sure password lenght can't be to set to longer than the number of availible buttons.
        if (passwordLength > unsetPasswordButtons.Count)
        {
            passwordLength = unsetPasswordButtons.Count;
            Debug.LogWarning("Password set to too long. Password length adjusted to " + unsetPasswordButtons.Count);
        }
    }
    //Starts the puzzle by randomizing the password and assigns values to clues on server to send to clients. 
    public void StartPuzzle()
    {
        passwordManager = GameObject.FindGameObjectWithTag("PasswordManager").GetComponent<Password>();

        if (isServer)
        {
			//Initializes by finding nessesary game objects assigning password length
            RpcStartPuzzleClues(passwordLength);

            for (int i = 0; i < unsetPasswordButtons.Count; i++)
            {
				//Generates random values used to randomize the password and create matching clues
                int _randomButton = Random.Range(0, unsetPasswordButtons.Count - i);
                int _randomSymbol = Random.Range(0, symbols.Count - i);

                if (i < passwordLength)
                {
                    if (i < symbolColors.Count - i)
                    {
						//Generates random color to link each pair of clues together
						int _randomColor = Random.Range(0, symbolColors.Count - i);
						//Uses the randomized values to create clues
                        RpcSetRandomPuzzleClues(_randomSymbol, _randomColor);
                    }
                    else
                    {
                        Debug.LogWarning("Not enough availible colors to assign clues for the whole password");
                    }
                }
				//Uses the randomized values to set the password
                RpcSetRandomPassword(i + 1, _randomButton, _randomSymbol);
            }
        }
    }
    [ClientRpc]
    void RpcSetRandomPuzzleClues(int _randomSymbol, int _randomColor)
    {
		//Provides the nessesary information to allow players to figure out the password using the availible clues (Symbol material, order & color that links the two halves of the clue together)
        P1Clues.SetPuzzleClues(symbols[_randomSymbol], symbolColors[_randomColor]);
        P2Clues.SetPuzzleClues(symbols[_randomSymbol], symbolColors[_randomColor]);
		//Removes the color used to link the clues so it can't be selected again
        symbolColors.RemoveAt(_randomColor);
    }
	[ClientRpc]
	void RpcSetRandomPassword(int _index, int _randomButton, int _randomSymbol)
	{
		//Adds the selected button to the password manager (Not nessesary, but useful for debuging purposes).
		passwordManager.passwordButtons.Add(unsetPasswordButtons[_randomButton]);  
		//If buttons is going to be used as a part of the password it's assigned a buttonOrderID value
		if (_index <= passwordLength)
		{
			unsetPasswordButtons[_randomButton].SetPasswordButton(_index, symbols[_randomSymbol]);
		}
		else 	//else if button isn't going to be used as a part of the password it's given a default incorrect buttnnOrderID of '0'
		{
			unsetPasswordButtons[_randomButton].SetPasswordButton(0, symbols[_randomSymbol]);
		}
		//Removes the selected button and symbol material so they can't be selected again
		unsetPasswordButtons.RemoveAt(_randomButton);	
		symbols.RemoveAt(_randomSymbol);				
	}
	[ClientRpc] //Finds the nessesary gameObjects to set password clues	(and assigns password length)
    void RpcStartPuzzleClues(int _passwordLength)
    {
        P1Clues = GameObject.FindGameObjectWithTag("P1").GetComponent<PuzzleClues>();
        P2Clues = GameObject.FindGameObjectWithTag("P2").GetComponent<PuzzleClues>();
        passwordManager.SetPasswordLength(passwordLength);
    }
}