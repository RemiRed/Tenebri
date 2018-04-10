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

	void Start(){

        Debug.Log("Awake?");

		if(isServer){

            Debug.Log(isServer);

			for (int i = 0; i < wallSymbols.Count; i++) {

				int _randomSymbol = Random.Range (0, symbols.Count - i);
				int _randomColor = Random.Range (0, symbolColors.Count - i);

                Debug.Log("In Loop");

                RpcSetSymbols(i,_randomSymbol,_randomColor);

				symbols.RemoveAt (_randomSymbol);
			}
		}
	}

    [ClientRpc]
    void RpcSetSymbols(int _index,int _randomSymbol, int _randomColor)
    {
        Debug.Log("Client Call");
        wallSymbols[_index].GetComponent<Renderer>().material = symbols[_randomSymbol];
        wallSymbols[_index].GetComponent<Renderer>().material.color = symbolColors[_randomColor];
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}