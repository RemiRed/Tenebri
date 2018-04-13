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
		GetComponent<RoundRoomWalls> ().CmdRandomizeEverything ();
	}

	public override void CompleteSuccess(){

		Debug.LogWarning ("You passed this Puzzle");
		GetComponent<RoundRoomWalls> ().passed = true;
		GetComponent<RoundRoomWalls> ().CloseWalls (false);
		OpenDoorToNextLevel ();
	}

	public override void Failure(){

		GetComponent<RoundRoomWalls> ().CloseWalls (false);
		GetComponent<RoundRoomWalls> ().usedCorrectSymbolMaterialIndex.Clear ();
		GetComponent<RoundRoomWalls> ().TriggerFailCondition ();
		GetComponentInChildren<RoundRomCenter> ().activeRandom = true;


	}
}
