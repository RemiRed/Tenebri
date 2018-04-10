using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown : MonoBehaviour {

    


    IEnumerator OnTriggerEnter(Collider other)
    {
        other.enabled = !other.enabled;
        yield return new WaitForSeconds(0.5f);
        other.enabled = !other.enabled;
    }

   

    
}
