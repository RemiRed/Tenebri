using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Password : MonoBehaviour {

	public RoomVariables success;
	public List<PasswordButton> passwordButtons;		//List of all buttons in the password. Not nessesary but useful for debug purposes.
	public int nextID, passwordLock, passwordLength;

	public int passwordRounds;
	public int curPasswordRound = 1;

	public bool solved = false;

	public void CheckPassword(int _ID){

		if (!solved) {

			if (_ID == nextID) {

				passwordLock = nextID;

				//Checks if password is solved
				if (passwordLock == passwordLength) {

					if (curPasswordRound >= passwordRounds) {

						solved = true;
						success.CompleteSuccess ();

					} else {

						success.PartialSuccess ();
						curPasswordRound++;
						nextID = 0;
					}
				}
				nextID++;
				
			} else {

				if (_ID == 1) {	//If first password button is pressed, password input is restarted to first password button

					nextID = 2;
					passwordLock = 1;

				} else if (_ID != nextID - 1) {

					nextID = 1;
					passwordLock = 0;
				}
				success.Failure ();
				curPasswordRound = 1;
			}
		}
	}

	public void SetPasswordLength(int _length){
	
		passwordLength = _length;
	}
}