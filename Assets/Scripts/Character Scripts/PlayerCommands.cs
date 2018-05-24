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
    public GameObject roomManager, roundRoom;
    public GameObject map1, map2, wall;


    RoundRoomCenter center;

    public bool moved = false;

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
    [Command]
    public void CmdCorridorLeverRelease()
    {
        roomLoader.clearedRoom = false;
    }

    [Command]
    public void CmdColorSymbolCompleteSuccess()
    {
        GameObject.FindGameObjectWithTag("ColorSymbol").GetComponent<ColorSymbolSuccess>().RpcCompleteSuccess();
    }


    [Command]
    public void CmdColorSymbolFailure()
    {
        GameObject.FindGameObjectWithTag("ColorSymbol").GetComponent<ColorSymbolSuccess>().RpcFailure();
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
    public void CmdPlayerInCenter(bool playerInCenter)
    {
        center = GameObject.FindGameObjectWithTag("RoundRoomCenter").GetComponent<RoundRoomCenter>();
        center.playerInCenter = playerInCenter;
    }

    [Command]
    public void CmdStartRoomLanded(int id, bool entered)
    {
        if (id == 1)
        {
            GameObject.FindGameObjectWithTag("SpawnPoint1").GetComponent<UnloadRooms>().entered = entered;
        }
        else
        {
            GameObject.FindGameObjectWithTag("SpawnPoint2").GetComponent<UnloadRooms>().entered = entered;
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
        roomLoader.UnloadAllRoomsExcept(RoomLoader.Room.startRoom, 1);
        roomLoader.UnloadAllRoomsExcept(RoomLoader.Room.startRoom, 2);
        roomLoader.UnloadAllCorridorsExcept(0, 1);
        roomLoader.UnloadAllCorridorsExcept(0, 2);
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
