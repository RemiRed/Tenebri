using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : Interractable {

    Animator anim;
    bool leverPulled = false;


    void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    void Pulling()
    {
        if (leverPulled == false) {
            
            leverPulled = true;
            anim.Play("Pull");
            Debug.Log("The Lever is now Pulled");
        }

        
    }
    void LettinGo()
    {
        if (leverPulled =!false)
        {
           
            anim.Play("Return");
            leverPulled = false;
        }
    }
 
}
