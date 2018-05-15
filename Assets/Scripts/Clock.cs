using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    RoomVariables room;

    [SerializeField]
    GameObject minuteHand, secondHand;

    bool setHands = false;
    int lastTick;

    private void Start()
    {
        room = GetComponentInParent<RoomVariables>();
    }

    private void Update()
    {
        //TESTING ONLY
        if (Input.GetKeyDown(KeyCode.AltGr))
        {
            lastTick = (int)room.currentTime;
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
            minuteHand.transform.localRotation = Quaternion.Euler(new Vector3((360 - ((room.currentTime / 60) / 60) * 360), minuteHand.transform.localRotation.eulerAngles.y, minuteHand.transform.localRotation.eulerAngles.z));
            secondHand.transform.localEulerAngles = new Vector3(-6 * room.currentTime, 0, 0);
            if ((int)room.currentTime < lastTick)
            {
                lastTick = (int)room.currentTime;
                //tick sound here
            }
        }
    }
}