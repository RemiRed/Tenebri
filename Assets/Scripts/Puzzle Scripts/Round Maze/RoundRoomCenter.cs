using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundRoomCenter : MonoBehaviour {

    public bool activeRandom = true;
    [SerializeField]
    GameObject roundRoom;
    [SerializeField]
    GameObject roomManager;

//    [SyncVar(hook = "PlayerInCenter")]
//    public bool playerInCenter = false;

    void OnTriggerEnter(Collider player)
    {        
		if (player.tag == "Player" && activeRandom == true)
        {
            player.gameObject.GetComponent<PlayerCommands>().CmdPlayerInCenter();

			GetComponentInParent<Password> ().success.playercommand = player.GetComponent<PlayerCommands>();
        }
    }

//    private void OnTriggerExit(Collider player)
//    {
//        if (player.tag == "Player" && activeRandom == true)
//        {
//            player.gameObject.GetComponent<PlayerCommands>().CmdPlayerInCenter(false);
//        }
//    }

    public void PlayerInCenter()
    {
		if (activeRandom) {
			activeRandom = false;

			roomManager.GetComponent<RoundRoomManager>().GetWallSymbols();
			roundRoom.GetComponent<RoundRoomWalls>().RandomSymbols();
			roomManager.GetComponent<RoundMazeMapRoom>().RpcMapButtons();
		}
    }
}