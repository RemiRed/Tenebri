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
    [SerializeField]
    RoomLoader roomLoader;
    // Update is called once per frame
    void Update()
    {
        //Testing REMOVE THIS BEFORE RELEASE!!!!!!!!!!!!
        if (Input.GetKeyDown(KeyCode.Alpha1)) //ColorSymbolsSolve
        {
            colorSymbolsP1.OpenDoorToNextLevel();
            colorSymbolsP2.OpenDoorToNextLevel();
            roomLoader.LoadRoom(RoomLoader.Room.roundMaze, 1);
            roomLoader.LoadRoom(RoomLoader.Room.roundMaze, 2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) //RoundMazeSolve
        {
            roundMazeP1.OpenDoorToNextLevel();
            roundMazeP2.OpenDoorToNextLevel();
            roomLoader.LoadRoom(RoomLoader.Room.outdoorMaze, 1);
            roomLoader.LoadRoom(RoomLoader.Room.outdoorMaze, 2);
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
