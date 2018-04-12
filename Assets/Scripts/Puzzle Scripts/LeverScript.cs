using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : Interractable
{

    Animator anim;
    bool leverPulled = false;
    bool a_isPulling;
    [SerializeField]
    GameObject openThis;

    [SerializeField]
    int leverId;

    void Start()
    {
        anim = GetComponent<Animator>();
        a_isPulling = false;
    }

    void Pulling()
    {
        if (!leverPulled)
        {
            if (leverPulled == false)
            {

                a_isPulling = true;

                if (a_isPulling == true)
                {
                    anim.SetBool("isPulling", true);
                }

                leverPulled = true;
                anim.Play("Pull");
                Debug.Log("The Lever is now Pulled");
                openThis.GetComponent<Animator>().SetTrigger("Open");
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
        }
    }
}

        
   /* }
    void LettinGo()
    {
        if (leverPulled =!false)
        {
            anim.Play("Return");
            leverPulled = false;
        }
    }
 
}
    }*/
