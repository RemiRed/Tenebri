﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RoomLoader : NetworkBehaviour
{
    public enum Room { startRoom, colorSymbols, roundMaze, outdoorMaze };
    [SyncVar]
    public bool clearedRoom;

    public GameObject startRoomP1, startRoomP2, colorSymbolsP1, colorSymbolsP2, roundMazeP1, roundMazeP2, outdoorMazeP1, outdoorMazeP2;

    [SerializeField]
    List<GameObject> corridorsP1 = new List<GameObject>(), corridorsP2 = new List<GameObject>();

    Room currentRoom = Room.startRoom;

    public void LoadNextRoom(Room room) //Loads the next room, unloads the previous.
    {
        if (room == Room.startRoom)
        {
            startRoomP1.SetActive(true);
            startRoomP2.SetActive(true);
        }

        if (room == Room.colorSymbols)
        {
            colorSymbolsP1.SetActive(true);
            colorSymbolsP2.SetActive(true);
        }

        if (room == Room.roundMaze)
        {
            roundMazeP1.SetActive(true);
            roundMazeP2.SetActive(true);
        }

        if (room == Room.outdoorMaze)
        {
            outdoorMazeP1.SetActive(true);
            outdoorMazeP2.SetActive(true);
        }
    }

    public void UnloadAllRoomsExcept(Room room)
    {
        if (room != Room.startRoom)
        {
            startRoomP1.SetActive(false);
            startRoomP2.SetActive(false);
        }

        if (room != Room.colorSymbols)
        {
            colorSymbolsP1.SetActive(false);
            colorSymbolsP2.SetActive(false);
        }

        if (room != Room.roundMaze)
        {
            roundMazeP1.SetActive(false);
            roundMazeP2.SetActive(false);
        }

        if (room != Room.outdoorMaze)
        {
            outdoorMazeP1.SetActive(false);
            outdoorMazeP2.SetActive(false);
        }
    }

    public void LoadNextCorridor(int corridorID)
    {
        for (int i = 0; i < corridorsP1.Count; i++)
        {
            if (i == corridorID)
            {
                corridorsP1[i].SetActive(true);
                corridorsP2[i].SetActive(true);
            }
        }
    }

    public void UnloadAlllCorridorsExcept(int corridorID)
    {
        for (int i = 0; i < corridorsP1.Count; i++)
        {
            if (i != corridorID)
            {
                corridorsP1[i].SetActive(false);
                corridorsP2[i].SetActive(false);
            }
        }
    }

}