using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    //By Andreas Halldin
    //Controls the movement of the clock hands and sound based on the rooms currentTime variable


    RoomVariables room; //The room the clock is in

    [SerializeField]
    GameObject minuteHand, secondHand; //The two hands of the clock
    [SerializeField]
    AudioClip clockTickStress; //The sound to change to when time left is low
    [SerializeField]
    float stressTick = 60f; //When the time is considered low

    bool setHands = false; //bool to check if the clock hands have been set

    private void Start() //Get the variable for the room
    {
        room = GetComponentInParent<RoomVariables>();
    }

    public void StopClock() //Stop the clock sounds
    {
        GetComponent<AudioSource>().Stop();
    }
    private void Update()
    {
        //TESTING ONLY RE-Enable for testing.
        /*if (Input.GetKeyDown(KeyCode.AltGr)) //Lets testers fail a puzzle by pressing Alt Gr
        {
            room.Fail();
        }*/

        if (!setHands) //Sets the clock hand rotations based on the current time left
        {
            setHands = true;
            minuteHand.transform.localRotation = Quaternion.Euler(new Vector3((360 - ((room.currentTime / 60) / 60) * 360), minuteHand.transform.localRotation.eulerAngles.y, minuteHand.transform.localRotation.eulerAngles.z));
            secondHand.transform.localRotation = Quaternion.Euler(new Vector3((360 - ((room.currentTime % 60) / 60f) * 360), secondHand.transform.localRotation.eulerAngles.y, secondHand.transform.localRotation.eulerAngles.z));
        }

        if (room.startTimer) //If the timer is started, start moving the clock hands
        {
            if (!GetComponent<AudioSource>().isPlaying) //Starts the clock sound if not already playing
            {
                GetComponent<AudioSource>().Play();
            }

            //Rotate the clock hands
            minuteHand.transform.localRotation = Quaternion.Euler(new Vector3((360 - ((room.currentTime / 60) / 60) * 360), minuteHand.transform.localRotation.eulerAngles.y, minuteHand.transform.localRotation.eulerAngles.z));
            secondHand.transform.localEulerAngles = new Vector3(-6 * room.currentTime, 0, 0);

            if (room.currentTime < stressTick) //Start the stress sound when time is low
            {
                GetComponent<AudioSource>().clip = clockTickStress;
            }

            if (room.currentTime <= 0f) //Stop the clock when time is up
            {
                StopClock();
            }
        }
    }
}