using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDown : MonoBehaviour {
   
	Animator anim;
    [SerializeField]
    OutdoorMazeSuccess failure;
   
    //Hämtar rum variabeln failure.
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    //När spelaren går på collidern så kommer spelarens collider stängas av och spelaren kommer falla igenom i 1 sekund.
    //Animation kommer att spelas och sedan så hämtas Failure condition.
    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.enabled = !other.enabled;
            anim.SetTrigger("isFalling");
            yield return new WaitForSeconds(0.8f);
            other.enabled = !other.enabled;
            yield return new WaitForSeconds(5f);
            failure.Respawn(other.gameObject);
            failure.Failure(other.GetComponent<PlayerCommands>());
        }
    }
}