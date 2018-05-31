using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    //By Andreas Halldin
    //Handles the Main Menu

    [SerializeField]
    GameObject mainMenu, optionsMenu; //Two different parts of the menu

    [SerializeField] //The volume slider in the Options menu
    Slider volumeSlider;


    //MAIN MENU

    //Matchmaking is handled by NetworkingLobby

    public void Options() //Show Options menu
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    public void Exit() //Quit the game
    {
        Application.Quit();
    }

    //OPTIONS
    public void Volume() //Update the volume slider
    {
        float volume = volumeSlider.value;
        AudioListener.volume = volume;
    }
    public void Controls() //Set the players controls
    {
        print("NOT DONE YET");
    }
    public void Back() //Back to main menu
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

}
