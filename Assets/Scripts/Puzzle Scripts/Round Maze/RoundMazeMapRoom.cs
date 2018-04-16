using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RoundMazeMapRoom : RoomVariables
{
    [SerializeField]
    List<GameObject> mapButtons = new List<GameObject>();
    public List<Color> mapButtonColors = new List<Color>();
    [SerializeField]
    List<Material> materials = new List<Material>();

    [ClientRpc]
    public void RpcMapButtons()
    {
        int _index = 0;

        //Debug.Log("setting  map buttons");

        //for (int i = 0; i < mapButtons.Count; i++)
        //{

        //    if (pairedRoom.GetComponent<RoundRoomWalls>().theseButtonsIndex.Contains(i))
        //    {

        //        mapButtons[i].SetActive(true);
        //        mapButtons[i].GetComponent<Renderer>().material.color = mapButtonColors[_index];
        //        _index++;
        //       // mapButtons[i].GetComponent<Renderer>().material.SetColor(Shader.PropertyToID("_SpecColor"), Color.red);

        //    }
        //    else
        //    {

        //        mapButtons[i].SetActive(false);
        //    }
        //}

        foreach (GameObject button in mapButtons)
        {
            foreach (int _vector in pairedRoom.GetComponent<RoundRoomWalls>().theseButtonsIndex)
            {
                if (_vector == button.GetComponent<RoundRoomMapButtonnumber>().buttonNumber/*pairedRoom.GetComponent<RoundRoomWalls>().theseButtonsIndex.Contains(button.GetComponent<RoundRoomMapButtonnumber>().buttonNumber)*/)
                {
                    button.SetActive(true);
                    button.GetComponent<Renderer>().material.color = mapButtonColors[_index];
                    _index++;
                    break;
                }
                else
                {
                    button.SetActive(false);
                }

            }

        }
    }
    }