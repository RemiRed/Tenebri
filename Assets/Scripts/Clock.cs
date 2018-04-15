using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField]
    RoomVariables room;

    int lastTick;
    // Use this for initialization
   /* void Start()
    {
        transform.localEulerAngles = new Vector3(-6 * room.timerSeconds, 0, 0);
        lastTick = (int)room.timerSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        if (room.startTimer)
        {
            print("I AM MOVING");
            transform.localEulerAngles = new Vector3(-6 * room.currentTime, 0, 0);
            if ((int)room.timerSeconds < lastTick)
            {
                lastTick = (int)room.timerSeconds;
                //tick sound here
    }      

        }
    }*/
}
