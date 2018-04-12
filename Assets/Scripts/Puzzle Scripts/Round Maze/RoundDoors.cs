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

    public int buttonNumber;

	//public RoundDoors origin;

	void Awake(){

		foreach (GameObject _room in availableRoomsList) {

			backupRoomList.Add (_room.GetComponent<RoundDoors>());
		}
	}

    public bool FindPath(RoundDoors _origin)
    {
		bool _bool = true;
		if (layer != 0) {
			
			enteredNow = true;

			//Uppdates availible rooms list to not include rooms previously entered by this path
			availableRoomsList.Clear ();
			foreach (RoundDoors _room in backupRoomList) {

				if (!_room.enteredNow && _room.layer <= layer /* Honestly makes it a lot more stable*/) {

					availableRoomsList.Add (_room.gameObject);
				}
			}
			//Checks if path reached a dead end. 
			if (availableRoomsList.Count == 0) {	//If True: return to origin and try again

				if (_origin != this) {
					_bool = _origin.FindPath (_origin);
					return _bool;
				} else {	//If this already is origin the path gets stuck. Returns false (To 'RoundRoomWalls') and start over.
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
					
					_bool = room.FindPath (_origin);

					enteredNow = false;
					entered = true;
				}
			}
		} 
		return _bool;
    }
}