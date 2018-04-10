using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RoundRoomManager : NetworkBehaviour {

	[SerializeField]
	List<GameObject> wallSymbols = new List<GameObject>();
	[SerializeField]
	List<Color> symbolColors = new List<Color>();
	[SerializeField]
	List<Material> symbols = new List<Material>();

	void Awake(){

		if(isServer){

			for (int i = 0; i < wallSymbols.Count; i++) {

				int _randomSymbol = Random.Range (0, symbols.Count - i);
				int _randomColor = Random.Range (0, symbolColors.Count - i);
					
				wallSymbols [i].GetComponent<Renderer> ().material = symbols [_randomSymbol];
				wallSymbols [i].GetComponent<Renderer> ().material.color = symbolColors [_randomColor];

				symbols.RemoveAt (_randomSymbol);
			}
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}