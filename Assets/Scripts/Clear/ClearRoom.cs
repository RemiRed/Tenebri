using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class ClearRoom : MonoBehaviour
{
    //By Andreas Halldin
    //Handles what happens when the player(s) enter the ClearRoom

    [SerializeField]
    GameObject clear, oldCanvas, clearCanvas; //The text on the clear screen, the old canvas, and the game clear canvas

    [SerializeField]
    Image white; //The White background

    [SerializeField]
    VideoPlayer video; //The video to play when players clear the game

    [SerializeField]
    float delay; //The delay before video has ended

    private void OnTriggerEnter(Collider other) //If a player has entered the room, start playing the video
    {
        if (other.tag == "Player")
        {
            video.targetCamera = other.gameObject.GetComponentInChildren<Camera>();
            Clear();
        }
    }
    public void Clear() //Disable old canvas and start the video
    {
        oldCanvas.SetActive(false);
        StartCoroutine(WaitForVideo());
    }

    IEnumerator WaitForVideo() //Play the video and show the clear canvas
    {
        clearCanvas.SetActive(true);
        video.gameObject.SetActive(true);
        video.Play(); //Start of video
        print("Started video");
        while (white.color.a > 0) //Fade the video in
        {
            Color temp = white.color;
            temp.a -= 1 / 255f;
            white.color = temp;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSecondsRealtime(((float)video.clip.length * video.playbackSpeed) + delay); //Wait until the video has ended
        print("Ended video");
        while (white.color.a < 1) //Fade the video out
        {
            Color temp = white.color;
            temp.a += 1 / 255f;
            white.color = temp;
            yield return new WaitForEndOfFrame();
        }
        clear.SetActive(true); //Set the game clear UI to active
        Cursor.lockState = CursorLockMode.None; //Unlock and show cursor
        Cursor.visible = true;
    }

}
