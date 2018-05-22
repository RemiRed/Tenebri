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


    //public override void CompleteSuccess()
    //{

    //}

    //public override void PartialSuccess()
    //{

    //}

    //public override void Failure()
    //{
    //    player.transform.position = respawnLocatiom.position;
    //    Fail();
    //}
}
