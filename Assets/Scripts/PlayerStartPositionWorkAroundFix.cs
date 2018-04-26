using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerStartPositionWorkAroundFix : NetworkBehaviour {

	public Transform realP1StartLocation, realP2StartLocation;
	public  GameObject[] theTwoPlayers;
	public bool lookingForMorePlayers = true;
	
	// Update is called once per frame
	void Update () {

		if (isServer && lookingForMorePlayers) {

			Debug.Log (theTwoPlayers.Length);

			if (theTwoPlayers.Length <= 1) {

				theTwoPlayers = GameObject.FindGameObjectsWithTag ("Player");
				RpcDebug(theTwoPlayers.Length);

			
			} else {

				lookingForMorePlayers = false;
				RpcTeleportPlayer1ToTheCorrectStartLocation (theTwoPlayers [0]);
				RpcTeleportPlayer2ToTheCorrectStartLocation (theTwoPlayers [1]);
			}
		}
	}


	[ClientRpc]
	void RpcTeleportPlayer1ToTheCorrectStartLocation(GameObject _player){

		_player.transform.position = realP1StartLocation.transform.position;
	}
	[ClientRpc]
	void RpcTeleportPlayer2ToTheCorrectStartLocation(GameObject _player){

		_player.transform.position = realP2StartLocation.transform.position;
	}

	[ClientRpc]
	void RpcDebug(int _length){

		Debug.Log (_length);
	}
}
