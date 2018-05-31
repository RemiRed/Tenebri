using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CorridorLever : Interractable
{
    //By Andreas Halldin & Andrés Ramirez
    //Handles pulling the lever in the corridors

    Animator anim; //The Animator of the lever

    bool pulled = false; //Bool for checking if the lever has been pulled

    void Start() //Get the animator
    {
        anim = GetComponent<Animator>();
    }

    void Pull() //The lever is pulled
    {
        if (!pulled)
        {
            pulled = true;
            anim.SetBool("isPull", true); //Trigger the animation for pulling the lever
            anim.Play("newPull");
            playerCmd.CmdCorridorLever();
        }
    }

    void Release() //Lever is released
    {
        if (pulled)
        {
            playerCmd.CmdCorridorLeverRelease(); 
            anim.SetBool("isPull", false); //Trigger animation for releasing the lever
            pulled = false;
        }
    }

}
