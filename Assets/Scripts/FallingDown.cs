using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDown : MonoBehaviour {
    Animator anim;
    [SerializeField]
    RoomVariables failure;

    void Start()
    {
        anim = GetComponent<Animator>();
    }



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
