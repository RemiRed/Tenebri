﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ButtonStartRoom : Interractable
{

    [SerializeField]
    private RoomLoader roomLoader;

    [SerializeField]
    float doorDelay;

    bool activated = false;
    

    void Press()
    {
        if (activated)
        {
            return;
        }
        activated = true;
        roomLoader.OpenRoomDoors();
    }
}
