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

    void Pull()
    {
        anim = GetComponent<Animator>();
        if (!pulled)
        {
            pulled = true;
            playerCmd.CmdCorridorLever();
            anim.Play("Pull");

        }
    }

    void Release()
    {
        playerCmd.CmdCorridorLeverRelease();
        pulled = false;
    }

}
