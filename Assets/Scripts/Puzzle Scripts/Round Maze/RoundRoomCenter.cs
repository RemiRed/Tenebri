using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//Activates The RoundMaze puzzle when player enter the trigger zone in the center of the room  

public class RoundRoomCenter : NetworkBehaviour {

    public bool activatePuzzle = true;	//If this puzzle has been activated
    [SerializeField]
	GameObject roundRoom, roomManager;	

    [SyncVar(hook = "PlayerInCenter")]
    public bool playerInCenter = false;

    void OnTriggerEnter(Collider player)
    {        
		if (player.tag == "Player" && activatePuzzle == true)
        {
			
            player.gameObject.GetComponent<PlayerCommands>().CmdPlayerInCenter(true);
			//Assigns this password's result's playerCommand component to the active player's playerCommand
			GetComponentInParent<Password> ().result1.playercommand = player.GetComponent<PlayerCommands>();
            GetComponentInParent<Password>().result2.playercommand = player.GetComponent<PlayerCommands>();
        }
    }

//    private void OnTriggerExit(Collider player)
//    {
//        if (player.tag == "Player" && activatePuzzle == true)
//        {
//            player.gameObject.GetComponent<PlayerCommands>().CmdPlayerInCenter(false);
//        }
//    }

    void PlayerInCenter(bool playerInCenter)
    {
		if (isServer && activatePuzzle) {
			activatePuzzle = false;

			roomManager.GetComponent<RoundRoomManager>().GetWallSymbols();
			roundRoom.GetComponent<RoundRoomWalls>().RandomSymbols();
			roomManager.GetComponent<RoundMazeMapRoom>().RpcMapButtons();
		}
    }
}