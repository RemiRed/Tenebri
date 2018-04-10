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
        NeutralMapButtons();
       
        foreach (GameObject button in mapButtons)
        {
            if (pairedRoom.GetComponent<RoundRoomWalls>().theseButtons.Contains(button.GetComponent<RoundRoomMapButtonnumber>().buttonNumber))
            {
                button.GetComponent<Renderer>().material.SetColor(Shader.PropertyToID("_SpecColor"), Color.red);
            }
        }
    }

    void NeutralMapButtons()
    {
        foreach(GameObject bwutt in mapButtons)
        {
            print("hello");
            bwutt.GetComponent<Renderer>().material.SetColor(Shader.PropertyToID("_SpecColor"), Color.black);
            print("hello2");
        }
        
    }
}
