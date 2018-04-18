﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CorridorLever : Interractable
{

    public GameObject exitDoor;
    public GameObject entryDoor;
    [SerializeField]
    RoomLoader roomLoader;

    [SerializeField]
    float doorDelay;

    [SerializeField]
    float leverDelay;

    bool pulled = false;

    void Pull()
    {
        if (!pulled)
        {
            print("PULLED");
            pulled = true;
            playerCmd.RpcCorridorLever();
        }
    }

    void Release()
    {
        playerCmd.CmdCorridorLeverRelease();
        pulled = false;
    }

}
