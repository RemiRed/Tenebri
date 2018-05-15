using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//Handles the symbols on the walls in the map room

public class RoundRoomManager : NetworkBehaviour {

	//Lists contining all symbols gameObject/location, color & symbol material
	public List<GameObject> wallSymbols = new List<GameObject>();	//
	public List<Color> symbolColors = new List<Color>();
	[SerializeField]
	List<Material> symbols = new List<Material>();

	//Temporary lists used to create & store randomized symbol-color combinations
	List<int> symbolOrder = new List<int> (), colorOrder = new List<int>(); 
	List<Material> _symbols; //<-- Might not be needed. (Then just use the original 'Symbols' list instead) 

	bool allSymbolsSet = false;

	void Start(){

		if(isServer){

			_symbols = new List<Material> (symbols);

			//			foreach(Material _symbol in symbols){
			//
			//				_symbols.Add(_symbol);
			//			}

			//Generates and saves a random order to assign symbol-color combinations to be applied on each client
			for (int i = 0; i < wallSymbols.Count; i++) {

				int _randomSymbol = Random.Range (0, _symbols.Count);
				int _randomColor = Random.Range (0, symbolColors.Count);

				symbolOrder.Add (_randomSymbol);
				colorOrder.Add (_randomColor);

				_symbols.RemoveAt (_randomSymbol);
			}
		}
	}
	//Applies each saved symbol-color combination on both clients 
	public void GetWallSymbols(){

		//Goes through all wall symbol objects and applies the stored combination values
		for (int i = 0; i < wallSymbols.Count; i++) {

			RpcSetWallSymbols (i, symbolOrder [i], colorOrder [i]);
		}
	}
	[ClientRpc]
	void RpcSetWallSymbols(int _index, int _randomSymbol, int _randomColor)
	{
		if (!allSymbolsSet) {

			//Assigns the same randomized color-symbol combination on both clients
			wallSymbols [_index].GetComponent<Renderer> ().material = symbols [_randomSymbol];
			wallSymbols [_index].GetComponent<Renderer> ().material.color = symbolColors [_randomColor];
			symbols.RemoveAt (_randomSymbol);

			// Makes sure wall symbols only can be assigned once
			if (_index == wallSymbols.Count - 1) {
				allSymbolsSet = true;
			}
		}
	}
}