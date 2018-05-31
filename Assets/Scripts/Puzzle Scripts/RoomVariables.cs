﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RoomVariables : NetworkBehaviour
{
    //By Andreas Halldin
    //Stores all variables for each room

    public GameObject entryDoor, exitDoor, pairedRoom;
    [SerializeField]
    protected RoomLoader roomLoader;
    public Room room = Room.startRoom;
    public PlayerCommands playercommand;

    [SerializeField]
    AudioClip openDoor;

    [SerializeField]
    float timerSeconds = 0,
            timerPenalty = 1;
    [HideInInspector]
    public float currentTime;

    public int allowedFailures = 1;
    public bool startTimer = false;
    public bool roomPassed = false;

    private void Start()
    {
        currentTime = timerSeconds;
    }

    //Called if the player fails in this room
    public bool Fail()
    {
        GetComponent<AudioSource>().Play();
        if (allowedFailures != 0)
        {
            allowedFailures--;
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
    //Handles the fail timer that results in GameOver() if it reaches 0
    IEnumerator StartTimer()
    {
        currentTime = timerSeconds;
        while (currentTime > 0 && !roomPassed)
        {
            print(currentTime);
            yield return new WaitForSeconds(.1f);
            currentTime -= .1f;
        }
        if (!roomPassed)
        {
            GameOver();
        }
    }
    
    //Triggers GameOver variables when the player loses the game 
    void GameOver()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponent<PlayerCommands>().isLocalPlayer == true)
            {
                player.GetComponent<PlayerCommands>().CmdGameOver();
                player.GetComponent<CharacterScript>().GameOver();
            }
        }
    }


    //Opens path to next section when a puzzle has been solved
    public void OpenDoorToNextLevel()
    {
        Clock clock = new Clock();
        bool tempCheck = false;
        if (GetComponentInChildren<Clock>())
        {
            clock = GetComponentInChildren<Clock>();
            tempCheck = true;
        }
        else if (pairedRoom.GetComponentInChildren<Clock>())
        {
            clock = pairedRoom.GetComponentInChildren<Clock>();
            tempCheck = true;
        }
        if (tempCheck)
        {
            clock.StopClock();
        }
        exitDoor.GetComponent<Animator>().SetBool("open", true);
        pairedRoom.GetComponent<RoomVariables>().exitDoor.GetComponent<Animator>().SetBool("open", true);
        exitDoor.GetComponent<AudioSource>().clip = openDoor;
        exitDoor.GetComponent<AudioSource>().Play();
        pairedRoom.GetComponent<RoomVariables>().exitDoor.GetComponent<AudioSource>().clip = openDoor;
        pairedRoom.GetComponent<RoomVariables>().exitDoor.GetComponent<AudioSource>().Play();
    }
    //Virutal methods to be overriden by inheriting scripts
    public virtual void CompleteSuccess(PlayerCommands playerCmd) { }
    public virtual void PartialSuccess(PlayerCommands playerCmd) { }
    public virtual void Failure(PlayerCommands playerCmd) { }
}