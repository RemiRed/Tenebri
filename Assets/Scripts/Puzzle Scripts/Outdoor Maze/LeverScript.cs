using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : Interractable
{

    [SerializeField]
    int leverId;

    Animator anim;
    bool leverPulled = false;
    bool a_isPulling;



    void Start()
    {
        anim = GetComponent<Animator>();
        a_isPulling = false;
    }

    void Pulling()
    {
        if (leverPulled == false)
        {

            a_isPulling = true;

            if (a_isPulling == true)
            {
                anim.SetBool("isPulling", true);
            }

            leverPulled = true;
           // anim.Play("Pull");
            Debug.Log("The Lever is now Pulled");
        }
        switch (leverId)
        {

            case 0:

                playerCmd.CmdMazeLever0();

                break;

            case 1:

                playerCmd.CmdMazeLever1();

                break;

            case 2:

                playerCmd.CmdMazeLever2();

                break;

            case 3:

                playerCmd.CmdLoad();

                break;

            default:

                break;

        }


    }
    void LettinGo()
    {
        if (leverPulled)
            if (leverPulled = !false)
            {
                anim.SetBool("isPulling", false);
                
                leverPulled = false;
            }
    }
}
