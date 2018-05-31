using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutdoorMusicScript : MonoBehaviour
{
    //By Andreas Halldin
    //Changes the music to the outdoor music after the player has gone outside
    [SerializeField]
    AudioClip outdoorAudio; //Outdoor music clip

    private void OnTriggerEnter(Collider other) //Change the music when a player enters the trigger
    {
        if (other.tag == "Player") 
        {
            GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<AudioSource>().clip = outdoorAudio;
        }
    }
}
