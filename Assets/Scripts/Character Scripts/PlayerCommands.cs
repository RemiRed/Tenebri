using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerCommands : NetworkBehaviour
{
    [SerializeField]
    RoomLoader roomLoader;
    [SerializeField]
    RevealMap map1, map2, wall;

    private void Start()
    {
        //roomLoader = GameObject.FindGameObjectWithTag("RoomLoader").GetComponent<RoomLoader>();
    }

    [Command]
    public void CmdCorridorLever()
    {
        if (roomLoader.clearedRoom)
        {
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
    public void CmdUppdatePosition()
    {


    }

    public void CmdMazeLever0()
    {

        map1.RpcRevealMap();

    }
    [Command]
    public void CmdMazeLever1()
    {
        map2.RpcRevealMap();
    }
    [Command]

    public void CmdMazeLever2()
    {

        wall.RpcWallRemover();

    }

    [Command]
    public void CmdLoad() //Loads the next room, or last room if the last room is the next room
    {
        if (roomLoader.nextRoomNumber <= roomLoader.numberOfRooms)
        {
            roomLoader.LoadNextRoom();
            roomLoader.OpenCorridorDoors();
        }
    }
}
