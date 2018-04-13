using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSymbolSuccess : RoomVariables  {

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
		GetComponent<RoomVariables> ().passed = true;
		OpenDoorToNextLevel ();
	}

	public override void Failure(){

		Debug.Log ("Wrong password");
		GetComponent<RoomVariables> ().Fail ();
	}
}
