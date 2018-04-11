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

	public List<Material> _symbols = new List<Material> ();

	void Start(){

		if(isServer){

			foreach(Material _symbol in symbols){

				_symbols.Add(_symbol);
			}

			for (int i = 0; i < wallSymbols.Count; i++) {

				int _randomSymbol = Random.Range (0, (_symbols.Count));
				int _randomColor = Random.Range (0, symbolColors.Count);

				RpcSetSymbols(i,_randomSymbol,_randomColor);

				_symbols.RemoveAt (_randomSymbol);
			}
		}
	}

    [ClientRpc]
	void RpcSetSymbols(int _index, int _randomSymbol, int _randomColor)
	{
		wallSymbols [_index].GetComponent<Renderer> ().material = symbols[_randomSymbol];
		wallSymbols [_index].GetComponent<Renderer> ().material.color = symbolColors[_randomColor];
		symbols.RemoveAt (_randomSymbol);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}