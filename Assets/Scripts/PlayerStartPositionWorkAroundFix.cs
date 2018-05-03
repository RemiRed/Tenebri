using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//Works around the issue with the players not spawning on their network starting positions correctly by teleporting them to their correct positions after the game loads.
public class PlayerStartPositionWorkAroundFix : NetworkBehaviour {

	public Transform realP1StartLocation, realP2StartLocation;	//The two correct start locations
	public  GameObject[] theTwoPlayers;	//An array of the two players
	public bool lookingForMorePlayers = true;
	
	// Update is called once per frame
	void Update () {

		//The server looks for the players
		if (isServer && lookingForMorePlayers) {

			//Keeps looking for more players is not all players are found
			if (theTwoPlayers.Length <= 1) {

				theTwoPlayers = GameObject.FindGameObjectsWithTag ("Player");
			
			} else {	//When all players are found the server stops looking for more players and they're teleporter to their respective correct starting location

				lookingForMorePlayers = false;
				RpcTeleportPlayer1ToTheCorrectStartLocation (theTwoPlayers [0], theTwoPlayers [1]);
				Destroy (gameObject, 7f);	//Destroy this object after it's task is completed
			}
		}
	}
		
	[ClientRpc]	//Teleports the player to their correct starting locations
	void RpcTeleportPlayer1ToTheCorrectStartLocation(GameObject _player1, GameObject _player2){

		_player1.transform.position = realP1StartLocation.transform.position;
		_player2.transform.position = realP2StartLocation.transform.position;
	}
}
//The Script could be made using loops and more variables to become more dynamic..
//..But this is a hotfix workaround and hopefully won't even be needed in the finished version. 
