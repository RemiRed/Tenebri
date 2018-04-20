using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ClearSceneManager : MonoBehaviour {
    [SerializeField]
    GameObject canvas;
    [SerializeField]
    VideoPlayer video;

    [SerializeField]
    float delay;
    bool videoCheck = false;

    void Start()
    {
        StartCoroutine(WaitForVideo());
    }

    IEnumerator WaitForVideo()
    {
        print("Started video");
        yield return new WaitForSecondsRealtime((float)video.clip.length + delay);
        print("Ended video");
        canvas.SetActive(true);
    }

}
