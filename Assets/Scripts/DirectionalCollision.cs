using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles collision (or proximity) in a single given direction and returns a bool value as True if collision (or proximity) is detected, else returns False.
//!<STILL WORK IN PROGRESS>!//
public class DirectionalCollision : MonoBehaviour {

	/// Returns 'True' if gameObject is grounded.  
	public bool IsGrounded(){

		return DetectDirectionalProximity(Vector3.down, true, gameObject, 0);
	}
	/// Detects collision in Vector3 direction _rayDirection
	public bool DetectDirectionalCollision(Vector3 _direction){

		return DetectDirectionalProximity(_direction, true, gameObject,0);
	}
	/// Detections collision in Vector3 direction _rayDirection and changed bool _variable to True if a collision is detected or False if no t
	protected bool DetectDirectionalCollision(Vector3 _direction, bool _variable)
	{
		return DetectDirectionalProximity(_direction,_variable, gameObject, 0);
	}

	public bool DetectDirectionalCollision(Vector3 _direction, GameObject _source){

		return DetectDirectionalProximity(_direction, true, _source, 0);
	}

	public bool DetectDirectionalProximity(Vector3 _direction, GameObject _source, float _distance){

		return DetectDirectionalProximity (_direction, true, _source, _distance);
	}

	bool DetectDirectionalProximity(Vector3 _direction, bool _variable, GameObject _source, float _distance)
	{
		//Runs Raycasts to detect if there's a collision or not in given direction. Uses a set of raycasts for more accurate edge detection.
		if (Physics.Raycast(_source.transform.position + new Vector3(0, 0, _source.GetComponent<Collider>().bounds.extents.x / 1.5f), _direction, _source.GetComponent<Collider>().bounds.extents.y + _distance + 0.05f)){
			_variable = true;
		}else if (Physics.Raycast(_source.transform.position + new Vector3(0, 0, -_source.GetComponent<Collider>().bounds.extents.x / 1.5f), _direction,  _source.GetComponent<Collider>().bounds.extents.y  + _distance +  0.05f)){
			_variable = true;
		}else if (Physics.Raycast(_source.transform.position + new Vector3(_source.GetComponent<Collider>().bounds.extents.x / 1.5f, 0, 0), _direction, _source.GetComponent<Collider>().bounds.extents.y  + _distance +  0.05f)){
			_variable = true;
		}else if (Physics.Raycast(_source.transform.position + new Vector3(-_source.GetComponent<Collider>().bounds.extents.x / 1.5f, 0, 0), _direction, _source.GetComponent<Collider>().bounds.extents.y  + _distance +  0.05f)){
			_variable = true;
		}else{
			_variable = false;
		}
		return _variable;
	}
}