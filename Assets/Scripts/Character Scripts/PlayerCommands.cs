﻿using System.Collections;
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

    [ClientRpc]
    public void RpcCorridorLever()
    {
        if (roomLoader.clearedRoom)
        {
            print("IF");
            print(currentRoom);
            switch (currentRoom)
            {
                case RoomLoader.Room.colorSymbols:
                    print("COLORSYMBOLS");
                    roomLoader.colorSymbolsP1.GetComponent<RoomVariables>().entryDoor.GetComponent<Animator>().SetBool("open", true);
                    roomLoader.colorSymbolsP2.GetComponent<RoomVariables>().entryDoor.GetComponent<Animator>().SetBool("open", true);
                    break;
                case RoomLoader.Room.roundMaze:
                    roomLoader.roundMazeP1.GetComponent<RoomVariables>().entryDoor.GetComponent<Animator>().SetBool("open", true);
                    roomLoader.roundMazeP2.GetComponent<RoomVariables>().entryDoor.GetComponent<Animator>().SetBool("open", true);
                    break;
                case RoomLoader.Room.outdoorMaze:
                    roomLoader.outdoorMazeP1.GetComponent<RoomVariables>().entryDoor.GetComponent<Animator>().SetBool("open", true);
                    roomLoader.outdoorMazeP2.GetComponent<RoomVariables>().entryDoor.GetComponent<Animator>().SetBool("open", true);
                    break;
                default:
                    break;
            }
            CmdLoad();
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
        //roomLoader.LoadNextRoom(currentRoom);
    }

}
