using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordButton : Interractable {

	public Password passwordManager;
	public GameObject graphicalObject;
	public Material symbol;
	public int buttonOrderID, materialIndex;
	public bool lightUpOnPress;

	bool buttonActive = true;

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

	//Sets initial values. Ajusting order ID & material
	public void SetPasswordButton(int _OrderID, Material _Symbol){

		buttonOrderID = _OrderID;
		symbol = _Symbol;
		Material[] _materials = graphicalObject.GetComponent<Renderer> ().materials;
		_materials [materialIndex] = symbol;
		graphicalObject.GetComponent<Renderer> ().materials = _materials;
	}
	public void SetPasswordButton(bool _active){

		buttonActive = _active;
	}
}