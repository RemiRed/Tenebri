using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : Interractable {

    Animator anim;
    bool leverPulled = false;
    bool a_isPulling;
    [SerializeField]
    GameObject openThis;


    void Start()
    {
        anim = GetComponent<Animator>();
        a_isPulling = false;
    }
    
    void Pulling()
    {
        if (leverPulled == false) {

            a_isPulling = true;
            
            if(a_isPulling == true)
            {
                anim.SetBool("isPulling", true);
            }
           
            leverPulled = true;
            openThis.GetComponent<Animator>().SetTrigger("Open");
        }

        
    }
    void LettinGo()
    {
        if (leverPulled)
        {
            Debug.Log("It Comes here??");
 
            Debug.Log("It Comes here 2n?");
            anim.SetBool("isPulling", false);
            leverPulled = false;
        }
    }
 
}
