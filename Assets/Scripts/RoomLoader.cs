using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum Room { startRoom, colorSymbols, roundMaze, outdoorMaze }; //Enum to easly and clearly define rooms
public class RoomLoader : NetworkBehaviour
{
    //By Andreas Halldin
    //Loads and Unloads rooms

    [SyncVar]
    public bool clearedRoom; //Check if a room has been cleared or not

    public GameObject startRoomP1, startRoomP2, colorSymbolsP1, colorSymbolsP2, roundMazeP1, roundMazeP2, outdoorMazeP1, outdoorMazeP2; // The Different rooms to be loaded/unloaded

    [SerializeField]
    List<GameObject> corridorsP1 = new List<GameObject>(), corridorsP2 = new List<GameObject>(); //Lists of all corridors by player
    

    public void LoadRoom(Room room, int player) //Loads a room for a player
    {
        if (player == 1)
        {
            if (room == Room.startRoom)
            {
                startRoomP1.SetActive(true);
            }
            if (room == Room.colorSymbols)
            {
                colorSymbolsP1.SetActive(true);
            }
            if (room == Room.roundMaze)
            {
                roundMazeP1.SetActive(true);
            }
            if (room == Room.outdoorMaze)
            {
                outdoorMazeP1.SetActive(true);
            }
        }
        else
        {
            if (room == Room.startRoom)
            {
                startRoomP2.SetActive(true);
            }

            if (room == Room.colorSymbols)
            {
                colorSymbolsP2.SetActive(true);
            }

            if (room == Room.roundMaze)
            {
                roundMazeP2.SetActive(true);
            }

            if (room == Room.outdoorMaze)
            {
                outdoorMazeP2.SetActive(true);
            }
        }
    }

    public void UnloadAllRoomsExcept(Room room, int player) //Unloads all rooms except one room, for one player
    {
        if (player == 1)
        {
            if (room != Room.startRoom)
            {
                startRoomP1.SetActive(false);
            }

            if (room != Room.colorSymbols)
            {
                colorSymbolsP1.SetActive(false);
            }

            if (room != Room.roundMaze)
            {
                roundMazeP1.SetActive(false);
            }

            if (room != Room.outdoorMaze)
            {
                outdoorMazeP1.SetActive(false);
            }
        }
        else
        {
            if (room != Room.startRoom)
            {
                startRoomP2.SetActive(false);
            }

            if (room != Room.colorSymbols)
            {
                colorSymbolsP2.SetActive(false);
            }

            if (room != Room.roundMaze)
            {
                roundMazeP2.SetActive(false);
            }

            if (room != Room.outdoorMaze)
            {
                outdoorMazeP2.SetActive(false);
            }
        }
    }

    public void LoadCorridor(int corridorID, int player) //Loads a certain Corrior for one player
    {
        if (player == 1)
        {
            for (int i = 0; i < corridorsP1.Count; i++)
            {
                if (i == corridorID)
                {
                    corridorsP1[i].SetActive(true);
                }
            }
        }
        else
        {
            for (int i = 0; i < corridorsP2.Count; i++)
            {
                if (i == corridorID)
                {
                    corridorsP2[i].SetActive(true);
                }
            }
        }
    }

    public void UnloadAllCorridorsExcept(int corridorID, int player) //Unloads all corridors except one for one player
    {

        if (player == 1)
        {
            for (int i = 0; i < corridorsP1.Count; i++)
            {
                if (i != corridorID)
                {
                    corridorsP1[i].SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < corridorsP2.Count; i++)
            {
                if (i != corridorID)
                {
                    corridorsP2[i].SetActive(false);
                }
            }
        }
    }

    [ClientRpc]
    public void RpcOpenDoorTo(Room room) //Opens a door to a specific room for both players
    {
        switch (room)
        {
            case Room.colorSymbols:
                colorSymbolsP1.GetComponent<RoomVariables>().entryDoor.GetComponent<Animator>().SetBool("open", true);
                colorSymbolsP2.GetComponent<RoomVariables>().entryDoor.GetComponent<Animator>().SetBool("open", true);
                LoadCorridor(1, 1);
                LoadCorridor(1, 2);
                break;
            case Room.roundMaze:
                roundMazeP1.GetComponent<RoomVariables>().entryDoor.GetComponent<Animator>().SetBool("open", true);
                roundMazeP2.GetComponent<RoomVariables>().entryDoor.GetComponent<Animator>().SetBool("open", true);
                LoadCorridor(2, 1);
                LoadCorridor(2, 2);
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