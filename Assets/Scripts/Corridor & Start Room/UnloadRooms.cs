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

    bool unloaded = false, check = false;

    private void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player" && !check)
        {
            check = true;
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
