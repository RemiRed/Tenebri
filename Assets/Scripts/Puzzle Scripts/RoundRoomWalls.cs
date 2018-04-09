using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundRoomWalls : MonoBehaviour {

	//Debug stuff
	public bool _testing;
	int testNumber = 0;

    [SerializeField]
    List<GameObject> walls = new List<GameObject>();
    [SerializeField]
    List<GameObject> buttons = new List<GameObject>();

	[SerializeField]
	int numberOfButtons;

	// Use this for initialization
	void Start () {

		if (!_testing) {
			
			RandomSymbols ();
		}
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            CloseWalls();
		
		if ( _testing || Input.GetKeyDown(KeyCode.G))
        {
			testNumber++;
            RandomSymbols();
        }
    }
    
    void RandomSymbols()
    {
		CloseWalls();

        List<GameObject> tempButtons = new List<GameObject>();
		foreach (GameObject button in buttons)
		{
			tempButtons.Add(button);
		}

        int tempLayer = 0;
        bool firstLayer = false;
        
		for(int i = 0; i < numberOfButtons; i++)
        {
            int randomDude = Random.Range(0, buttons.Count-i);

			if(tempButtons[randomDude] && i == 0)
            {
                tempButtons[randomDude].GetComponent<Renderer>().material.color = Color.red;
				tempButtons [randomDude].GetComponent<RoundDoors> ().origin = tempButtons [randomDude].GetComponent<RoundDoors> ();
				if (!tempButtons [randomDude].GetComponent<RoundDoors> ().FindPath ()) {
					Debug.LogError ("RE-RANDOM EVERYTHING! at " + testNumber);
					_testing = false;
					testNumber = 0;
				
					RandomSymbols ();
					break;
				} 
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
					if (!tempButtonslayers [anotherRandomDude].GetComponent<RoundDoors> ().FindPath ()) {
						Debug.LogError ("RE-RANDOM EVERYTHING! at " + testNumber);
						_testing = false;
						testNumber = 0;

						RandomSymbols ();
						break;
					} 
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
					if (!tempButtonslayers [anotherRandomDude].GetComponent<RoundDoors> ().FindPath ()) {
						Debug.LogError ("RE-RANDOM EVERYTHING! at " + testNumber);
						_testing = false;
						testNumber = 0;

						RandomSymbols ();
						break;
					} 
                    tempLayer = tempButtonslayers[anotherRandomDude].GetComponent<RoundDoors>().layer;
                    tempButtons.Remove(tempButtonslayers[anotherRandomDude]);
                }
            }
        }

		foreach(GameObject _rest in tempButtons){
		
			if (!_rest.GetComponent<RoundDoors> ().entered) {

				_rest.GetComponent<RoundDoors> ().origin = _rest.GetComponent<RoundDoors> ();
				_rest.GetComponent<RoundDoors> ().FindPath ();
			}

			//_rest.GetComponent<RoundDoors>().OpenToRandomRoom ();
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
			bwu.GetComponent<RoundDoors> ().bool2 = false;
        }
    }
}
	  
//	public void RandomSymbols()
//    {
//		CloseWalls();
//
//        List<GameObject> tempButtons = new List<GameObject>();
//        foreach (GameObject button in buttons)
//        {
//            tempButtons.Add(button);
//        }
//		for(int i = 0; i < numberOfButtons; i++)
//        {
//            int randomDude = Random.Range(0, buttons.Count-i);
//
//			if(tempButtons[randomDude])
//            {
//                tempButtons[randomDude].GetComponent<Renderer>().material.color = Color.red;
//				tempButtons [randomDude].GetComponent<RoundDoors> ().origin = tempButtons [randomDude].GetComponent<RoundDoors> ();
//				if (!tempButtons [randomDude].GetComponent<RoundDoors> ().FindPath ()) {
//					Debug.LogError ("RE-RANDOM EVERYTHING! at " + testNumber);
//					_testing = false;
//					testNumber = 0;
//
//					RandomSymbols ();
//					break;
//				} 
//				tempButtons.Remove (tempButtons [randomDude]);
//            }
//        }
//		foreach(GameObject _rest in tempButtons){
//
//			_rest.GetComponent<RoundDoors>().OpenToRandomRoom ();
//		}
//    }