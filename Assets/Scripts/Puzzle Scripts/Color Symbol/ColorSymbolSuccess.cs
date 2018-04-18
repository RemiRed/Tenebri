using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ColorSymbolSuccess : RoomVariables  {

    [ClientRpc]
	public override void RpcCompleteSuccess(){

		Debug.LogWarning ("You passed this Puzzle");
		GetComponent<RoomVariables> ().passed = true;
		OpenDoorToNextLevel ();
	}

    [ClientRpc]
	public override void RpcFailure(){

		Debug.Log ("Wrong password");
		GetComponent<RoomVariables> ().Fail ();
	}
}