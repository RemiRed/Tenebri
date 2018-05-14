using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTING : MonoBehaviour
{
    //TESTING ONLY REMOVE THIS BEFORE RELEASE
    [SerializeField]
    RoomVariables colorSymbolsP1, colorSymbolsP2, roundMazeP1, roundMazeP2;
    [SerializeField]
    List<Interractable> corridorLevers;

    // Update is called once per frame
    void Update()
    {
        //Testing REMOVE THIS BEFORE RELEASE!!!!!!!!!!!!
        if (Input.GetKeyDown(KeyCode.Alpha1)) //ColorSymbolsSolve
        {
            colorSymbolsP1.OpenDoorToNextLevel();
            colorSymbolsP2.OpenDoorToNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) //RoundMazeSolve
        {
            roundMazeP1.OpenDoorToNextLevel();
            roundMazeP2.OpenDoorToNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.Tab)) //OpenCorridors
        {
            foreach (Interractable lever in corridorLevers)
            {
                lever.Interract();
            }
        }
    }
}
