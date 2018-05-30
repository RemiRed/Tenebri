/*
 *Made by: Andres 
 * 
 */
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RevealMap : NetworkBehaviour
{
    [SerializeField]
    GameObject map;
    //ClientRpc commandon är kod som kallas på Unity multiplayer servern och anropas på respektive gameobject.
    [ClientRpc]
    public void RpcRevealMap()
    {
        map.SetActive(true);
    }
		
    [ClientRpc]
    public void RpcWallRemover()
    {
        gameObject.transform.position += 50 * Vector3.down;
    }

    [ClientRpc]
    public void RpcOpenDoor()
    {
        GameObject.FindGameObjectWithTag("OutdoorMaze").GetComponent<RoomVariables>().OpenDoorToNextLevel();
        GameObject.FindGameObjectWithTag("OutdoorMaze2").GetComponent<RoomVariables>().OpenDoorToNextLevel();
    }
}