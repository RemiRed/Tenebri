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

    //ClientRpc commandon är kod som kallas på Unity multiplayer servern och anropas på respektive gameobject.
    [ClientRpc] //Stänger av mesh renderer.
    public void RpcRevealMap()
    {
        gameObject.GetComponent<Renderer>().enabled = true;
    }
		
    [ClientRpc]
    public void RpcWallRemover()
    { // Avaktiverar en vägg.
        gameObject.SetActive(false);
    }
}