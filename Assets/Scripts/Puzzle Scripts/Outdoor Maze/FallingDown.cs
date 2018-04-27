using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDown : MonoBehaviour {
   
	Animator anim;
    [SerializeField]
    RoomVariables failure;
    //Hämtar rum variabeln failure.
    void Start()
    {
        anim = GetComponent<Animator>();
		failure = GameObject.FindGameObjectWithTag ("OutdoorMaze").GetComponent<RoomVariables> ();
    }
    //När spelaren går på collidern så kommer spelarens collider stängas av och spelaren kommer falla igenom i 1 sekund.
    //Animation kommer att spelas och sedan så hämtas Failure condition.
    IEnumerator OnTriggerEnter(Collider other)
    {
        other.enabled = !other.enabled;
        anim.SetTrigger("isFalling");
        yield return new WaitForSeconds(1f);
        other.enabled = !other.enabled;
        yield return new WaitForSeconds(7f);
        failure.Failure();
    }
}