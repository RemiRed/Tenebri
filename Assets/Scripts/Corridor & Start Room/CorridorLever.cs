using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CorridorLever : Interractable
{

    public GameObject exitDoor;
    public GameObject entryDoor;
    Animator anim;

    [SerializeField]
    float doorDelay;

    [SerializeField]
    float leverDelay;

    bool a_isPulling;
    bool pulled = false;

    void Start()
    {
        anim = GetComponent<Animator>();

    }

    void Pull()
    { 
        if (!pulled)
        {
            a_isPulling = true;
            if (a_isPulling = true)
            {
                anim.SetBool("isPull", true);
            }
            anim.Play("newPull");
            playerCmd.CmdCorridorLever();
            
        }
    }

    void Release()
    {
        if (pulled)
        {
            if (pulled != false)
            {

            

        playerCmd.CmdCorridorLeverRelease();
        anim.SetBool("isPull", false);
        pulled = false;
                }
        }
    }

}
