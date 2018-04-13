using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class inherited by different puzzles success condition scripts
public class SuccessCondition : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public virtual void CompleteSuccess(){

	}

	public virtual void PartialSuccess(){

	}

	public virtual void Failure(){

	}

	public void OpenDoorToNextLevel(){

		 
		Debug.Log ("Door to next level should now be open");
	}
}