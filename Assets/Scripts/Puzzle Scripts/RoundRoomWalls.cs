using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundRoomWalls : MonoBehaviour {

    [SerializeField]
    List<GameObject> walls = new List<GameObject>();

	// Use this for initialization
	void Start () {
        RandomRooms();
    }
    
    private void RandomRooms()
    {
		for(int i = 0; i < walls.Count; i++)
        {
            if (Random.Range(0, 10) < 5)
            {
                walls[i].SetActive(false);
            }
	    }
    }

}
