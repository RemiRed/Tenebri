﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRespawn : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponentInParent<OutdoorMazeSuccess>().player = other.gameObject;
        }
    }
}
