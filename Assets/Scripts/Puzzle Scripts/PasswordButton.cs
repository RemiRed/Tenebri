using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The functionality of the buttons that belong to a password.
//Functionality when button is pressed and released, as well as the ability to set the button's values

public class PasswordButton : Interractable {

	public Password passwordManager;		//The manager which this button belongs to
	public Material symbol;					//This Button's current material
	public GameObject graphicalObject;		//The gameObject which will represent the button graphically and have it's material adjusted. (Defaults to this gameObject if left null) 
	public int materialIndex,				//The index of the 'graphicalObject''s material that will be adjusted (Default is 0)
				buttonOrderID; 				//This buttons order in the password
	public bool lightUpOnPress;				//If this button should light up if pressed
					
	bool buttonActive = true;				//If button is active and can be interacted with

	void Start(){

		if (graphicalObject == null) {
			graphicalObject = gameObject;
		}
	}

	//Called when button is pressed
	void PasswordButtonPressed(){

		if (buttonActive) {

			buttonActive = false;
			passwordManager.CheckPassword (buttonOrderID);

			if (lightUpOnPress) {
				Material[] _materials = graphicalObject.GetComponent<Renderer> ().materials;
				_materials[materialIndex].SetColor ("_EmissionColor", Color.white);
				graphicalObject.GetComponent<Renderer> ().materials = _materials;
			}
		}
	}
	//Called when button is released
	void RevertColor(){

		buttonActive = true;
		Material[] _materials = graphicalObject.GetComponent<Renderer> ().materials;
		_materials[materialIndex].SetColor ("_EmissionColor", Color.black);
		graphicalObject.GetComponent<Renderer> ().materials = _materials;
	}
	//Sets initial password button values. Ajusting order ID & material
	public void SetPasswordButton(int _OrderID, Material _Symbol){

		buttonOrderID = _OrderID;
		symbol = _Symbol;
		Material[] _materials = graphicalObject.GetComponent<Renderer> ().materials;
		_materials [materialIndex] = symbol;
		graphicalObject.GetComponent<Renderer> ().materials = _materials;
	}
	//sets if password button is active
	public void SetPasswordButton(bool _active){

		buttonActive = _active;
	}
}