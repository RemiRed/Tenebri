using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UnloadRooms : NetworkBehaviour
{
    //By Andreas Halldin
    //Handles Unloading rooms when the players are both in their rooms

    [SerializeField]
    RoomLoader roomLoader; //The Room Loader

    [SerializeField]
    UnloadRooms otherUnloadRooms; //The other Unload rooms, attached to the other spawn point

    [SyncVar]
    public bool entered; //a bool to see if a player has entered this objects trigger

    [SerializeField]
    int id; //Id of the spawn, used to determine what player this room is


    [SerializeField]
    FadeFromBlack fadeFromBlack; //The fade from black script, used to fade the instructions at the start of the game

    PlayerCommands playerCmd; //the Player Commands of the player landed here

    bool unloaded = false; //bool to check if everything has been unloaded or not

    private void OnTriggerEnter(Collider c) //Transports the server to the other spawn point, both players started in the same room prior
    {
        if (c.tag == "Player")
        {
            playerCmd = c.gameObject.GetComponent<PlayerCommands>();
            if (isServer && !playerCmd.moved)
            {
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                {
                    if (player.GetComponentInChildren<Camera>().enabled)
                    {
                        player.GetComponent<PlayerCommands>().moved = true;
                        player.transform.position = otherUnloadRooms.transform.position;
                        break;
                    }
                }
            }
            playerCmd.CmdStartRoomLanded(id, true);
            StartCoroutine(fadeFromBlack.Fade()); //Start the fade
        }
    }

    private void OnTriggerStay(Collider c) //While both players are in their rooms, unload the other rooms
    {
        if (unloaded)
        {
            return;
        }
        if (c.tag == "Player")
        {
            if (entered && otherUnloadRooms.entered)
            {
                unloaded = true;
                playerCmd.CmdUnloadBeginning();
            }
        }
    }

    private void OnTriggerExit(Collider c) //Prevents one player from triggering both rooms as entered
    {
        if (unloaded)
        {
            return;
        }
        if (c.tag == "Player")
        {
            playerCmd = c.gameObject.GetComponent<PlayerCommands>();
            playerCmd.CmdStartRoomLanded(id, false);
        }
    }
}
