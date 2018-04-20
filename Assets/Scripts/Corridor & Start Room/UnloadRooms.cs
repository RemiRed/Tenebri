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

    PlayerCommands playerCmd;

    bool unloaded = false;

    private void OnTriggerStay(Collider c)
    {
        if (unloaded)
        {
            return;
        }
        if (c.tag == "Player")
        {
            playerCmd = c.gameObject.GetComponent<PlayerCommands>();
            playerCmd.CmdStartRoomLanded(id);
            if (entered && otherUnloadRooms.entered)
            {
                unloaded = true;
                playerCmd.CmdUnloadBeginning();
            }
        }
    }
}
