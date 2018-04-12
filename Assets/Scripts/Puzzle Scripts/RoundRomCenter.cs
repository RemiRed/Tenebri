using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundRomCenter : MonoBehaviour {

    bool activeRandom = true;
    [SerializeField]
    GameObject roundRoom;
    [SerializeField]
    GameObject roomManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider playa)
    {        
        if (playa.gameObject.tag == "Player" && activeRandom == true)
        {
            roomManager.GetComponent<RoundRoomManager>().CmdGetWallSymbols();
            roundRoom.GetComponent<RoundRoomWalls>().RandomSymbols();
            roomManager.GetComponent<RoundMazeMapRoom>().RpcMapButtons();
            activeRandom = false;
        }
    }
}
