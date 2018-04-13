using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundRoomSuccess : RoomVariables {

//	public override void PartialSuccess(){
//
//		Debug.Log ("YOU WON!");
//		GetComponent<RoundRoomWalls> ().CmdRandomizeEverything ();
//	}
//
//	public override void CompleteSuccess(){
//
//		Debug.LogWarning ("You passed this Puzzle");
//
//		GetComponent<RoundRoomWalls> ().passed = true;
//		GetComponent<RoundRoomWalls> ().CloseWalls (false);
//		OpenDoorToNextLevel ();
//	}
//
//	public override void Failure(){
//
//		GetComponent<RoundRoomWalls> ().CloseWalls (false);
//		GetComponent<RoundRoomWalls> ().usedCorrectSymbolMaterialIndex.Clear ();
//		GetComponentInChildren<RoundRomCenter> ().activeRandom = true;
//		//Fail ();
//		GetComponent<RoundRoomWalls>().TriggerFailure();
//	}
}
