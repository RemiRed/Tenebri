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
        if (!leverPulled) {
            
            leverPulled = true;
            anim.Play("isPulling");
            Debug.Log("The Lever is now Pulled");
        }

        
    }
    void LettinGo()
    {
        if (leverPulled)
        {
           
            anim.Play("LetGo");
            leverPulled = false;
            Debug.Log("Lettin go...");
        }
    }
 
}
