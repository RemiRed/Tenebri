using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnloadRooms : MonoBehaviour
{
    [SerializeField]
    RoomLoader roomLoader;


    private void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            Unload();
        }
    }

    void Unload()
    {
        roomLoader.UnloadAllRoomsExcept(RoomLoader.Room.startRoom);
        roomLoader.UnloadAlllCorridorsExcept(0);
    }
}
