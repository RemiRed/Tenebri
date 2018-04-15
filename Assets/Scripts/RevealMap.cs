using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RevealMap : NetworkBehaviour
{

    [ClientRpc]
    public void RpcRevealMap()
    {
        //Reveal the map
        print("Lever on clients");

    }


    [ClientRpc]
    public void RpcWallRemover()

    {

        // Flytta väggen.
    }



}
