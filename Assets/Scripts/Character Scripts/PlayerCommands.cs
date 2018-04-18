using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerCommands : NetworkBehaviour
{
    [SerializeField]
    RoomLoader roomLoader;
    RoomLoader.Room currentRoom = RoomLoader.Room.colorSymbols;

    
    public GameObject map1, map2, wall;

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
                roomLoader.colorSymbolsP1.GetComponent<PasswordRandomizer>().CmdStartPuzzle();
            }
            roomLoader.RpcOpenDoorTo(currentRoom);
        }
        roomLoader.clearedRoom = true;
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

    [Command]
    public void CmdLoad() //Loads the next room, or last room if the last room is the next room
    {
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
        print(currentRoom);
        print("HEJ");
        roomLoader.LoadRoom(currentRoom);
    }

}
