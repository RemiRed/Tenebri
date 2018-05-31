using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
	
    //By Andreas Halldin
    //Handles the pause menu

    [SerializeField]
    GameObject pauseMenu, optionsMenu; // The two option windows to switch between
	 
    [SerializeField]
    Slider volumeSlider; //The volume slider in the options

    NetworkingLobby networkingLobby; //The lobby

    public CharacterScript character; //The Character Script of the local player


    private void Start()  //Find the Network Manager
    {
        networkingLobby = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkingLobby>();
    }

    //PAUSE MENU
    public void Resume() //Return to the game
    {
        character.ToggleMenu();
    }
    public void Options() //Go to options window
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    public void MainMenu() //Back to main menu
    {
        GameObject tempGO = new GameObject();
        networkingLobby.gameObject.transform.parent = tempGO.transform;
		Cursor.lockState = CursorLockMode.None;
		networkingLobby.StopHost();
    }
    public void Exit() //Quit the game
    {
        Application.Quit();
    }

    //OPTIONS
    public void Volume() //Update volume
    {
        float volume = volumeSlider.value;
        AudioListener.volume = volume;
    }
    public void Controls() //Change the controls
    {
        print("NOT DONE YET");
    }
    public void Back() //Back to pause menu
    {
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

}
