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

    [SerializeField]
    int id;

    private void OnTriggerEnter(Collider c)
    {

        if (c.tag == "Player")
        {
            c.gameObject.GetComponent<PlayerCommands>().CmdStartRoomLanded(id);
            if (entered && otherUnloadRooms.entered)
            {
                Unload();
            }
        }
    }

    void Unload()
    {
        roomLoader.UnloadAllRoomsExcept(RoomLoader.Room.startRoom);
        roomLoader.UnloadAlllCorridorsExcept(0);
    }
}
