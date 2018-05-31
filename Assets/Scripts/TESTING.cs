using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTING : MonoBehaviour
{
    //By Andreas Halldin

    //TESTING ONLY REMOVE THIS BEFORE RELEASE
    [SerializeField]
    RoomVariables colorSymbolsP1, colorSymbolsP2, roundMazeP1, roundMazeP2;
    [SerializeField]
    List<Interractable> corridorLevers;
    [SerializeField]
    RoomLoader roomLoader;

    void Update()
    {
        //Testing REMOVE THIS BEFORE RELEASE

        if (Input.GetKeyDown(KeyCode.Alpha1)) //ColorSymbolsSolve
        {
            colorSymbolsP1.OpenDoorToNextLevel();
            colorSymbolsP2.OpenDoorToNextLevel();
            roomLoader.LoadRoom(Room.roundMaze, 1);
            roomLoader.LoadRoom(Room.roundMaze, 2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) //RoundMazeSolve
        {
            roundMazeP1.OpenDoorToNextLevel();
            roundMazeP2.OpenDoorToNextLevel();
            roomLoader.LoadRoom(Room.outdoorMaze, 1);
            roomLoader.LoadRoom(Room.outdoorMaze, 2);
        }
        if (Input.GetKey(KeyCode.Tab)) //OpenCorridors
        {
            foreach (Interractable lever in corridorLevers)
            {
                lever.playerCmd.CmdCorridorLever();
            }
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            foreach (Interractable lever in corridorLevers)
            {
                lever.playerCmd.CmdCorridorLeverRelease();
            }
        }
    }
}
