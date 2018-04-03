using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundDoors : MonoBehaviour {

    [SerializeField]
    List<GameObject> WallList = new List<GameObject>(),
					availableRoomsList = new List<GameObject>(); //room list har alla angränsande rum som antingen är på samma lager eller ett lager in, wall list är väggarna till de rummen.
    
    public bool entered = false;
    public int layer;

//	// Use this for initialization
//	void Start () {
//        
//	}
//	
//	// Update is called once per frame
//	void Update () {
//
//	}

    public void FindPath()
    {
        if (layer != 0)
        {
            entered = true;
            int randomRoomID = Random.Range(0, availableRoomsList.Count);
            RoundDoors room = availableRoomsList[randomRoomID].GetComponent<RoundDoors>();
            Debug.Log(gameObject + " went into " + room);
            foreach(GameObject wall in WallList)
            {
                foreach(GameObject walle in room.WallList)
                {
                    if(wall == walle)
                    {
                        wall.GetComponent<RoundWallDoors>().OpenSesamy();
                        break;
                    }
                }
            }
           
            if (!room.entered)
            {
                if (layer == room.layer)
                {
                    room.availableRoomsList.Remove(gameObject);
                }
                room.FindPath();
            }
        }
        /*if(layer == 0)
        {
            bool allActive = true;
            for(int i = 0; i < WallList.Count; i++)
            {
                if (!WallList[i].activeInHierarchy)
                {
                    allActive = false;
                    break;
                }
                    
            }
            if (allActive == true)
            {
                WallList[Random.Range(0, WallList.Count)].SetActive(false);
                Debug.Log("noo");
            }
        }*/
    }

}
