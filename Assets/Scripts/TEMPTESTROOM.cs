using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMPTESTROOM : RoomVariables
{
    
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            print(Fail());
        }
    }
}
