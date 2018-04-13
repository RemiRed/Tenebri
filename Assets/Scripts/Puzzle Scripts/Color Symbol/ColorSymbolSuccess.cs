using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSymbolSuccess : SuccessCondition  {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	public override void PartialSuccess(){

	}

	public override void CompleteSuccess(){

		Debug.LogWarning ("You passed this Puzzle");
		passed = true;
		OpenDoorToNextLevel ();
	}

	public override void Failure(){

		Debug.Log ("Wrong password");
		Fail ();
	}
}
