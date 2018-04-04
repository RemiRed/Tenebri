using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundDoors : MonoBehaviour {

	public RoundRoomWalls resetManager;

    [SerializeField]
    List<GameObject> WallList = new List<GameObject>(),
					availableRoomsList = new List<GameObject>(), //room list har alla angränsande rum som antingen är på samma lager eller ett lager in, wall list är väggarna till de rummen.
					backupRoomList = new List<GameObject>();
    public bool entered = false, bool2 = false;
    public int layer;

	public RoundDoors origin;

	void Awake(){

		foreach (GameObject _room in availableRoomsList) {

			backupRoomList.Add (_room);
		}
	}

    public bool FindPath()
    {
		bool _bool = true;
		if (layer != 0) {

			//Allows for infinite rests for improved debugging
			availableRoomsList.Clear ();
			foreach(GameObject _room in backupRoomList){

				availableRoomsList.Add (_room);
			}
				
//			//Improved layout randomization. Maybe? 
//			int randomRoomToKill = Random.Range (0, availableRoomsList.Count);
//			availableRoomsList.RemoveAt (randomRoomToKill);

			//Removes previously entered rooms from list of availible rooms
			bool2 = true;

			List<int> _intList = new List<int> ();
			for (int i = 0; i < availableRoomsList.Count; i++) {
				if (availableRoomsList [i].GetComponent<RoundDoors> ().bool2 /*|| availableRoomsList[i].GetComponent<RoundDoors>().layer > layer*/){
					_intList.Add (i);
				}
			}
			for (int i = 0; i < _intList.Count; i++) {
				availableRoomsList.RemoveAt (_intList [i] - i);
			}
				
			//Checks if path reached a dead end. 
			if (availableRoomsList.Count == 0) {	//If True: return to origin and try again

				//Debug.LogWarning ("Got stuck in a dead end");
				if (origin != this) {
					_bool = origin.FindPath ();
					return _bool;
				} else {
					Debug.LogWarning ("<! DEAD END GOT STUCK !>");
					return false;
				}

			} else {	//If False: choose a random room to go to

				int randomRoomID = Random.Range (0, availableRoomsList.Count);

				RoundDoors room = availableRoomsList [randomRoomID].GetComponent<RoundDoors> ();

			//	Debug.Log (gameObject + " went into " + room.gameObject);

				//Checks which wall the two rooms shares between them and Removes the wall between the two rooms
				foreach (GameObject wall in WallList) {
					foreach (GameObject walle in room.WallList) {
						if (wall == walle) {
							wall.GetComponent<RoundWallDoors> ().OpenSesamy ();
							break;
						}
					}
				}
					
				if (room.entered) {	//End path if room previously entered by another path

					Debug.Log ("FOUND ANOTHER BUTTONS PATH");

					bool2 = false;
					entered = true;

					return _bool;

				} else {	//Continue looking for a path

					room.origin = origin;
					_bool = room.FindPath ();

					bool2 = false;
					entered = true;
				}
			}

		} else {
			
			Debug.Log ("FOUND THE CENTER OF THE MAZE");
		}
		return _bool;
    }

	public void OpenToRandomRoom(){

		if (entered) {

			return;
		}

		if (availableRoomsList.Count == 0) {

			Debug.Log ("Screw it");

			availableRoomsList.Clear ();
			foreach(GameObject _room in backupRoomList){

				availableRoomsList.Add (_room);
			}
		}

		int randomRoomID = Random.Range (0, availableRoomsList.Count);

		RoundDoors room = availableRoomsList [randomRoomID].GetComponent<RoundDoors> ();

		Debug.Log ("Open a Door Now from " + gameObject + " to " + room.gameObject);

		//Checks which wall the two rooms shares between them and Removes the wall between the two rooms
		foreach (GameObject wall in WallList) {
			foreach (GameObject walle in room.WallList) {
				if (wall == walle) {
					wall.GetComponent<RoundWallDoors> ().OpenSesamy ();
					break;
				}
			}
		}

	}
}