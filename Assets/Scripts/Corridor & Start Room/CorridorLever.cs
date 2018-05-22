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

    bool pulled = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Pull()
    { 
        if (!pulled)
        {
            pulled = true;
            playerCmd.CmdCorridorLever();
            anim.SetBool("isPulling", true);
            anim.Play("Pull");
        }
    }

    void Release()
    {
        playerCmd.CmdCorridorLeverRelease();
        anim.SetBool("isPulling", false);
        pulled = false;
    }

}
