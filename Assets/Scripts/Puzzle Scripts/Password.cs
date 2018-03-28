using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Password : MonoBehaviour {

	public List<PasswordButton> passwordButtons;
	public int nextID, passwordLock, passwordLength;
		
	public void CheckPassword(int _ID){

		if (_ID == nextID) {

			nextID++;
			passwordLock++;

			if (passwordLock == passwordLength) {

				Debug.Log ("CORRECT PASSWORD!"); //Replace with some door opening method
			}
				
		} else if(_ID == 1){	//If 1st password button is pressed, password input is restarted
			
			nextID = 2;
			passwordLock = 1;

		}else if(_ID != nextID-1){	//Resets password input unless repeating same button 
			
			nextID = 1;
			passwordLock = 0;
		}
	}
}