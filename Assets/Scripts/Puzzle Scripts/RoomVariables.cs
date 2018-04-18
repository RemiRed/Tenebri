using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RoomVariables : NetworkBehaviour
{
    public GameObject entryDoor, exitDoor;
    public GameObject pairedRoom;
    public RoomLoader.Room room = RoomLoader.Room.startRoom;

    [SerializeField]
    float timerSeconds = 0, timerPenalty = 1;
    public float currentTime;

    [SerializeField]
    protected RoomLoader roomLoader;

    [SerializeField]
    string doorAnimOpen, doorAnimClose;

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
        if (!passed)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            player.GetComponent<CharacterScript>().gameOver = true;
            player.GetComponent<CharacterScript>().menu = true;
            player.GetComponent<CharacterScript>().gameOverMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        Debug.LogError("NI E KASS, LUL GEJM ÖVER. GETGUDSTÄDSKRUBB");
    }

    public void OpenDoorToNextLevel()
    {
        exitDoor.GetComponent<Animator>().SetBool("open", true);
        pairedRoom.GetComponent<RoomVariables>().exitDoor.GetComponent<Animator>().SetBool("open", true);
    }


    public virtual void CompleteSuccess()
    {

    }

    public virtual void PartialSuccess()
    {

    }


    public virtual void Failure()
    {

    }
}