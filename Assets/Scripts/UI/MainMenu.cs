using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    [SerializeField]
    GameObject mainMenu, optionsMenu;

    [SerializeField]
    Slider volumeSlider;

    public void Options()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
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
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

}
