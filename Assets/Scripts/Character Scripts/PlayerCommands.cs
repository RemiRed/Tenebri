using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerCommands : NetworkBehaviour
{
    //By Andreas Halldin
    //Handles all commands sent from a player
    [SerializeField]
    RoomLoader roomLoader; //The Room loader, a script that handles loading and unloading rooms
    Room currentRoom = Room.colorSymbols; //The current room of the player

    //Various variables for different commands
    [SerializeField]
    PasswordRandomizer colorSymbol;
    public GameObject roomManager, roundRoom;
    RevealMap revealMap;
    RoundRoomCenter center;
    public bool moved = false;

    private void Update() //Find the roomLoader, not done in start due to errors when this was tried.
    {
        if (roomLoader == null)
        {
            roomLoader = GameObject.FindGameObjectWithTag("RoomLoader").GetComponent<RoomLoader>();
        }
    }

    [Command]
    public void CmdStartRoomLanded(int id, bool entered) //If a player has landed in their start room, set the correct UnloadRooms.entered to true
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
    public void CmdUnloadBeginning() //Used to do UnloadBeginning on all clients
    {
        RpcUnloadBeginning();
    }

    [ClientRpc]
    void RpcUnloadBeginning() //Unloads all rooms, except the start room and the first corridor. This is to reduce any lag other rooms might cause
    {
        RoomLoader roomLoader = GameObject.FindGameObjectWithTag("RoomLoader").GetComponent<RoomLoader>();
        roomLoader.UnloadAllRoomsExcept(Room.startRoom, 1);
        roomLoader.UnloadAllRoomsExcept(Room.startRoom, 2);
        roomLoader.UnloadAllCorridorsExcept(0, 1);
        roomLoader.UnloadAllCorridorsExcept(0, 2);
    }

    [Command]
    public void CmdCorridorLever() //Open the door to, and start, the next puzzle
    {
        if (roomLoader.clearedRoom)
        {
            if (currentRoom == Room.colorSymbols)
            {
                GameObject.FindGameObjectWithTag("ColorSymbol").GetComponent<PasswordRandomizer>().StartPuzzle();
            }
            else if (currentRoom == Room.roundMaze)
            {
                roomManager = GameObject.FindGameObjectWithTag("roomManager");
                roundRoom = GameObject.FindGameObjectWithTag("roundRoom");
                Debug.Log(roundRoom.name);
            }
            roomLoader.RpcOpenDoorTo(currentRoom);
            switch (currentRoom)
            {
                case Room.colorSymbols:
                    currentRoom = Room.roundMaze;
                    break;
                case Room.roundMaze:
                    currentRoom = Room.outdoorMaze;
                    break;
                default:
                    break;
            }
        }
        roomLoader.clearedRoom = true;
    }
    [Command]
    public void CmdCorridorLeverRelease() //Release the lever to the next room
    {
        roomLoader.clearedRoom = false;
    }

    [Command]
    public void CmdColorSymbolCompleteSuccess() //ColorSymbol puzzle completed
    {
        GameObject.FindGameObjectWithTag("ColorSymbol").GetComponent<ColorSymbolSuccess>().RpcCompleteSuccess();
    }


    [Command]
    public void CmdColorSymbolFailure()//ColorCymbol Puzzle failed
    {
        GameObject.FindGameObjectWithTag("ColorSymbol").GetComponent<ColorSymbolSuccess>().RpcFailure();
    }

    [Command]
    public void CmdPlayerInCenter(bool playerInCenter) //Sets the playerInCenter bool for Round Maze Puzzle
    {
        center = GameObject.FindGameObjectWithTag("RoundRoomCenter").GetComponent<RoundRoomCenter>();
        center.playerInCenter = playerInCenter;
    }

    [Command]
    public void CmdReRandomRoundMazePuzzle() //Rerandomize RoundMaze Puzzle
    {
        GameObject.FindGameObjectWithTag("roundRoom").GetComponent<RoundRoomWalls>().RpcRandomizeEverything();
    }

    [Command]
    public void CmdRoundMazeCompleteSuccess() //Round Maze cleared
    {
        GameObject.FindGameObjectWithTag("roundRoom").GetComponent<RoundRoomWalls>().RpcCompleteSuccess();
    }

    [Command]
    public void CmdRoundMazeFailure() //Round Maze Failed
    {
        GameObject.FindGameObjectWithTag("roundRoom").GetComponent<RoundRoomWalls>().RpcFailure();
    }

    [Command]
    public void CmdOutdoorMazeFailure() //Outdoor Maze Failed
    {
        GameObject.FindGameObjectWithTag("OutdoorMaze").GetComponent<OutdoorMazeSuccess>().RpcFailure();
    }

    [Command]
    public void CmdMazeLever(int i) //Outdoor Maze Lever has been pulled. Call the correct Rpc in revealMap based on i
    {
        revealMap = GameObject.FindGameObjectWithTag("RevealMap").GetComponent<RevealMap>();
        switch (i)
        {
            case 1: //Reveal Map
                revealMap.RpcRevealMap();
                break;
            case 2: //Open Fake Wall
                revealMap.RpcWallRemover();
                break;
            case 3: //Open Door to credits
                revealMap.RpcOpenDoor();
                break;
            default:
                break;
        }
    }

    [Command]
    public void CmdGameOver() //Call on RpcGameOver on all clients from the server
    {
        RpcGameOver();
    }

    [ClientRpc]
    public void RpcGameOver() //Call on GameOver on all players
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            player.GetComponent<CharacterScript>().GameOver();
        }
    }
}

