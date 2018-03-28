using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordButton : Interractable {

	public Password passwordManager;

	public Material symbol;
	public int buttonOrderID;
	public bool lightUpOnPress;

	bool buttonActive = true;

	void PasswordButtonPressed(){

		if (buttonActive) {

			buttonActive = false;

			passwordManager.CheckPassword (buttonOrderID);

			if (lightUpOnPress) {
				gameObject.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", Color.white);
			}
		}
	}

	void RevertColor(){

		buttonActive = true;
		gameObject.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", Color.black);
	}

	public void SetPasswordButton(int _OrderID, Material _Symbol){

		buttonOrderID = _OrderID;
		symbol = _Symbol;
		GetComponent<Renderer> ().material = symbol;
	}
}