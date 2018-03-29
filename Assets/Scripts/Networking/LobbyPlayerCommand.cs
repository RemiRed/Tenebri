using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyPlayerCommand : NetworkBehaviour {

    [Command]
    public void CmdDisconnect()
    {
        print("CmdDisconnect");
        Destroy(gameObject);
    }
}
