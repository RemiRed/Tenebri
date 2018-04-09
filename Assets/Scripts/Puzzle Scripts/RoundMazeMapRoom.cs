using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RoundMazeMapRoom : RoomVariables
{
    [SerializeField]
    List<GameObject> mapButtons = new List<GameObject>();
  
    [ClientRpc]
    public void RpcMapButtons()
    {
        print("helo");
        foreach (int i in pairedRoom.GetComponent<RoundRoomWalls>().theseButtons)
        {
            print(i);
        }
        foreach (GameObject button in mapButtons)
        {
            if (pairedRoom.GetComponent<RoundRoomWalls>().theseButtons.Contains(button.GetComponent<RoundRoomMapButtonnumber>().buttonNumber))
            {
                print("bla");
                button.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }
}
