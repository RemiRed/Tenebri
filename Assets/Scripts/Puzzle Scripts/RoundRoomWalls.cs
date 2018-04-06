using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RoundRoomWalls : NetworkBehaviour {

    [SerializeField]
    List<GameObject> walls = new List<GameObject>();
    [SerializeField]
    List<GameObject> buttons = new List<GameObject>();
	List<RoundDoors> usedButtons = new List<RoundDoors> ();

	[SerializeField]
	int numberOfButtons;

    void Update()
    {
		//Debug Stuff
		if (Input.GetKeyDown(KeyCode.G))
        {
            RandomSymbols();
        }
    }
    
    void RandomSymbols()
    {
		//CloseWalls();

		int tempLayer = 0;
		bool firstLayer = false;

		for(int i = 0; i < numberOfButtons; i++)
        {
			List<RoundDoors> tempButtons = new List<RoundDoors>();
			foreach (GameObject _button in buttons)
			{
				//Conditions for buttons to be added to list of possible buttons to be selected
				if (!usedButtons.Contains (_button.GetComponent<RoundDoors>()) && 
					(firstLayer == false || (firstLayer == true && _button.GetComponent<RoundDoors> ().layer != 1)) && 
					_button.GetComponent<RoundDoors> ().layer != tempLayer) {
					 	
					tempButtons.Add (_button.GetComponent<RoundDoors>());
				}
			}

			int randomDude = Random.Range(0, tempButtons.Count);

			tempButtons [randomDude].GetComponent<Renderer> ().material.color = Color.red;
			tempButtons [randomDude].origin = tempButtons [randomDude];

			if (!tempButtons [randomDude].FindPath ()) {
				
				RandomSymbols ();
				break;
			} 
			tempLayer = tempButtons [randomDude].layer;
			if (tempLayer == 1) firstLayer = true;
			usedButtons.Add (tempButtons [randomDude]);
        }

		foreach(GameObject _button in buttons){
		
			if (!_button.GetComponent<RoundDoors> ().entered) {

				_button.GetComponent<RoundDoors> ().origin = _button.GetComponent<RoundDoors> ();
				_button.GetComponent<RoundDoors> ().FindPath ();
			}
			//_rest.GetComponent<RoundDoors>().OpenToRandomRoom (); //Maybe Obselete
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
			bwu.GetComponent<RoundDoors> ().origin = null;
			bwu.GetComponent<RoundDoors> ().entered = false;
			bwu.GetComponent<RoundDoors> ().enteredNow = false;
        }
		usedButtons.Clear ();
    }
}

