using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MapManager : NetworkBehaviour
{
    public GameObject map1, fakewall;

    [SerializeField]
    GameObject player;

    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerCommands>().map1 = map1;
        player.GetComponent<PlayerCommands>().wall = fakewall;

    }
}