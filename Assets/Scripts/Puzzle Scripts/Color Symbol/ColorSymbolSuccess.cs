using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ColorSymbolSuccess : RoomVariables  {


	public override void CompleteSuccess(){

		Debug.LogWarning ("You passed this Puzzle");
		GetComponent<RoomVariables> ().passed = true;
		OpenDoorToNextLevel ();
        roomLoader.LoadRoom(RoomLoader.Room.roundMaze);
	}


	public override void Failure(){

		Debug.Log ("Wrong password");
		GetComponent<RoomVariables> ().Fail ();
	}
}