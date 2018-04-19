using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ColorSymbolSuccess : RoomVariables  {

	public override void CompleteSuccess ()
	{
		Debug.Log (isServer);
		if (isServer) {

			RpcCompleteSuccess ();
		} else {
			GetComponent<RoomVariables> ().passed = true;
			OpenDoorToNextLevel ();
			roomLoader.LoadRoom (RoomLoader.Room.roundMaze);
		}
	}
		
    [ClientRpc]
	public void RpcCompleteSuccess(){

		if (isServer) {
			Debug.Log ("Color symbol success server:" + isServer);
			Debug.LogWarning ("You passed this Puzzle");
			GetComponent<RoomVariables> ().passed = true;
			OpenDoorToNextLevel ();
			roomLoader.LoadRoom (RoomLoader.Room.roundMaze);
		}
	}

	public override void Failure ()
	{
		Debug.Log (isServer);
		if (isServer) {
			RpcFailure ();
		} else {
			Debug.Log ("Wrong password");
			GetComponent<RoomVariables> ().Fail ();
		}
	}

    [ClientRpc]
	public void RpcFailure(){

		if (isServer) {
			Debug.Log ("server: " + isServer);
			Debug.Log ("Wrong password");
			GetComponent<RoomVariables> ().Fail ();
		}
	}
}