using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RevealMap : NetworkBehaviour
{

    [ClientRpc]
    public void RpcRevealMap()
    {
        gameObject.GetComponent<Renderer>().enabled = true;
    }


    [ClientRpc]
    public void RpcWallRemover()

    {
        Debug.Log("Wall is now a gonner");
        gameObject.SetActive(false);
    }



}
