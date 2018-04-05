using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RoomVariables : NetworkBehaviour
{
    public float roomLength;
    public List<GameObject> compatibleRooms = new List<GameObject>();
    public GameObject entryDoor, exitDoor;
    public GameObject pairedRoom; //Needs to be changed to an enum or something similar later. This is to do an easy Switch.

    [SerializeField]
    float timerSeconds, timerPenalty;
    public float currentTime;

    bool firstPenalty = true, startTimer = false;

    protected bool Fail()
    {
        if (firstPenalty)
        {
            firstPenalty = false;
            return false;
        }
        else if (!startTimer)
        {
            startTimer = true;
            StartCoroutine(StartTimer());
        }
        else
        {
            currentTime /= timerPenalty;
        }
        return true;
    }

    IEnumerator StartTimer()
    {
        currentTime = timerSeconds;
        while (currentTime > 0)
        {
            print(currentTime);
            yield return new WaitForSeconds(.1f);
            currentTime -= .1f;
        }
        GameOver();
    }

    private void GameOver()
    {
        print("NI E KASS, LUL GEJM ÖVER. GETGUDSTÄDSKRUBB");
        //Gameover kod
    }
}
