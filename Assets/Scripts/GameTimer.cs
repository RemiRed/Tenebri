using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour {

  public void TotalTime()
  {
    //Change to UI instead of print
    print(Mathf.FloorToInt(Time.timeSinceLevelLoad / 60) + ":" + Mathf.FloorToInt(Time.timeSinceLevelLoad % 60));
  }
}
