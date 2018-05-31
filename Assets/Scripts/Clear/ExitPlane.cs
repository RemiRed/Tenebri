using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPlane : MonoBehaviour
{
    //By Andreas Halldin
    //Handles moving the player to the clear room
    [SerializeField]
    GameObject clearRoomSpawn; //Spawn inside of the clear room

    private void OnTriggerEnter(Collider c) //Moves any player to the clear room that walks into the trigger
    {
        if (c.tag == "Player")
        {
            c.transform.position = clearRoomSpawn.transform.position;
        }
    }    
}
