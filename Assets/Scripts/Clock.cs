using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    RoomVariables room;

    [SerializeField]
    GameObject minuteHand, secondHand;
    [SerializeField]
    AudioClip clockTickStress;

    bool setHands = false;
    [SerializeField]
    float stressTick = 60f;

    private void Start()
    {
        room = GetComponentInParent<RoomVariables>();
    }

    public void StopClock()
    {
        GetComponent<AudioSource>().Stop();
    }
    private void Update()
    {
        //TESTING ONLY
        if (Input.GetKeyDown(KeyCode.AltGr))
        {
            room.Fail();
        }

        if (!setHands)
        {
            setHands = true;
            minuteHand.transform.localRotation = Quaternion.Euler(new Vector3((360 - ((room.currentTime / 60) / 60) * 360), minuteHand.transform.localRotation.eulerAngles.y, minuteHand.transform.localRotation.eulerAngles.z));
            secondHand.transform.localRotation = Quaternion.Euler(new Vector3((360 - ((room.currentTime % 60) / 60f) * 360), secondHand.transform.localRotation.eulerAngles.y, secondHand.transform.localRotation.eulerAngles.z));
        }

        if (room.startTimer)
        {
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play();
            }
            minuteHand.transform.localRotation = Quaternion.Euler(new Vector3((360 - ((room.currentTime / 60) / 60) * 360), minuteHand.transform.localRotation.eulerAngles.y, minuteHand.transform.localRotation.eulerAngles.z));
            secondHand.transform.localEulerAngles = new Vector3(-6 * room.currentTime, 0, 0);
            if (room.currentTime < stressTick)
            {
                GetComponent<AudioSource>().clip = clockTickStress;
            }
            if (room.currentTime <= 0f)
            {
                StopClock();
            }
        }
    }
}