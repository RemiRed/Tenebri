using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
	
    [SerializeField]
    GameObject pauseMenu, optionsMenu;
	 
    [SerializeField]
    Slider volumeSlider;

    NetworkingLobby networkingLobby;
    public CharacterScript character;
    private void Start()
    {
        networkingLobby = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkingLobby>();
    }

    public void Resume()
    {
        character.ToggleMenu();
    }
    public void Options()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    public void MainMenu()
    {
        GameObject tempGO = new GameObject();
        networkingLobby.gameObject.transform.parent = tempGO.transform;
		Cursor.lockState = CursorLockMode.None;
		networkingLobby.StopHost();
    }
    public void Exit()
    {
        Application.Quit();
    }

    //OPTIONS
    public void Volume()
    {
        float volume = volumeSlider.value;
        AudioListener.volume = volume;
    }
    public void Controls()
    {
        print("NOT DONE YET");
    }
    public void Back()
    {
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

}
