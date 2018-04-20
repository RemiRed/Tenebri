using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UnloadRooms : NetworkBehaviour
{
    [SerializeField]
    RoomLoader roomLoader;

    [SerializeField]
    UnloadRooms otherUnloadRooms;

    [SyncVar]
    public bool entered;

    private void OnTriggerEnter(Collider c)
    {

        if (c.tag == "Player")
        {
            CmdEntered();
            if (entered && otherUnloadRooms.entered)
            {
                Unload();
            }
        }
    }

    [Command]
    void CmdEntered()
    {
        entered = true;
    }

    void Unload()
    {
        roomLoader.UnloadAllRoomsExcept(RoomLoader.Room.startRoom);
        roomLoader.UnloadAlllCorridorsExcept(0);
    }
}
