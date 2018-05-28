using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutdoorMazeSuccess : RoomVariables
{

    [SerializeField]
    Transform respawnLocatiom;

    public override void Failure(PlayerCommands playerCmd)
    {
        playerCmd.gameObject.transform.position = respawnLocatiom.position;
        Fail();
    }
}
