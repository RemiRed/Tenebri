using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDown : MonoBehaviour {




    IEnumerator OnTriggerEnter(Collider other)
    {
        other.enabled = !other.enabled;
        yield return new WaitForSeconds(1f);
        other.enabled = !other.enabled;
    }




}
