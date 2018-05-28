/*
 * 
 * Made by: Andres
 * 
 */


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
    [SerializeField]
    Collider trapCollider;

    void Start()
    {
        anim = GetComponent<Animator>();
        a_isPulling = false;
    }
    // LeverScript ärver av Interractable som gör det möjligt att använda networking. Beroende på vad som skrivs i 
    // inspektorn i Unity så fungerar i detta fall då Pulling och LetGo en update funktion. Pulling är när man håller in och LetGo är när spelaren släpper knappen E.
    void Pulling()
    {
        //Animation för spaken.
        if (leverPulled == false)
        {

            a_isPulling = true;

            if (a_isPulling == true)
            {
                anim.SetBool("isPulling", true);
            }

            leverPulled = true;
           anim.Play("Pull");
            
        }
        
        if (leverId == 0) //Fake Lever
        {
            trapCollider.enabled = true;   
        }
        else //Real Lever
        {
            playerCmd.CmdMazeLever(leverId);
        }

    }
    //När spelaren har släppt knappen E.
    void LetGo()
    {
        if (leverPulled)
            if (leverPulled != false)
            {
                anim.SetBool("isPulling", false);
                
                leverPulled = false;
            }
    }
}
