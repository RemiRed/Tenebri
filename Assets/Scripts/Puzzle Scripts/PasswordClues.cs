using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordClues : MonoBehaviour {

	public bool numbers; 

	public GameObject infoCube;

//	public List<Color> symbolColors;
//	//public List<int> symbolNumbers;
//	public List<GameObject> symbols;

//	public List<Material> symbolMaterial;

	public List<Material> symbolNumbers;

	public int curClueNumber = 0;

	public List<Transform> clueLocations;

	// Use this for initialization
	void Start () {


		
	}

	public void SetClues(Material _symbolMaterial, Color _symbolColor){

		int _randomLocation = Random.Range (0, clueLocations.Count);
		GameObject _curClue = Instantiate (infoCube, clueLocations [_randomLocation].position, clueLocations [_randomLocation].rotation,gameObject.transform);

		if (numbers) {

			_curClue.GetComponent<Renderer> ().material = symbolNumbers [curClueNumber];
			_curClue.GetComponent<Renderer> ().material.color = _symbolColor;
			_curClue.name = curClueNumber.ToString ();
			curClueNumber++;

		} else {
			
			_curClue.GetComponent<Renderer> ().material = new Material (_symbolMaterial);
			_curClue.GetComponent<Renderer> ().material.color = _symbolColor;
		}
		clueLocations.RemoveAt (_randomLocation);
	}
}