//public class RoundRoomWalls : NetworkBehaviour {
//
//	[SerializeField]
//	List<GameObject> walls = new List<GameObject>();
//	[SerializeField]
//	List<GameObject> buttons = new List<GameObject>();
//
//	[SerializeField]
//	int numberOfButtons;
//
//	// Use this for initialization
//	void Start () {
//
//	}
//
//	void Update()
//	{
//		//Debug Stuff
//		if (Input.GetKeyDown(KeyCode.G))
//		{
//			RandomSymbols();
//		}
//	}
//
//	void RandomSymbols()
//	{
//		CloseWalls();
//
//		List<GameObject> tempButtons = new List<GameObject>();
//		foreach (GameObject button in buttons)
//		{
//			tempButtons.Add(button);
//		}
//
//		int tempLayer = 0;
//		bool firstLayer = false;
//
//		for(int i = 0; i < numberOfButtons; i++)
//		{
//			int randomDude = Random.Range(0, buttons.Count-i);
//
//			if(tempButtons[randomDude] && i == 0)
//			{
//				tempButtons[randomDude].GetComponent<Renderer>().material.color = Color.red;
//				tempButtons [randomDude].GetComponent<RoundDoors> ().origin = tempButtons [randomDude].GetComponent<RoundDoors> ();
//				if (!tempButtons [randomDude].GetComponent<RoundDoors> ().FindPath ()) {
//
//					RandomSymbols ();
//					break;
//				} 
//				tempLayer = tempButtons[randomDude].GetComponent<RoundDoors>().layer;
//				if (tempButtons[randomDude].GetComponent<RoundDoors>().layer == 1)
//					firstLayer = true;
//				tempButtons.Remove(tempButtons[randomDude]);
//			}
//			else if(firstLayer != true)
//			{
//				List<GameObject> tempButtonslayers = new List<GameObject>();
//				foreach(GameObject newButton in tempButtons)
//				{
//					if (newButton.GetComponent<RoundDoors>().layer != tempLayer)
//					{
//						tempButtonslayers.Add(newButton);
//					}
//				}
//
//				int anotherRandomDude = Random.Range(0, tempButtonslayers.Count - 1);
//
//				if (tempButtonslayers[anotherRandomDude])
//				{
//					tempButtonslayers[anotherRandomDude].GetComponent<Renderer>().material.color = Color.red;
//					tempButtonslayers[anotherRandomDude].GetComponent<RoundDoors>().origin = tempButtonslayers[anotherRandomDude].GetComponent<RoundDoors>();
//					if (!tempButtonslayers [anotherRandomDude].GetComponent<RoundDoors> ().FindPath ()) {
//
//						RandomSymbols ();
//						break;
//					} 
//					tempLayer = tempButtonslayers[anotherRandomDude].GetComponent<RoundDoors>().layer;
//					if (tempButtonslayers[anotherRandomDude].GetComponent<RoundDoors>().layer == 1)
//						firstLayer = true;
//					tempButtons.Remove(tempButtonslayers[anotherRandomDude]);
//				}
//			}
//			else
//			{
//				List<GameObject> tempButtonslayers = new List<GameObject>();
//				foreach (GameObject newButton in tempButtons)
//				{
//					if (newButton.GetComponent<RoundDoors>().layer != tempLayer && newButton.GetComponent<RoundDoors>().layer != 1)
//					{
//						tempButtonslayers.Add(newButton);
//					}
//				}
//				int anotherRandomDude = Random.Range(0, tempButtonslayers.Count - 1);
//				if (tempButtonslayers[anotherRandomDude])
//				{
//					tempButtonslayers[anotherRandomDude].GetComponent<Renderer>().material.color = Color.red;
//					tempButtonslayers[anotherRandomDude].GetComponent<RoundDoors>().origin = tempButtonslayers[anotherRandomDude].GetComponent<RoundDoors>();
//					if (!tempButtonslayers [anotherRandomDude].GetComponent<RoundDoors> ().FindPath ()) {
//
//						RandomSymbols ();
//						break;
//					} 
//					tempLayer = tempButtonslayers[anotherRandomDude].GetComponent<RoundDoors>().layer;
//					tempButtons.Remove(tempButtonslayers[anotherRandomDude]);
//				}
//			}
//		}
//
//		foreach(GameObject _rest in tempButtons){
//
//			if (!_rest.GetComponent<RoundDoors> ().entered) {
//
//				_rest.GetComponent<RoundDoors> ().origin = _rest.GetComponent<RoundDoors> ();
//				_rest.GetComponent<RoundDoors> ().FindPath ();
//			}
//
//			//_rest.GetComponent<RoundDoors>().OpenToRandomRoom (); //Maybe Obselete
//		}
//	}
//
//	//Resets Everything to default
//	void CloseWalls()
//	{
//		foreach(GameObject bwa in walls)
//		{
//			bwa.SetActive(true);
//		}
//		NeutralSymbols();
//	}
//
//	//Resets all buttons to default
//	void NeutralSymbols()
//	{
//		foreach(GameObject bwu in buttons)
//		{
//			bwu.GetComponent<Renderer>().material.color = Color.gray;
//			bwu.GetComponent<RoundDoors> ().origin = null;
//			bwu.GetComponent<RoundDoors> ().entered = false;
//			bwu.GetComponent<RoundDoors> ().enteredNow = false;
//		}
//	}
//}