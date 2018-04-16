using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundRoomMapButtonnumber : MonoBehaviour {
//Questionable script...

    public int buttonNumber;
    public string colour;
    
    
    void ChangeMaterial()
    {
        switch (colour)
        {
            case "blue":
                print("hello");
                break;
        }
    }
}