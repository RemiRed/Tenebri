﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RoundRoomWalls : RoomVariables
{

    [SerializeField]
    List<GameObject> walls = new List<GameObject>();
    [SerializeField]
    List<GameObject> buttons = new List<GameObject>();
    List<RoundDoors> usedButtons = new List<RoundDoors>();
    [SerializeField]
    List<GameObject> symbols = new List<GameObject>();
    public List<int> theseButtons = new List<int>();
    List<GameObject> _symbols = new List<GameObject>();

    [SerializeField]
    int numberOfButtons, curButtonNumber = 0;

    bool foundPath = true;

    void Update()
    {
        //Debug Stuff
        if (isServer && Input.GetKeyDown(KeyCode.G))
        {
			pairedRoom.GetComponent<RoundRoomManager> ().CmdGetWallSymbols ();
            RandomSymbols();
            pairedRoom.GetComponent<RoundMazeMapRoom>().RpcMapButtons();
        }
    }

    void RandomSymbols()
    {
        RpcCloseWalls();
        CloseWalls();

        foreach (GameObject symbol in symbols)
        {
            _symbols.Add(symbol);
        }
        curButtonNumber = 0;
        int tempLayer = 0;
        bool firstLayer = false;

        for (int i = 0; i < numberOfButtons; i++)
        {
            List<RoundDoors> tempButtons = new List<RoundDoors>();
            foreach (GameObject _button in buttons)
            {
                //Conditions for buttons to be added to list of possible buttons to be selected
                if (!usedButtons.Contains(_button.GetComponent<RoundDoors>()) &&
                (firstLayer == false || (firstLayer == true && _button.GetComponent<RoundDoors>().layer != 1)) &&
                _button.GetComponent<RoundDoors>().layer != tempLayer)
                {
                    tempButtons.Add(_button.GetComponent<RoundDoors>());
                }
            }
            //Selects random button positions and opens a path from selected button
            int randomButtonInt = Random.Range(0, tempButtons.Count);

			int randomSymbol = Random.Range(0, pairedRoom.GetComponent<RoundRoomManager>().wallSymbols.Count);
			RpcFindPath(tempButtons[randomButtonInt].gameObject, true, randomSymbol);
            //Adjusts varables for next loop
            tempLayer = tempButtons[randomButtonInt].layer;
            if (tempLayer == 1) firstLayer = true;
            usedButtons.Add(tempButtons[randomButtonInt]);
        }
        //Opens the rooms that has not been entered
        foreach (GameObject _button in buttons)
        {
            RpcFindPath(_button, false,0);
        }
    }

    [ClientRpc]
	void RpcFindPath(GameObject _button, bool _ifButton, int _randomSymbol)
    {

        print(_symbols.Count);

        if (_ifButton)
        {

            curButtonNumber++;
               
			_button.GetComponent<Renderer>().material = pairedRoom.GetComponent<RoundRoomManager>().wallSymbols[_randomSymbol].GetComponent<Renderer>().material;

            theseButtons.Add(_button.GetComponent<RoundDoors>().buttonNumber);

            if (!_button.GetComponent<RoundDoors>().FindPath(_button.GetComponent<RoundDoors>()))
            {
                foundPath = false;
            }
            if (curButtonNumber >= numberOfButtons && !foundPath)
            {

                CmdReRandomizeEverything();
            }
        }
        else if (!_button.GetComponent<RoundDoors>().entered)
        {

            _button.GetComponent<RoundDoors>().FindPath(_button.GetComponent<RoundDoors>());
        }
    }

    [Command]
    void CmdReRandomizeEverything()
    {

        RandomSymbols();
        pairedRoom.GetComponent<RoundMazeMapRoom>().RpcMapButtons();
    }

    //Resets Everything to default // Twice becasue temporary solution to fix over network..
    [ClientRpc]
    void RpcCloseWalls()
    {
        CloseWalls();
    }
    void CloseWalls()
    {
        foreach (GameObject bwa in walls)
        {
            bwa.SetActive(true);
        }
        foreach (GameObject bwu in buttons)
        {
            bwu.GetComponent<Renderer>().material.color = Color.gray;
            bwu.GetComponent<RoundDoors>().entered = false;
            bwu.GetComponent<RoundDoors>().enteredNow = false;
        }
        usedButtons.Clear();
        theseButtons.Clear();
        foundPath = true;
    }
}