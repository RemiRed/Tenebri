﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class ClearRoom : MonoBehaviour
{
    [SerializeField]
    GameObject clear, oldCanvas, clearCanvas;

    [SerializeField]
    Image white;

    [SerializeField]
    VideoPlayer video;

    [SerializeField]
    float delay;
    bool videoCheck = false;

    private void OnTriggerEnter(Collider other)
    {
        print(other.tag);
        if (other.tag == "Player")
        {
            video.targetCamera = other.gameObject.GetComponentInChildren<Camera>();
            Clear();
        }
    }
    public void Clear()
    {
        Cursor.lockState = CursorLockMode.None;
        oldCanvas.SetActive(false);
        StartCoroutine(WaitForVideo());
    }

    IEnumerator WaitForVideo()
    {
        clearCanvas.SetActive(true);
        video.gameObject.SetActive(true);
        video.Play();
        print("Started video");
        while (white.color.a > 0)
        {
            Color temp = white.color;
            temp.a -= 1 / 255f;
            white.color = temp;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSecondsRealtime(((float)video.clip.length * video.playbackSpeed) + delay);
        print("Ended video");
        while (white.color.a < 1)
        {
            Color temp = white.color;
            temp.a += 1 / 255f;
            white.color = temp;
            yield return new WaitForEndOfFrame();
        }
        clear.SetActive(true);
    }

}