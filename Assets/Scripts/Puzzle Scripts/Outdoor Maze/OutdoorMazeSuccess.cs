using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OutdoorMazeSuccess : RoomVariables
{

    [SerializeField]
    Transform respawnLocation;

    GameObject player;

    public void Respawn(GameObject player)
    {
        player.transform.position = respawnLocation.position;
    }
    public override void Failure(PlayerCommands playerCmd)
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
    public void RpcFailure()
    {
        //Fail();
        pairedRoom.GetComponent<RoomVariables>().Fail();
    }
}
