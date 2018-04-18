using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ButtonStartRoom : Interractable
{
    bool activated = false;


    void Press()
    {
        if (activated)
        {
            return;
        }
        activated = true;
        GetComponentInParent<RoomVariables>().OpenDoorToNextLevel();
        roomLoader.CmdLoadRoom(RoomLoader.Room.colorSymbols);
    }
}
