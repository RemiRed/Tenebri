using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//Handles the randomization of RoundMaze's password 

public class RoundRoomWalls : RoomVariables
{
    public Password passwordManager;

    [SerializeField]
    List<GameObject> walls = new List<GameObject>();
    [SerializeField]
    List<RoundDoors> buttons = new List<RoundDoors>();
    List<RoundDoors> usedButtons = new List<RoundDoors>();

    List<int> mazeSymbolMaterialIndex = new List<int>();
    public List<int> usedCorrectSymbolMaterialIndex = new List<int>();
    public List<int> theseButtonsIndex = new List<int>();

    public Material defaultButtonMaterial;

    [SerializeField]
    int numberOfButtons, curButtonNumber, numberOfPasswordButtons, materialIndex;

    bool foundPath = true;

    public void RandomSymbols()
    {
        //Resets to default
        RpcCloseWalls(false);
        CloseWalls(false);

        curButtonNumber = 0;
        int tempLayer = 0;
        int _curSymbolIndex = 0;
        bool firstLayer = false;

        //Creates list of references to the Index of maze symbol materials to be used
        foreach (GameObject _symbol in pairedRoom.GetComponent<RoundRoomManager>().wallSymbols)
        {
            if (!usedCorrectSymbolMaterialIndex.Contains(_curSymbolIndex))
            {

                mazeSymbolMaterialIndex.Add(_curSymbolIndex);
            }
            _curSymbolIndex++;
        }
        //Sets Maze buttons
        for (int i = 0; i < numberOfButtons; i++)
        {
            int _n = 0;
            //Generates list of possible buttons to be selected
            List<RoundDoors> tempButtons = new List<RoundDoors>();
            foreach (RoundDoors _button in buttons)
            {
                _button.buttonNumber = _n;
                _n++;
                //Conditions for buttons to be added to list of possible buttons to be selected
                if (!usedButtons.Contains(_button) &&
                    (firstLayer == false || (firstLayer == true && _button.layer != 1)) &&
                    _button.layer != tempLayer)
                {
                    tempButtons.Add(_button);
                }
            }
            //Generates random values for button variables
            int randomButtonInt = Random.Range(0, tempButtons.Count);
            int randomSymbol = Random.Range(0, mazeSymbolMaterialIndex.Count);
            //Calls Client to Find path from these button variables
            RpcFindPath(tempButtons[randomButtonInt].gameObject, true, mazeSymbolMaterialIndex[randomSymbol]);
            //Adjusts varables for next loop
            mazeSymbolMaterialIndex.RemoveAt(randomSymbol);
            tempLayer = tempButtons[randomButtonInt].layer;
            if (tempLayer == 1) firstLayer = true;
            usedButtons.Add(tempButtons[randomButtonInt]);
        }
        //Opens the rooms that has not been entered
        foreach (RoundDoors _button in buttons)
        {
            RpcFindPath(_button.gameObject, false, 0);
        }
    }

    [ClientRpc]
    void RpcFindPath(GameObject _button, bool _ifButton, int _randomSymbol)
    {
        if (_ifButton)
        {

            curButtonNumber++;
            _button.tag = "Interractable";
            //Sets first button to be the correct matching button
            if (curButtonNumber <= numberOfPasswordButtons)
            {

                _button.GetComponent<PasswordButton>().SetPasswordButton(1, pairedRoom.GetComponent<RoundRoomManager>().wallSymbols[_randomSymbol].GetComponent<Renderer>().material);
                usedCorrectSymbolMaterialIndex.Add(_randomSymbol);
                pairedRoom.GetComponent<RoundMazeMapRoom>().mapColours.Add(pairedRoom.GetComponent<RoundRoomManager>().wallSymbols[_randomSymbol].GetComponent<Renderer>().material.color);

            }
            else
            {

                _button.GetComponent<PasswordButton>().SetPasswordButton(0, pairedRoom.GetComponent<RoundRoomManager>().wallSymbols[_randomSymbol].GetComponent<Renderer>().material);
                //Changes color to a non-matching color
                List<Color> _symbolColors = new List<Color>();
                foreach (Color _color in pairedRoom.GetComponent<RoundRoomManager>().symbolColors)
                {

                    if (_color != _button.GetComponent<RoundDoors>().graphicalObject.GetComponent<Renderer>().materials[materialIndex].color)
                    {

                        _symbolColors.Add(_color);

                    }
                }
                Material[] _materials = _button.GetComponent<RoundDoors>().graphicalObject.GetComponent<Renderer>().materials;
                int rara = Random.Range(0, _symbolColors.Count);
                _materials[materialIndex].color = _symbolColors[rara];
                pairedRoom.GetComponent<RoundMazeMapRoom>().mapColours.Add(_symbolColors[rara]);
                _button.GetComponent<RoundDoors>().graphicalObject.GetComponent<Renderer>().materials = _materials;
            }

            //Adds button locations to map room
            theseButtonsIndex.Add(_button.GetComponent<RoundDoors>().buttonNumber);

            //Looks for & opens path. If path fails, marks as 'False' to be Re-Randomized 
            if (!_button.GetComponent<RoundDoors>().FindPath(_button.GetComponent<RoundDoors>()))
            {
                foundPath = false;
            }
            //If Path failed; Re-Randomize everything
            if (curButtonNumber >= numberOfButtons && !foundPath)
            {
                //                RandomizeEverything(true);
                //				//reRandomNow = true;
                playercommand.CmdReRandomRoundMazePuzzle();
            }
        }
        else if (!_button.GetComponent<RoundDoors>().entered)   //Opens remaining unopened rooms
        {
            _button.GetComponent<RoundDoors>().FindPath(_button.GetComponent<RoundDoors>());
            _button.GetComponent<PasswordButton>().SetPasswordButton(false);
        }
    }

