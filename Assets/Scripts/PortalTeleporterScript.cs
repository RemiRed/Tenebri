using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporterScript : MonoBehaviour {
    
    Transform player;
    [SerializeField]
    Transform reciver;

    bool playerIsOverLapping = false;

	void Update () {
        
		if (playerIsOverLapping)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            // Ifall detta är sant: Så flyttas spelaren igenom portalen.
            if (dotProduct < 0f)
            {
                // Teleportera spelaren. 
                float rotationDiff = -Quaternion.Angle(transform.rotation, reciver.rotation);
               // rotationDiff += 40;
                player.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                player.position = reciver.position + positionOffset;

                playerIsOverLapping = false;
            }
        }
	}

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverLapping = true;
            player = other.transform;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverLapping = false;
            player = other.transform;
        }
    }
}
