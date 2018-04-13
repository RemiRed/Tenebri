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
	float timerSeconds, timerPenalty = 1;
    public float currentTime;

	bool firstPenalty = true, startTimer = false; 
	public bool passed = false;

	public /*protected*/ bool Fail()
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
		while (currentTime > 0 && !passed)
        {
            print(currentTime);
            yield return new WaitForSeconds(.1f);
            currentTime -= .1f;
        }
		if (!passed) {
			GameOver ();
		}
    }

    private void GameOver()
    {
		Debug.LogError ("NI E KASS, LUL GEJM ÖVER. GETGUDSTÄDSKRUBB");
    }

//	public void OpenDoorToNextLevel(){
//
//		Debug.Log ("Door to next level should now be open");
//	}
//
//	public virtual void CompleteSuccess(){
//
//	}
//
//	public virtual void PartialSuccess(){
//
//	}
//
//	public virtual void Failure(){
//
//	}
}