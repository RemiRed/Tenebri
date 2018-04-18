using System.Collections;
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
    
    public void LoadRoom(Room room) //Loads the next room
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
       // CmdLoadRoom(room);
    }

    //[Command]
    //public void CmdLoadRoom(Room room) //Loads the next room
    //{
    //    if (room == Room.startRoom)
    //    {
    //        startRoomP1.SetActive(true);
    //        startRoomP2.SetActive(true);
    //    }

    //    if (room == Room.colorSymbols)
    //    {
    //        colorSymbolsP1.SetActive(true);
    //        colorSymbolsP2.SetActive(true);
    //    }

    //    if (room == Room.roundMaze)
    //    {
    //        roundMazeP1.SetActive(true);
    //        roundMazeP2.SetActive(true);
    //    }

    //    if (room == Room.outdoorMaze)
    //    {
    //        outdoorMazeP1.SetActive(true);
    //        outdoorMazeP2.SetActive(true);
    //    }
//    }


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

    public void LoadCorridor(int corridorID)
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

    [ClientRpc]
    public void RpcOpenDoorTo(Room room)
    {
        switch (room)
        {
            case Room.colorSymbols:
                colorSymbolsP1.GetComponent<RoomVariables>().entryDoor.GetComponent<Animator>().SetBool("open", true);
                colorSymbolsP2.GetComponent<RoomVariables>().entryDoor.GetComponent<Animator>().SetBool("open", true);
                LoadCorridor(1);
                break;
            case Room.roundMaze:
                roundMazeP1.GetComponent<RoomVariables>().entryDoor.GetComponent<Animator>().SetBool("open", true);
                roundMazeP2.GetComponent<RoomVariables>().entryDoor.GetComponent<Animator>().SetBool("open", true);
                LoadCorridor(2);
                break;
            case Room.outdoorMaze:
                outdoorMazeP1.GetComponent<RoomVariables>().entryDoor.GetComponent<Animator>().SetBool("open", true);
                outdoorMazeP2.GetComponent<RoomVariables>().entryDoor.GetComponent<Animator>().SetBool("open", true);
                break;
            default:
                break;
        }
    }
}