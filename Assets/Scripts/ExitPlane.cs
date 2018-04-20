using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ExitPlane : MonoBehaviour
{
    [SerializeField]
    VideoPlayer video;
    bool videoCheck = false;

    private void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            video.targetCamera = c.gameObject.GetComponentInChildren<Camera>();
            video.Play();
            videoCheck = true;
        }
    }

    private void Update()
    {
        if (!video.isPlaying && videoCheck)
        {
            //Byt till Victory Scene
            print("Victory scene here");
        }
    }
}
