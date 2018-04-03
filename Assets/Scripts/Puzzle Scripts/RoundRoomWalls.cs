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
        if (Input.GetKeyDown(KeyCode.G))
        {
            print("Wohoo");
            RandomSymbols();
        }

    }
    
    void CloseWalls()
    {
		foreach(GameObject bwa in walls)
        {
            bwa.SetActive(true);
        }
        NeutralSymbols();
    }

    void RandomSymbols()
    {
        List<GameObject> tempButtons = new List<GameObject>();
        foreach (GameObject button in buttons)
        {
            tempButtons.Add(button);
        }
        for(int i = 0; i < 3; i++)
        {
            int randomDude = Random.Range(0, buttons.Count);
            if(tempButtons[randomDude])
            {
                tempButtons[randomDude].GetComponent<Renderer>().material.color = Color.red;
                tempButtons[randomDude].GetComponent<RoundDoors>().FindPath();
                tempButtons.Remove(tempButtons[randomDude]);
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
