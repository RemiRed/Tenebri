using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundRoomWalls : MonoBehaviour {

    [SerializeField]
    List<GameObject> walls = new List<GameObject>();

	// Use this for initialization
	void Start () {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            CloseWalls();
    }
    
    private void CloseWalls()
    {
		foreach(GameObject bwa in walls)
        {
            bwa.SetActive(true);
        }
    }

}
