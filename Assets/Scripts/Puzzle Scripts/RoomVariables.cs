using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RoomVariables : NetworkBehaviour
{
    public GameObject entryDoor, exitDoor;
    public GameObject pairedRoom;
    public RoomLoader.Room room = RoomLoader.Room.startRoom;
	public PlayerCommands playercommand;

    [SerializeField]
    float timerSeconds = 0, timerPenalty = 1;
    public float currentTime;

    [SerializeField]
    protected RoomLoader roomLoader;
    
	bool firstPenalty = true; 
	public int allowedFailures = 1; 
	bool startTimer = false;
    public bool passed = false;

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
	// 'firstPenalty' Could be an Int to allow for a dynamic amount of allowed failures before the GameOver timer is started. 

	//Handles the fail timer that results in GameOVer if it reaches 0
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
	//Triggers GameOver variables when the player looses the game 
    private void GameOver()
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
	public virtual void CompleteSuccess(){}
    public virtual void PartialSuccess(){}	 
	public virtual void Failure(){}
}