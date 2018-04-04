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
        int tempLayer = 0;
        bool firstLayer = false;
        foreach (GameObject button in buttons)
        {
            tempButtons.Add(button);
        }
		for(int i = 0; i < numberOfButtons; i++)
        {
            int randomDude = Random.Range(0, buttons.Count-i);

			if(tempButtons[randomDude] && i == 0)
            {
                tempButtons[randomDude].GetComponent<Renderer>().material.color = Color.red;
				tempButtons [randomDude].GetComponent<RoundDoors> ().origin = tempButtons [randomDude].GetComponent<RoundDoors> ();
                tempButtons[randomDude].GetComponent<RoundDoors>().FindPath();
                tempLayer = tempButtons[randomDude].GetComponent<RoundDoors>().layer;
                if (tempButtons[randomDude].GetComponent<RoundDoors>().layer == 1)
                    firstLayer = true;
                tempButtons.Remove(tempButtons[randomDude]);
            }
            else if(firstLayer != true)
            {
                List<GameObject> tempButtonslayers = new List<GameObject>();
                foreach(GameObject newButton in tempButtons)
                {
                    if (newButton.GetComponent<RoundDoors>().layer != tempLayer)
                    {
                        tempButtonslayers.Add(newButton);
                    }
                }
                int anotherRandomDude = Random.Range(0, tempButtonslayers.Count - 1);
                if (tempButtonslayers[anotherRandomDude])
                {
                    tempButtonslayers[anotherRandomDude].GetComponent<Renderer>().material.color = Color.red;
                    tempButtonslayers[anotherRandomDude].GetComponent<RoundDoors>().origin = tempButtonslayers[anotherRandomDude].GetComponent<RoundDoors>();
                    tempButtonslayers[anotherRandomDude].GetComponent<RoundDoors>().FindPath();
                    tempLayer = tempButtonslayers[anotherRandomDude].GetComponent<RoundDoors>().layer;
                    if (tempButtonslayers[anotherRandomDude].GetComponent<RoundDoors>().layer == 1)
                        firstLayer = true;
                    tempButtons.Remove(tempButtonslayers[anotherRandomDude]);
                }
            }
            else
            {
                List<GameObject> tempButtonslayers = new List<GameObject>();
                foreach (GameObject newButton in tempButtons)
                {
                    if (newButton.GetComponent<RoundDoors>().layer != tempLayer && newButton.GetComponent<RoundDoors>().layer != 1)
                    {
                        tempButtonslayers.Add(newButton);
                    }
                }
                int anotherRandomDude = Random.Range(0, tempButtonslayers.Count - 1);
                if (tempButtonslayers[anotherRandomDude])
                {
                    tempButtonslayers[anotherRandomDude].GetComponent<Renderer>().material.color = Color.red;
                    tempButtonslayers[anotherRandomDude].GetComponent<RoundDoors>().origin = tempButtonslayers[anotherRandomDude].GetComponent<RoundDoors>();
                    tempButtonslayers[anotherRandomDude].GetComponent<RoundDoors>().FindPath();
                    tempLayer = tempButtonslayers[anotherRandomDude].GetComponent<RoundDoors>().layer;
                    tempButtons.Remove(tempButtonslayers[anotherRandomDude]);
                }
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