using System.Collections;
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
	[SerializeField]
	GameObject roomManager, roundRoom;
    public GameObject map1, map2, wall;

    RoundRoomCenter center;

    private void Start()
    {
        roomLoader = GameObject.FindGameObjectWithTag("RoomLoader").GetComponent<RoomLoader>();
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

	[Command]
	public void CmdActivateRoundMazePuzzle(){

		Debug.Log ("commanding from player");

        roomManager = GameObject.FindGameObjectWithTag("roomManager");
        roundRoom = GameObject.FindGameObjectWithTag("roundRoom");
        roomManager.GetComponent<RoundRoomManager>().GetWallSymbols();
		roundRoom.GetComponent<RoundRoomWalls>().RandomSymbols();
		roomManager.GetComponent<RoundMazeMapRoom>().RpcMapButtons();
	}

    [Command]
    public void CmdStartRoundMaze()
    {
        center = GameObject.FindGameObjectWithTag("RoundRoomCenter").GetComponent<RoundRoomCenter>();
        if (center.playerInCenter)
        {
            print("JAMEN JA");
        }
    }


    [Command]
    public void CmdCorridorLeverRelease()
    {
        roomLoader.clearedRoom = false;
    }

    [Command]
    public void CmdMazeLever0()
    {

        map1.GetComponent<RevealMap>().RpcRevealMap();

    }
    [Command]
    public void CmdMazeLever1()
    {
        map2.GetComponent<RevealMap>().RpcRevealMap();
    }
    [Command]
    public void CmdMazeLever2()
    {

        wall.GetComponent<RevealMap>().RpcWallRemover();

    }

}
