using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RoomVariables : NetworkBehaviour
{
    public float length;
    public List<GameObject> compatibleRooms = new List<GameObject>();
    public GameObject entryDoor, exitDoor;
    public GameObject pairedRoom; //Needs to be changed to an enum or something similar later. THis is to do an easy Switch.
}
