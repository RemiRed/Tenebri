using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{

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
            Close();
        }
    }

    void Close()
    {
        room.entryDoor.GetComponent<Animator>().SetTrigger("isClosing");
        roomLoader.UnloadAllRoomsExcept(room.room);
    }
}
