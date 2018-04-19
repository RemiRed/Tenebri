using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ColorSymbolSuccess : RoomVariables  {

    [ClientRpc]
	public override void RpcCompleteSuccess(){

		Debug.Log ("Color symbol success server:" + isServer);
		Debug.LogWarning ("You passed this Puzzle");
		GetComponent<RoomVariables> ().passed = true;
		OpenDoorToNextLevel ();
        roomLoader.LoadRoom(RoomLoader.Room.roundMaze);
	}

    [ClientRpc]
	public override void RpcFailure(){

		Debug.Log ("server: " + isServer);
		Debug.Log ("Wrong password");
		GetComponent<RoomVariables> ().Fail ();
	}
}