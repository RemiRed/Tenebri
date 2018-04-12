using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordButton : Interractable {

	public Password passwordManager;

	public Material symbol;
	public int buttonOrderID;
	public bool lightUpOnPress;

	bool buttonActive = true;

	//Called when button is pressed
	void PasswordButtonPressed(){

		if (buttonActive) {

			buttonActive = false;

			passwordManager.CheckPassword (buttonOrderID);

			if (lightUpOnPress) {
				gameObject.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", Color.white);
			}
		}
	}

	//Called when button is released
	void RevertColor(){

		buttonActive = true;
		gameObject.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", Color.black);
	}

	//Sets initial values. Called from 'PasswordRandomizer'
	public void SetPasswordButton(int _OrderID, Material _Symbol){

		buttonOrderID = _OrderID;
		symbol = _Symbol;
		GetComponent<Renderer> ().material = symbol;
	}

	public void SetPasswordButton(bool _active){

		buttonActive = _active;
	}
}