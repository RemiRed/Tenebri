using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MapManager : NetworkBehaviour
{
    public GameObject map1, map2, fakewall;

    GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerCommands>().map1 = map1;
        player.GetComponent<PlayerCommands>().map2 = map2;
        player.GetComponent<PlayerCommands>().wall = fakewall;

    }
}