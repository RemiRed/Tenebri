using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundDoors : MonoBehaviour {

    [SerializeField]
    List<GameObject> WallList = new List<GameObject>(),
					availableRoomsList = new List<GameObject>(); //room list har alla angränsande rum som antingen är på samma lager eller ett lager in, wall list är väggarna till de rummen.
    
    public bool entered = false, bool2 = false;
    public int layer;

	public RoundDoors origin;

	// Use this for initialization
	void Start () {
        
	}

    public void FindPath()
    {
		if (layer != 0) {
			bool2 = true;

			List<int> _intList = new List<int> ();
			for (int i = 0; i < availableRoomsList.Count; i++) {
				if (availableRoomsList [i].GetComponent<RoundDoors> ().bool2) {
					_intList.Add (i);
				}
			}
			for (int i = 0; i < _intList.Count; i++) {
				availableRoomsList.RemoveAt (_intList [i] - i);
			}

			int randomRoomID = Random.Range (0, availableRoomsList.Count);

			Debug.Log (randomRoomID + " " + availableRoomsList.Count);

			if (availableRoomsList.Count == 0) {

				Debug.LogWarning ("Got stuck in a dead end");
				origin.FindPath ();
				return;
			}

			RoundDoors room = availableRoomsList [randomRoomID].GetComponent<RoundDoors> ();
           
			Debug.Log (gameObject + " went into " + room.gameObject);
            
			foreach (GameObject wall in WallList) {
				foreach (GameObject walle in room.WallList) {
					if (wall == walle) {
						wall.GetComponent<RoundWallDoors> ().OpenSesamy ();
						break;
					}
				}
			}
           
			Debug.Log (room.gameObject + "Entered = " + room.entered + " and Bool2 = " + bool2);

			if (room.entered) {

				Debug.Log ("FOUND ANOTHER BUTTONS PATH");
				return;
			}
				
			room.origin = origin;
			room.FindPath ();

			bool2 = false;
			entered = true;

		} else {
			
			Debug.Log ("FOUND THE CENTER OF THE MAZE");
		}
    }
}