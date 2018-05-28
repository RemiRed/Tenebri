using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RoomVariables : NetworkBehaviour
{
    public GameObject entryDoor, exitDoor;
    public RoomVariables pairedRoom;
	[SerializeField]
	protected RoomLoader roomLoader;
	public RoomLoader.Room room = RoomLoader.Room.startRoom;
	public PlayerCommands playercommand;

    [SerializeField]
    float timerSeconds = 0, 
			timerPenalty = 1;
    //[HideInInspector]
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
	//Triggers GameOver variables when the player looses the game 
    void GameOver()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            player.GetComponent<CharacterScript>().gameOver = true;
            player.GetComponent<CharacterScript>().menu = true;
            player.GetComponent<CharacterScript>().gameOverMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }
	//Opens path to next section when a puzzle has been solved
    public void OpenDoorToNextLevel()
    {
        exitDoor.GetComponent<Animator>().SetBool("open", true);
        pairedRoom.GetComponent<RoomVariables>().exitDoor.GetComponent<Animator>().SetBool("open", true);
    }
	//Virutal methods to be overriden by inheriting scripts
	public virtual void CompleteSuccess(PlayerCommands playerCmd){}
    public virtual void PartialSuccess(PlayerCommands playerCmd) {}	 
	public virtual void Failure(PlayerCommands playerCmd) {}
}