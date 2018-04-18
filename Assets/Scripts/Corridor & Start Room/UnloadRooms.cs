using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnloadRooms : MonoBehaviour {

    RoomVariables room;
    RoomLoader roomLoader;

    private void Start()
    {
        roomLoader = GameObject.FindGameObjectWithTag("RoomLoader").GetComponent<RoomLoader>();
        room = transform.parent.GetComponent<RoomVariables>();
    }

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
