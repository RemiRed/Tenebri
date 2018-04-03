using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundRoomWalls : MonoBehaviour {

    [SerializeField]
    List<GameObject> walls = new List<GameObject>();
    [SerializeField]
    List<GameObject> buttons = new List<GameObject>();

	// Use this for initialization
	void Start () {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            CloseWalls();
        if (Input.GetKeyDown(KeyCode.T))
            RandomSymbols();
    }
    
    void CloseWalls()
    {
		foreach(GameObject bwa in walls)
        {
            bwa.SetActive(true);
        }
    }

    void RandomSymbols()
    {
        NeutralSymbols();
        for(int i = 0; i < 3; i++)
        {
            int randomDude = Random.Range(0, buttons.Count);
            if(buttons[randomDude].GetComponent<RoundDoors>().entered == true)
            {
                buttons[randomDude].GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }
    void NeutralSymbols()
    {
        foreach(GameObject bwu in buttons)
        {
            bwu.GetComponent<Renderer>().material.color = Color.gray;
        }
    }

}
