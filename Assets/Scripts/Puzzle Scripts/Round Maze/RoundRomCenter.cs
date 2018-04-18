using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RoundRomCenter : NetworkBehaviour {

    public bool activeRandom = true;
    [SerializeField]
    GameObject roundRoom;
    [SerializeField]
    GameObject roomManager;
//	NetworkingLobby lobby;
//
//	void Start(){
//
//		lobby = GameObject.FindGameObjectWithTag ("NetworkManager").GetComponent<NetworkingLobby> ();
//	}

    void OnTriggerEnter(Collider playa)
    {        
		if (playa.tag == "Player" && activeRandom == true)
        {
			CmdActivateRoundMazePuzzle ();
            activeRandom = false;
        }
    }

	[Command]
	void CmdActivateRoundMazePuzzle(){

		roomManager.GetComponent<RoundRoomManager>().GetWallSymbols();
		roundRoom.GetComponent<RoundRoomWalls>().RandomSymbols();
		roomManager.GetComponent<RoundMazeMapRoom>().RpcMapButtons();

	}
}
