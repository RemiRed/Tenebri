﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject map1, map2, fakewall;

    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerCommands>().map1 = map1;
        player.GetComponent<PlayerCommands>().map2 = map2;
        player.GetComponent<PlayerCommands>().wall = fakewall;

    }
}