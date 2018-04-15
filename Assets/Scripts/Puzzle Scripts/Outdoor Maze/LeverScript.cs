using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : Interractable
{

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
