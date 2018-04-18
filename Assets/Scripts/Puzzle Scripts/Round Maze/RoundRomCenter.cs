﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundRomCenter : MonoBehaviour {

    public bool activeRandom = true;
    [SerializeField]
    GameObject roundRoom;
    [SerializeField]
    GameObject roomManager;
	NetworkingLobby lobby;

	void Start(){

		lobby = GameObject.FindGameObjectWithTag ("NetworkManager").GetComponent<NetworkingLobby> ();
	}

    void OnTriggerEnter(Collider playa)
    {        
		if (playa.tag == "Player" && activeRandom == true)
        {
            roomManager.GetComponent<RoundRoomManager>().CmdGetWallSymbols();
            roundRoom.GetComponent<RoundRoomWalls>().CmdRandomSymbols();
            roomManager.GetComponent<RoundMazeMapRoom>().MapButtons();
            activeRandom = false;
        }
    }
}
