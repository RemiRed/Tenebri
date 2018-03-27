﻿using System.Collections;
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

						Debug.Log ("CORRECT PASSWORD!"); //Replace with some door opening method cool
			}
				
		} else if(_ID == 1){

			Debug.Log ("Reset to 1st");
			nextID = 2;
			passwordLock = 1;

		}else{

//			for (int i = 0; i < passwordButtons.Count; i++) {
//
//				passwordButtons [i].buttonActive = true;
//			}

			nextID = 1;
			passwordLock = 0;
		}
	}
}
