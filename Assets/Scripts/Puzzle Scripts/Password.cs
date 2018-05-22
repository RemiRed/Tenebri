using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Password : MonoBehaviour {

    public RoomVariables result;						//Script to be called when player either succeeds or fails the password
	public List<PasswordButton> passwordButtons;		//List of all buttons in the password. Not nessesary but useful for debuging purposes.
	public int passwordRounds;							//The number of times the password needs to be solved
	public bool resetRounds;							//If the number of times a password needs to be solved should be reset completely as well

	int nextID = 1, 									//The ID of the next correct button
		passwordLength,  								//The length of the password
		curPasswordRoundsLeft;							//The current number of times left needed to solve the password 

	bool solved = false;								//If the password is solved. Prevents further actions to have any effects

	void Start(){

		curPasswordRoundsLeft = passwordRounds;
	}
	//Called to check if a correct password button was pressed 
	public void CheckPassword(int _ID){

		if (!solved) {

			//If correct button was pressed
			if (_ID == nextID) {

				//Checks if password is solved
				if (_ID == passwordLength) {

					//If this was the final round
					if (curPasswordRoundsLeft <= 1) {

						solved = true;
						result.CompleteSuccess ();

                    } else {
						
						result.PartialSuccess ();
                        nextID = 0;
						curPasswordRoundsLeft--;
					}
				}
				nextID++;
				
			} else { //if wrong button was pressed

				//Handles different cases what nextID should be set as when not pressing the nect passoword button
				if (_ID == 1) {	//If first password button is pressed, password input is restarted to first password button

					nextID = 2;

				} else if (_ID != nextID - 1) {	//Allows the same button to be pressed multiple times without resetting the password

					nextID = 1;
				}

				result.Failure ();
                if (resetRounds) {
					curPasswordRoundsLeft = passwordRounds;
				}
			}
		}
	}
	//Sets the passwords' length 
	public void SetPasswordLength(int _length){
	
		passwordLength = _length;
	}
}