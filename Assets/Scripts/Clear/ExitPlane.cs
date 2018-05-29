using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPlane : MonoBehaviour
{
    [SerializeField]
    GameObject clearRoomSpawn;

    private void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            c.transform.position = clearRoomSpawn.transform.position;
        }
    }    
}
