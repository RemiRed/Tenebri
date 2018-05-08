using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//Manages success and failure for 'ColorSymbol' 
//(Designed to function from both server and client, but not certain if both are needed)

public class ColorSymbolSuccess : RoomVariables  {

	////override method inherited from 'RoomVaribles'
	public override void CompleteSuccess ()
	{
		if (isServer) {
			RpcCompleteSuccess ();
		} else {
			roomPassed = true;
			OpenDoorToNextLevel ();
			roomLoader.LoadRoom (RoomLoader.Room.roundMaze);
		}
	}
    [ClientRpc]
	public void RpcCompleteSuccess()
	{
		roomPassed = true;
		OpenDoorToNextLevel ();
		roomLoader.LoadRoom (RoomLoader.Room.roundMaze);
	}

	//override method inherited from 'RoomVaribles'
	public override void Failure ()
	{
		if (isServer) {
			RpcFailure ();
		} else {
			Fail ();
		}
	}
    [ClientRpc]
	public void RpcFailure()
	{
		Fail ();
	}
}