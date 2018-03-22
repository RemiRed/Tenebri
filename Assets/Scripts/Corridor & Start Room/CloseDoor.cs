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
        roomLoader.SetEntryDoors();
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
        roomLoader.UnloadCorridor();
        room.entryDoor.SetActive(true);
        Destroy(gameObject);
    }
}
