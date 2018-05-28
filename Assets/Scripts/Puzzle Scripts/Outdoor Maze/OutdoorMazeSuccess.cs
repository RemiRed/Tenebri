using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutdoorMazeSuccess : RoomVariables
{

    [SerializeField]
    Transform respawnLocatiom;
    [HideInInspector]
    public GameObject player;

    public override void Failure(PlayerCommands playerCmd)
    {
        player.transform.position = respawnLocatiom.position;
        Fail();
    }
}
