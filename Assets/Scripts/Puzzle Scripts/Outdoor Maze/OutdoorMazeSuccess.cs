using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OutdoorMazeSuccess : RoomVariables
{
    //By Andreas Halldin
    //Handles success and failure of OutdoorMaze

    [SerializeField]
    Transform respawnLocation; //Location where the player should respawn after failing

    public void Respawn(GameObject player) //Respawn the player
    {
        player.transform.position = respawnLocation.position;
    }

    public override void Failure(PlayerCommands playerCmd) //Trigger RpcFailure on all clients
    {
        if (isServer)
        {
            RpcFailure();
        }
        else
        {
            Fail();
            playerCmd.CmdOutdoorMazeFailure();
        }
    }
    [ClientRpc]
    public void RpcFailure() //The paired room triggers its fail (The one with the clock)
    {
        pairedRoom.GetComponent<RoomVariables>().Fail();
    }
}
