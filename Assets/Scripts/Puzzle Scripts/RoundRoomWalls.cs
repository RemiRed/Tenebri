using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundRoomWalls : MonoBehaviour {

    [SerializeField]
    List<GameObject> walls = new List<GameObject>();
    [SerializeField]
    List<GameObject> buttons = new List<GameObject>();

	[SerializeField]
	int numberOfButtons;

	// Use this for initialization
	void Start () {

		RandomSymbols();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            CloseWalls();
		
        if (Input.GetKeyDown(KeyCode.G))
        {
			CloseWalls();
            RandomSymbols();
        }
    }
    
    void RandomSymbols()
    {
        List<GameObject> tempButtons = new List<GameObject>();
        foreach (GameObject button in buttons)
        {
            tempButtons.Add(button);
        }
		for(int i = 0; i < numberOfButtons; i++)
        {
            int randomDude = Random.Range(0, buttons.Count-i);

			if(tempButtons[randomDude])
            {
                tempButtons[randomDude].GetComponent<Renderer>().material.color = Color.red;
				tempButtons [randomDude].GetComponent<RoundDoors> ().origin = tempButtons [randomDude].GetComponent<RoundDoors> ();
                tempButtons[randomDude].GetComponent<RoundDoors>().FindPath();
                tempButtons.Remove(tempButtons[randomDude]);
            }
        }
    }
		
	//Resets Everything to default
	void CloseWalls()
	{
		foreach(GameObject bwa in walls)
		{
			bwa.SetActive(true);
		}
		NeutralSymbols();
	}

	//Resets all buttons to default
    void NeutralSymbols()
    {
        foreach(GameObject bwu in buttons)
        {
            bwu.GetComponent<Renderer>().material.color = Color.gray;
			bwu.GetComponent<RoundDoors> ().entered = false;
			bwu.GetComponent<RoundDoors> ().bool2 = false;
        }
    }
}