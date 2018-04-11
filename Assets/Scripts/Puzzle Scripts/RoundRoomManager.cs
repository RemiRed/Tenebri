using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RoundRoomManager : NetworkBehaviour {

	[SerializeField]
	public List<GameObject> wallSymbols = new List<GameObject>();
	[SerializeField]
	List<Color> symbolColors = new List<Color>();
	[SerializeField]
	List<Material> symbols = new List<Material>();

	[SerializeField]
	List<int> symbolOrder = new List<int> (), colorOrder = new List<int>(); 

	public List<Material> _symbols = new List<Material> ();

	void Start(){

		if(isServer){

			foreach(Material _symbol in symbols){

				_symbols.Add(_symbol);
			}

			for (int i = 0; i < wallSymbols.Count; i++) {

				int _randomSymbol = Random.Range (0, (_symbols.Count));
				int _randomColor = Random.Range (0, symbolColors.Count);

				symbolOrder.Add (_randomSymbol);
				colorOrder.Add (_randomColor);

				_symbols.RemoveAt (_randomSymbol);
			}
		}
	}

    [ClientRpc]
	void RpcSetWallSymbols(int _index, int _randomSymbol, int _randomColor)
	{
		wallSymbols [_index].GetComponent<Renderer> ().material = symbols[_randomSymbol];
		wallSymbols [_index].GetComponent<Renderer> ().material.color = symbolColors[_randomColor];
		symbols.RemoveAt (_randomSymbol);
    }

	[Command]
	public void CmdGetWallSymbols(){

		for (int i = 0; i < wallSymbols.Count; i++) {

			RpcSetWallSymbols (i, symbolOrder [i], colorOrder [i]);
		}
	}
		
	// Update is called once per frame
	void Update () {

//		if (Input.GetKeyDown (KeyCode.H)) {
//
//			Debug.Log ("Setting wall symbols");
//			CmdGetWallSymbols ();
//		}
	}
}