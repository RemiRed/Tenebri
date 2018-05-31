using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ButtonStartRoom : Interractable
{
    //By Andreas Halldin
    //Button in the start room(s)
    bool activated = false; //bool to check if button has been pressed

    [SerializeField]
    UnloadRooms p1, p2;
    
    void Press() //Called when the button is pressed
    {
        if (activated || !p1.entered || !p2.entered) //Checks that both rooms have a player in them, aswell as checking that this is the first time the button has been pressed
        {
            return;
        }
        activated = true; //Button has been pressed
        GetComponentInParent<RoomVariables>().OpenDoorToNextLevel(); //Open the door to the corridor
        roomLoader.LoadRoom(Room.colorSymbols, 1); //Load next room for both players
        roomLoader.LoadRoom(Room.colorSymbols, 2);
    }
}
