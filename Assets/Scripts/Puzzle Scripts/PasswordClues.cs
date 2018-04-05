using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordClues : MonoBehaviour {

	public bool numbers; 

	public GameObject infoCube;
	public List<Transform> clueLocations;

	public List<Material> symbolNumbers;
	int curClueNumber = 0;

	public void SetClues(Material _symbolMaterial, Color _symbolColor){

		int _randomLocation = Random.Range (0, clueLocations.Count);
		GameObject _curClue = Instantiate (infoCube, clueLocations [_randomLocation].position, clueLocations [_randomLocation].rotation,gameObject.transform);

		if (numbers) {

			_curClue.GetComponent<Renderer> ().material = symbolNumbers [curClueNumber];
			curClueNumber++;

		} else {
			
			_curClue.GetComponent<Renderer> ().material = new Material (_symbolMaterial);
		}
		_curClue.GetComponent<Renderer> ().material.color = _symbolColor;
		clueLocations.RemoveAt (_randomLocation);
	}
}