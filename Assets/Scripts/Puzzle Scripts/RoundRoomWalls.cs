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

	[SyncVar(hook = "ReRandomizeEverything")]
	public bool foundPath = true;

    void Update()
    {
		//Debug Stuff
		if (isServer && Input.GetKeyDown(KeyCode.G))
        {
			RpcCloseWalls ();
            RandomSymbols ();
        }
    }
    
    void RandomSymbols()
    {
		CloseWalls ();

		int tempLayer = 0;
		bool firstLayer = false;

		for(int i = 0; i < numberOfButtons; i++)
        {
			if (foundPath) {
				List<RoundDoors> tempButtons = new List<RoundDoors> ();
				foreach (GameObject _button in buttons) {
					//Conditions for buttons to be added to list of possible buttons to be selected
					if (!usedButtons.Contains (_button.GetComponent<RoundDoors> ()) &&
					    (firstLayer == false || (firstLayer == true && _button.GetComponent<RoundDoors> ().layer != 1)) &&
					    _button.GetComponent<RoundDoors> ().layer != tempLayer) {
					 	
						tempButtons.Add (_button.GetComponent<RoundDoors> ());
					}
				}

				int randomDude = Random.Range (0, tempButtons.Count);

				RpcFindPath (tempButtons [randomDude].gameObject);

//			tempButtons [randomDude].GetComponent<Renderer> ().material.color = Color.red;
//			tempButtons [randomDude].origin = tempButtons [randomDude];

//			if (!tempButtons [randomDude].FindPath ()) {
//				
//				RandomSymbols ();
//				break;
//			} 

				tempLayer = tempButtons [randomDude].layer;
				if (tempLayer == 1)
					firstLayer = true;
				usedButtons.Add (tempButtons [randomDude]);
			} 
        }

		//Opens the rooms that has not been entered
		foreach(GameObject _button in buttons){
		
			if (!_button.GetComponent<RoundDoors> ().entered) {

				_button.GetComponent<RoundDoors> ().origin = _button.GetComponent<RoundDoors> ();
				_button.GetComponent<RoundDoors> ().FindPath ();
			}
		}
    }
		
	[ClientRpc]
	void RpcFindPath(GameObject _button){

		Debug.Log ("Added a button");

		_button.GetComponent<Renderer> ().material.color = Color.red;
		_button.GetComponent<RoundDoors>().origin = _button.GetComponent<RoundDoors>();
		foundPath = _button.GetComponent<RoundDoors>().FindPath ();
	}

	void ReRandomizeEverything(bool _foundpath){

		if(!_foundpath) {

			Debug.Log ("EVERYTHING");
			CloseWalls ();

			if (isServer) {
				RandomSymbols ();
			}
		}
	}
		
	[ClientRpc]
	void RpcCloseWalls(){

		foreach(GameObject bwa in walls)
		{
			bwa.SetActive(true);
		}
		NeutralSymbols();
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
		foundPath = true;
    }
}