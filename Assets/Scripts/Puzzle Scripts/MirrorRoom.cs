using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MirrorRoom : RoomVariables
{

    [SyncVar]
    public int playerCol, playerRow;

    [SerializeField]
    List<MirrorPuzzleWalls> walls = new List<MirrorPuzzleWalls>();

    int col = 3, row = 0;

    void Start()
    {
        foreach (GameObject wall in GameObject.FindGameObjectsWithTag("MirrorRoomWall"))
        {
            if (wall.GetComponent<MirrorPuzzleWalls>())
            {
                walls.Add(wall.GetComponent<MirrorPuzzleWalls>());
            }
        }
     
        List<MirrorPuzzleWalls> tempTestList = new List<MirrorPuzzleWalls>();
        foreach (MirrorPuzzleWalls wall in walls)
        {
            if (wall.col + 1 == col && wall.row == row)
            {
                tempTestList.Add(wall);
            }
            else if (wall.col - 1 == col && wall.row == row)
            {
                tempTestList.Add(wall);
            }
            else if (wall.col == col && wall.row - 1 == row)
            {
                tempTestList.Add(wall);
            }

        }
        int tempTest = Random.Range(0, 3);
        Destroy(tempTestList[tempTest].gameObject);
        tempTestList.RemoveAt(tempTest);
    }

    void Update()
    {
        CheckIfSameTile();
    }

    void CheckIfSameTile()
    {
        if (pairedRoom.GetComponent<MirrorRoom>().playerCol == playerCol && pairedRoom.GetComponent<MirrorRoom>().playerRow == playerRow)
        {
            Debug.Log("is nice");
        }
        else
        {
            Debug.Log("is not nice");
        }
    }

}
