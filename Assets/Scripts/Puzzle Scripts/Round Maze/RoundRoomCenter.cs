using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RoundRoomCenter : NetworkBehaviour {

    public bool activeRandom = true;
    [SerializeField]
    GameObject roundRoom;
    [SerializeField]
    GameObject roomManager;

    [SyncVar(hook = "Test")]
    public bool playerInCenter = false;

    void OnTriggerEnter(Collider player)
    {        
		if (player.tag == "Player" && activeRandom == true)
        {
            player.gameObject.GetComponent<PlayerCommands>().CmdPlayerInCenter(true);

			GetComponentInParent<Password> ().success.playercommand = player.GetComponent<PlayerCommands>();
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if (player.tag == "Player" && activeRandom == true)
        {
            player.gameObject.GetComponent<PlayerCommands>().CmdPlayerInCenter(false);
        }
    }

    void Test(bool playerInCenter)
    {
		if (isServer && activeRandom) {
			activeRandom = false;

			roomManager.GetComponent<RoundRoomManager>().GetWallSymbols();
			roundRoom.GetComponent<RoundRoomWalls>().RandomSymbols();
			roomManager.GetComponent<RoundMazeMapRoom>().RpcMapButtons();
		}
    }
}