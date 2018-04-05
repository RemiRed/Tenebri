using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Password : MonoBehaviour {

	public List<PasswordButton> passwordButtons;		//List of all buttons in the password. Not nessesary but useful for debug purposes.
	public int nextID, passwordLock, passwordLength;
	public bool solved = false;
		
	public void CheckPassword(int _ID){

		if (!solved) {

			if (_ID == nextID) {

				passwordLock = nextID;

				//Checks if password is solved
				if (passwordLock == passwordLength) {

					Debug.Log ("CORRECT PASSWORD!"); //Replace with some door opening method
					solved = true;
				}
				nextID++;
				
			} else if (_ID == 1) {	//If first password button is pressed, password input is restarted to first password button
			
				nextID = 2;
				passwordLock = 1;

			} else if (_ID != nextID - 1) {	//Resets password input unless last password button is pressed
			
				nextID = 1;
				passwordLock = 0;

				Debug.Log ("CORRECT PASSWORD!"); //Replace with some door opening method cool
			}
				
		} else {
			
			nextID = 1;
			passwordLock = 0;

		}
	}

	public void SetPasswordLength(int _length){
	
		passwordLength = _length;
	}
}