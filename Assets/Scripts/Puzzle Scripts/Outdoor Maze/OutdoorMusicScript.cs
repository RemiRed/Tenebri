using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutdoorMusicScript : MonoBehaviour {

    [SerializeField]
    AudioClip outdoorAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<AudioSource>().clip = outdoorAudio;
        }
    }
}
