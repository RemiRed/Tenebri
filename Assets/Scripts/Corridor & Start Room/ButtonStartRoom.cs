using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ButtonStartRoom : Interractable
{
    bool activated = false;

    [SerializeField]
    UnloadRooms p1, p2;

    void Press()
    {
        if (activated || !p1.entered || !p2.entered)
        {
            return;
        }
        activated = true;
        GetComponentInParent<RoomVariables>().OpenDoorToNextLevel();
        roomLoader.LoadRoom(RoomLoader.Room.colorSymbols);
    }
}
