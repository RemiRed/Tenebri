using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    //By Andreas Halldin
    //Handles closing doors after the player has walked throgh

    RoomVariables room; //The room where this close door trigger is
    RoomLoader roomLoader; // The roomLoader

    [SerializeField]
    int player; //The side that the room belongs to. (Player 1's side, or Player 2's side)
    [SerializeField]
    AudioClip closeDoor; //Close Door Audio Clip

    bool closed = false;

    private void Start() //Find the Room and Room Loader
    {
        roomLoader = GameObject.FindGameObjectWithTag("RoomLoader").GetComponent<RoomLoader>();
        room = transform.parent.GetComponent<RoomVariables>();
    }

    private void OnTriggerEnter(Collider c) //Trigger the Close method when a player enters the trigger
    {
        if (c.tag == "Player" && !closed)
        {
            Close();
        }
    }

    void Close() //Close the door
    {
        closed = true;
        room.entryDoor.GetComponent<Animator>().SetTrigger("isClosing"); //Start the animation
        roomLoader.UnloadAllRoomsExcept(room.room, player); //Unload old rooms
        room.entryDoor.GetComponent<AudioSource>().clip = closeDoor; //Play the Close door sound
        room.entryDoor.GetComponent<AudioSource>().Play();
    }
}
