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
    
//	NetworkingLobby lobby;
//
//	void Start(){
//
//		lobby = GameObject.FindGameObjectWithTag ("NetworkManager").GetComponent<NetworkingLobby> ();
//	}

    void OnTriggerEnter(Collider player)
    {        
		if (player.tag == "Player" && activeRandom == true)
        {
            //player.GetComponent<PlayerCommands>().CmdActivateRoundMazePuzzle ();
            //         activeRandom = false;
            player.gameObject.GetComponent<PlayerCommands>().CmdPlayerInCenter(true);
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
        print("Hook en är hä");
    }

    //	[Command]
    //	void CmdActivateRoundMazePuzzle(){
    //
    //		roomManager.GetComponent<RoundRoomManager>().GetWallSymbols();
    //		roundRoom.GetComponent<RoundRoomWalls>().RandomSymbols();
    //		roomManager.GetComponent<RoundMazeMapRoom>().RpcMapButtons();
    //
    //	}
}
