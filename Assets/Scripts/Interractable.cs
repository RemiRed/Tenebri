using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interractable : MonoBehaviour
{
    //By Andreas Halldin
    //All interractable game objects inherit this script

    [SerializeField]
	string interractMethodName = "", stopInterractMethodName = ""; //The name of the method that should be called when the object is interractade with
    [SerializeField]
    protected float delay; //The delay, if any, before the method is called

    [HideInInspector]
    public PlayerCommands playerCmd; //The players "PlayerCommands" script that interacted with this object
    [SerializeField]
    protected RoomLoader roomLoader; //The roomLoader script

    bool audioCheck = false; //Bool for checking if audio has been started

    public void Interract() //Calls the interactable objects method that should be called when it's interracted with after a delay, works like an Update, with a optional delay.
    {
        if (GetComponent<AudioSource>() && !audioCheck) //Plays the interactables sound, if any.
        {
            audioCheck = true;
            GetComponent<AudioSource>().PlayDelayed(delay);
        }
        Invoke(interractMethodName, delay); //Call the method of the object
    }

    public void StopInterract() //Calls the interactable objects method that should be called when it's no longer interracted with after a delay
    {
        audioCheck = false; //Let the audio for interacting with the object play again when the object is interacted with
        if (stopInterractMethodName != null && stopInterractMethodName != "") //If There is a method to be called when the object is no longer interacted with, call it.
        {
            Invoke(stopInterractMethodName, delay);
        }
    }
}
