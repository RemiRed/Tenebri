using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundRoomSuccess : SuccessCondition {

	// Use this for initialization
	void Start () {
		
	}


	
	// Update is called once per frame
	void Update () {
		
	}

	public override void PartialSuccess(){

		Debug.Log ("YOU WON!");
		GetComponent<RoundRoomWalls> ().CmdReRandomizeEverything ();
	}

	public override void CompleteSuccess(){

		Debug.Log ("You passed this Puzzle");
	}
}
