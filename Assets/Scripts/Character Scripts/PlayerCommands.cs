﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerCommands : NetworkBehaviour
{
    [SerializeField]
    RoomLoader roomLoader;
    RoomLoader.Room currentRoom = RoomLoader.Room.colorSymbols;

    [SerializeField]
    PasswordRandomizer colorSymbol;
    //[SerializeField]
    public GameObject roomManager, roundRoom;
    public GameObject map1, map2, wall;

    RoundRoomCenter center;

    [SyncVar]
    public bool localPlayer;



    private void Start()
    {
        //roomLoader = GameObject.FindGameObjectWithTag("RoomLoader").GetComponent<RoomLoader>();
        // CmdLocalPlayer(isLocalPlayer);
    }

    [Command]
    public void CmdLocalPlayer()
    {
        localPlayer = isLocalPlayer;
    }

    private void Update()
    {
        if (roomLoader == null)
        {
            roomLoader = GameObject.FindGameObjectWithTag("RoomLoader").GetComponent<RoomLoader>();
        }
    }

    [Command]
    public void CmdCorridorLever()
    {
        if (roomLoader.clearedRoom)
        {
            if (currentRoom == RoomLoader.Room.colorSymbols)
            {
                GameObject.FindGameObjectWithTag("ColorSymbol").GetComponent<PasswordRandomizer>().StartPuzzle();
            }
            else if (currentRoom == RoomLoader.Room.roundMaze)
            {
                roomManager = GameObject.FindGameObjectWithTag("roomManager");
                roundRoom = GameObject.FindGameObjectWithTag("roundRoom");
                Debug.Log(roundRoom.name);
            }
            roomLoader.RpcOpenDoorTo(currentRoom);
            switch (currentRoom)
            {
                case RoomLoader.Room.colorSymbols:
                    currentRoom = RoomLoader.Room.roundMaze;
                    break;
                case RoomLoader.Room.roundMaze:
                    currentRoom = RoomLoader.Room.outdoorMaze;
                    break;
                default:
                    break;
            }
        }
        roomLoader.clearedRoom = true;
    }

//    [Command]
//    public void CmdStartRoundMaze()
//    {
//        center = GameObject.FindGameObjectWithTag("RoundRoomCenter").GetComponent<RoundRoomCenter>();
////        if (center.playerInCenter)
////        {
////            print("JAMEN JA");
////        }
//    }

    [Command]
    public void CmdReRandomRoundMazePuzzle()
    {
        //Debug.Log ("Looking for round room:" + roundRoom.name);
        //GameObject.FindGameObjectWithTag("roundRoom").GetComponent<RoundRoomWalls>().reRandomNow = true;
        GameObject.FindGameObjectWithTag("roundRoom").GetComponent<RoundRoomWalls>().RpcRandomizeEverything();
    }

    [Command]
    public void CmdRoundMazeCompleteSuccess()
    {

        GameObject.FindGameObjectWithTag("roundRoom").GetComponent<RoundRoomWalls>().RpcCompleteSuccessOnRoundMaze();
    }

    [Command]
    public void CmdRoundMazeFailure()
    {

        GameObject.FindGameObjectWithTag("roundRoom").GetComponent<RoundRoomWalls>().RpcRoundMazeFailure();
    }



    [Command]
    public void CmdPlayerInCenter()
    {
		GameObject.FindGameObjectWithTag ("RoundRoomCenter").GetComponent<RoundRoomCenter> ().PlayerInCenter ();
       // center.playerInCenter = playerInCenter;
    }

    [Command]
    public void CmdStartRoomLanded(int id)
    {
        if (id == 1)
        {
            GameObject.FindGameObjectWithTag("SpawnPoint1").GetComponent<UnloadRooms>().entered = true;
        }
        else
        {
            GameObject.FindGameObjectWithTag("SpawnPoint2").GetComponent<UnloadRooms>().entered = true;
        }
    }

    [Command]
    public void CmdUnloadBeginning()
    {
        RpcUnloadBeginning();
    }

    [ClientRpc]
    void RpcUnloadBeginning()
    {
        RoomLoader roomLoader = GameObject.FindGameObjectWithTag("RoomLoader").GetComponent<RoomLoader>();
        roomLoader.UnloadAllRoomsExcept(RoomLoader.Room.startRoom);
        roomLoader.UnloadAlllCorridorsExcept(0);
    }

    [Command]
    public void CmdCorridorLeverRelease()
    {
        roomLoader.clearedRoom = false;
    }

    [Command]
    public void CmdMazeLever0()
    {
	//	map1.GetComponent<RevealMap>().MapRevealer(map1.gameObject);
		RpcRevealMap1(map1.gameObject);
    }
	[ClientRpc]
	void RpcRevealMap1(GameObject _map1)
	{
		_map1.GetComponent<Renderer>().enabled = true;
	}
    [Command]
    public void CmdMazeLever1()
    {
		//map2.GetComponent<RevealMap>().MapRevealer(map2.gameObject);
		RpcRevealMap2(map2.gameObject);
    }
	[ClientRpc]
	void RpcRevealMap2(GameObject _map2)
	{
		_map2.GetComponent<Renderer> ().enabled = true;
	}
    [Command]
    public void CmdMazeLever2()
    {
       // wall.GetComponent<RevealMap>().WallRemover();
		RpcRemoveWall(wall.gameObject);
    }
	[ClientRpc]
	void RpcRemoveWall(GameObject _wall)
	{
		_wall.SetActive (false);
	}
    [Command]
    public void CmdMazeLever3()
    {
        RpcMazeLever3();
    }
    [ClientRpc]
    void RpcMazeLever3()
    {
        GameObject.FindGameObjectWithTag("OutdoorMaze").GetComponent<RoomVariables>().OpenDoorToNextLevel();
    }
}
