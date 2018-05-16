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


    [SerializeField]
    FadeFromBlack fadeFromBlack;

    PlayerCommands playerCmd;

    bool unloaded = false;

    private void OnTriggerEnter(Collider c)
    {
        if (unloaded)
        {
            return;
        }
        if (c.tag == "Player")
        {
            bool tempCheck = true;
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (player.GetComponent<NetworkTransform>().netId.Value < c.GetComponent<NetworkTransform>().netId.Value)
                {
                    tempCheck = false;
                    break;
                }
            }
            if (tempCheck)
            {
                c.gameObject.transform.position = otherUnloadRooms.gameObject.transform.position;
            }

            playerCmd = c.gameObject.GetComponent<PlayerCommands>();
            playerCmd.CmdStartRoomLanded(id, true);
            StartCoroutine(fadeFromBlack.Fade());
        }
    }

    private void OnTriggerStay(Collider c)
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

    private void OnTriggerExit(Collider c)
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
