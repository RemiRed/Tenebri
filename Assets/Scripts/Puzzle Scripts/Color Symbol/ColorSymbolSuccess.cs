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
        playercommand = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCommands>();
        playercommand.CmdColorSymbolCompleteSuccess();
	}
    [ClientRpc]
	public void RpcCompleteSuccess()
	{
		roomPassed = true;
		OpenDoorToNextLevel ();
        roomLoader.LoadRoom(RoomLoader.Room.roundMaze, 1);
        roomLoader.LoadRoom(RoomLoader.Room.roundMaze, 2);
    }

	//override method inherited from 'RoomVaribles'
	public override void Failure ()
    {
        playercommand = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCommands>();
        playercommand.CmdColorSymbolFailure();
    }
    [ClientRpc]
	public void RpcFailure()
	{
		Fail ();
	}
}