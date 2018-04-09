using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown : MonoBehaviour {

    [SerializeField]
    Collider PlayerCollider;

    void OnTriggerEnter(Collider other)
    {
        (other.gameObject.GetComponent(typeof(Collider)) as Collider).isTrigger = true;

    }

    void OnTriggerExit(Collider other)
    {
        (other.gameObject.GetComponent(typeof(Collider)) as Collider).isTrigger = false;
    }

    
}
