using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//Handles the symbols on the walls in the map room

public class RoundRoomManager : NetworkBehaviour {

	public List<GameObject> wallSymbols = new List<GameObject>();
	public List<Color> symbolColors = new List<Color>();
	[SerializeField]
	List<Material> symbols = new List<Material>();
	[SerializeField]
	List<int> symbolOrder = new List<int> (), colorOrder = new List<int>(); 

	List<Material> _symbols = new List<Material> ();

	bool symbolsSet = false;

	void Start(){

		if(isServer){

			foreach(Material _symbol in symbols){

				_symbols.Add(_symbol);
			}

			for (int i = 0; i < wallSymbols.Count; i++) {

				int _randomSymbol = Random.Range (0, _symbols.Count);
				int _randomColor = Random.Range (0, symbolColors.Count);

				symbolOrder.Add (_randomSymbol);
				colorOrder.Add (_randomColor);

				_symbols.RemoveAt (_randomSymbol);
			}
		}
	}

	public void GetWallSymbols(){

		for (int i = 0; i < wallSymbols.Count; i++) {

			RpcSetWallSymbols (i, symbolOrder [i], colorOrder [i]);
		}
	}

	[ClientRpc]
	void RpcSetWallSymbols(int _index, int _randomSymbol, int _randomColor)
	{
		if (!symbolsSet) {

			wallSymbols [_index].GetComponent<Renderer> ().material = symbols [_randomSymbol];
			wallSymbols [_index].GetComponent<Renderer> ().material.color = symbolColors [_randomColor];
			symbols.RemoveAt (_randomSymbol);

			if (_index == wallSymbols.Count - 1) {
				symbolsSet = true;
			}
		}
	}
}