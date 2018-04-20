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

    private void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            playerCmd = c.gameObject.GetComponent<PlayerCommands>();
            playerCmd.CmdStartRoomLanded(id);
            if (entered && otherUnloadRooms.entered)
            {
                playerCmd.CmdUnloadBeginning();
            }
        }
    }
}
