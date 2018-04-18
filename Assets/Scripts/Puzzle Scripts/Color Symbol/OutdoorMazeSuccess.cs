using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutdoorMazeSuccess : RoomVariables {

    [SerializeField]
    Transform respawnLocatiom;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    public override void RpcCompleteSuccess()
    {

    }

    public override void PartialSuccess()
    {

    }

    public override void RpcFailure()
    {
        player.transform.position = respawnLocatiom.position;
        Fail();
    }
}
