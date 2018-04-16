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
		for (int i = 0; i < mapButtons.Count; i++) {

			if (pairedRoom.GetComponent<RoundRoomWalls> ().theseButtonsIndex.Contains (i)) {

				mapButtons[i].SetActive (true);
				mapButtons[i].GetComponent<Renderer> ().material.SetColor (Shader.PropertyToID ("_SpecColor"), Color.red);

			} else {
				
				mapButtons[i].SetActive (false);
			}
		}
		
//        foreach (GameObject button in mapButtons)
//        {
//			if (pairedRoom.GetComponent<RoundRoomWalls> ().theseButtonsIndex.Contains (button.GetComponent<RoundRoomMapButtonnumber> ().buttonNumber)) {
//
//				button.SetActive (true);
//				button.GetComponent<Renderer> ().material.SetColor (Shader.PropertyToID ("_SpecColor"), Color.red);
//
//			} else {
//				button.SetActive (false);
//			}
//        }
    }
}