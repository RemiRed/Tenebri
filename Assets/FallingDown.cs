using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDown : MonoBehaviour {

    Animator animu;

    void Start()
    {
        animu = GetComponent<Animator>();
    }

    IEnumerator OnTriggerEnter(Collider other)
    {
        other.enabled = !other.enabled;
        animu.SetTrigger("isFalling");
        yield return new WaitForSeconds(1f);
        other.enabled = !other.enabled;
    }
}