using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundDoors : MonoBehaviour {

	public RoundRoomWalls resetManager;

    [SerializeField]
    List<GameObject> WallList = new List<GameObject>(), availableRoomsList = new List<GameObject>(); //room list har alla angränsande rum som antingen är på samma lager eller ett lager in, wall list är väggarna till de rummen.
	List<RoundDoors> backupRoomList = new List<RoundDoors>();
    public bool entered = false, enteredNow = false;
    public int layer;

	public RoundDoors origin;

	void Awake(){

		foreach (GameObject _room in availableRoomsList) {

			backupRoomList.Add (_room.GetComponent<RoundDoors>());
		}
	}

    public bool FindPath()
    {
		bool _bool = true;
		if (layer != 0) {
			
			enteredNow = true;

			//Uppdates availible rooms list to not include rooms previously entered by this path
			availableRoomsList.Clear ();
			foreach (RoundDoors _room in backupRoomList) {

				if (!_room.enteredNow) {

					availableRoomsList.Add (_room.gameObject);
				}
			}

			//Checks if path reached a dead end. 
			if (availableRoomsList.Count == 0) {	//If True: return to origin and try again

				if (origin != this) {
					_bool = origin.FindPath ();
					return _bool;
				} else {
//					Debug.LogWarning ("<! DEAD END GOT STUCK !>");
					return false;
				}

			} else {	//If False: Selects a random room to conntinue to
				
				RoundDoors room = availableRoomsList [Random.Range (0, availableRoomsList.Count)].GetComponent<RoundDoors> ();

				//Checks which wall the two rooms shares between them and Removes the wall between the two rooms
				foreach (GameObject wall in WallList) {

					if (room.WallList.Contains (wall)) {
						wall.GetComponent<RoundWallDoors> ().OpenSesamy ();
						break;
					}
				}
					
				if (room.entered) {	//End path if room previously entered by another path

					enteredNow = false;
					entered = true;

					return _bool;

				} else {	//Continue looking for a path

					room.origin = origin;
					_bool = room.FindPath ();

					enteredNow = false;
					entered = true;
				}
			}
		} 
		return _bool;
    }

	//Maybe obselete

//	public void OpenToRandomRoom(){
//
//		if (entered) {
//
//			return;
//		}
//		entered = true;
//
//		if (availableRoomsList.Count == 0) {
//
//			availableRoomsList.Clear ();
//			foreach(GameObject _room in backupRoomList){
//
//				availableRoomsList.Add (_room);
//			}
//		}
//
//		int randomRoomID = Random.Range (0, availableRoomsList.Count);
//
//		RoundDoors room = availableRoomsList [randomRoomID].GetComponent<RoundDoors> ();
//
//		Debug.Log ("Open a Door Now from " + gameObject + " to " + room.gameObject);
//
//		//Checks which wall the two rooms shares between them and Removes the wall between the two rooms
//		foreach (GameObject wall in WallList) {
//			foreach (GameObject walle in room.WallList) {
//				if (wall == walle) {
//					wall.GetComponent<RoundWallDoors> ().OpenSesamy ();
//					break;
//				}
//			}
//		}
//		room.availableRoomsList.Remove (gameObject);
//	}
}