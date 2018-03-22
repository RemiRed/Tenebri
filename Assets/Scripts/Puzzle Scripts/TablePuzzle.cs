using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TablePuzzle : RoomVariables
{
    [SerializeField]
    List<GameObject> availableItems = new List<GameObject>();

    List<GameObject> spawnedItems = new List<GameObject>();

    [SerializeField]
    List<GameObject> spawnPositions = new List<GameObject>();

    [SerializeField]
    int totalItems;

    [SerializeField]
    List<OnTable> onTables = new List<OnTable>();

    [SerializeField]
    List<Sprite> symbols = new List<Sprite>();

    [SerializeField]
    List<GameObject> symbolClues = new List<GameObject>();



    void Start()
    {
        for (int i = 0; i < totalItems; i++)
        {
            int tempID = Random.Range(0, availableItems.Count);
            int tempPos = Random.Range(0, spawnPositions.Count);
            spawnedItems.Add(Instantiate(availableItems[tempID], spawnPositions[tempPos].transform.position, new Quaternion(), transform));
            availableItems.RemoveAt(tempID);
            spawnPositions.RemoveAt(tempPos);
        }

        for (int i = 0; i < spawnedItems.Count; i++)
        {
            onTables[i % 3].correctItems.Add(spawnedItems[i]);
        }

    }

    public void CheckSolution()
    {
        bool tempCheck = true;
        foreach (OnTable onTable in onTables)
        {
            if (!onTable.correct)
            {
                tempCheck = false;
            }
        }
        if (tempCheck)
        {
            print("CLEAR!");
        }
    }
}