    //Starts over if fails to find a working path
    [ClientRpc]
    public void RpcRandomizeEverything()
    {
        //		if(_reRandomNow == true){
        //			Debug.Log ("RERANDOMIZE EVERYTHING!");
        //			Debug.Log (isServer);
        //			if (isServer) {
        //				Debug.Log ("SHOULD BE RERANDOMIZED NOW");
        RandomSymbols();
        pairedRoom.GetComponent<RoundMazeMapRoom>().RpcMapButtons();
        //			}
        //			reRandomNow = false;
        //		}
    }
    //Resets Everything to default
    [ClientRpc]
    void RpcCloseWalls(bool _walls)
    {
        Debug.Log("Close wall on client from Rpc. Set: " + _walls);
        CloseWalls(_walls);
    }

    public void CloseWalls(bool _walls)
    {
        curButtonNumber = 0;
        Debug.Log("Close walls locally");
        foreach (GameObject bwa in walls)
        {
            Debug.Log("Animation should be called");
            //bwa.SetActive (_walls);
            bwa.GetComponent<Animator>().SetBool("OpenSesamy", _walls);
        }

        foreach (RoundDoors bwu in buttons)
        {
            bwu.entered = false;
            bwu.enteredNow = false;
            Material[] _materials = bwu.graphicalObject.GetComponent<Renderer>().materials;
            _materials[materialIndex] = defaultButtonMaterial;
            bwu.graphicalObject.GetComponent<Renderer>().materials = _materials;
            bwu.tag = "Untagged";
        }
        usedButtons.Clear();
        mazeSymbolMaterialIndex.Clear();
        pairedRoom.GetComponent<RoundMazeMapRoom>().mapColours.Clear();
        theseButtonsIndex.Clear();
        foundPath = true;
    }

    public override void PartialSuccess(PlayerCommands playerCmd)
    {
        Debug.Log("YOU WON!");
        playerCmd.CmdReRandomRoundMazePuzzle();
    }

    public override void CompleteSuccess(PlayerCommands playerCmd)
    {
        playerCmd.CmdRoundMazeCompleteSuccess();
    }

    public override void Failure(PlayerCommands playerCmd)
    {

        if (isServer)
        {
            RpcFailure();
        }
        else
        {
            playerCmd.CmdRoundMazeFailure();
        }


        playerCmd.CmdRoundMazeFailure();

    }
    [ClientRpc]
    public void RpcCompleteSuccess()
    {
        Debug.LogWarning("You passed this Puzzle");
        roomPassed = true;
        CloseWalls(true);
        roomLoader.LoadRoom(RoomLoader.Room.outdoorMaze, 1);
        roomLoader.LoadRoom(RoomLoader.Room.outdoorMaze, 2);
        OpenDoorToNextLevel();
    }

    [ClientRpc]
    public void RpcFailure()
    {

        Debug.Log("Fail");
        CloseWalls(true);
        usedCorrectSymbolMaterialIndex.Clear();
        pairedRoom.GetComponent<RoundMazeMapRoom>().RpcResetMap();
        GetComponentInChildren<RoundRoomCenter>().activatePuzzle = true;
     pairedRoom.Fail();
    }
}