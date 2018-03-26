using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordButton : Interractable {

	public Password passwordManager;
	public int buttonOrderID;
	public bool buttonActive = true;
	public Material symbol;

	void PasswordButtonPressed(){

		if (buttonActive) {

			buttonActive = false;

			passwordManager.CheckPassword (buttonOrderID);
			gameObject.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", Color.white);


		}
	}

	void RevertColor(){

		Debug.Log ("Called");
		buttonActive = true;

		gameObject.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", Color.black);


	}

}


