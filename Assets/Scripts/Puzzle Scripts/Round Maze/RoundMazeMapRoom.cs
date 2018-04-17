using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RoundMazeMapRoom : RoomVariables
{
    [SerializeField]
    List<GameObject> mapButtons = new List<GameObject>();
    public List<Color> mapColours = new List<Color>();

    [ClientRpc]
    public void RpcMapButtons()
    {
        /*for (int i = 0; i < mapButtons.Count; i++) {

			if (pairedRoom.GetComponent<RoundRoomWalls> ().theseButtonsIndex.Contains (i)) {

				mapButtons[i].SetActive (true);
				mapButtons[i].GetComponent<Renderer> ().material.SetColor (Shader.PropertyToID ("_SpecColor"), Color.red);

			} else {
				
				mapButtons[i].SetActive (false);
			}
		}*/
        List<GameObject> moreButtons = new List<GameObject>();
        int index = 0;
        foreach (int button in pairedRoom.GetComponent<RoundRoomWalls>().theseButtonsIndex)
        {
            foreach(GameObject _buttons in mapButtons)
            {
                if(button == _buttons.GetComponent<RoundRoomMapButtonnumber>().buttonNumber)
                {
                    _buttons.SetActive(true);
                    _buttons.GetComponent<Renderer>().material.color = mapColours[index];
                    index++;
                    moreButtons.Add(_buttons);
                }
                else if(!moreButtons.Contains(_buttons))
                {
                    _buttons.SetActive(false);
                }
            }
        }
    }
}