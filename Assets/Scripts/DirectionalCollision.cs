using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	
public class DirectionalCollision : MonoBehaviour {

	public bool IsGrounded(){

		return DetectDirectionalCollision(Vector3.down, true, gameObject);
	}
	/// Detects collision in Vector3 direction _rayDirection
	public bool DetectDirectionalCollision(Vector3 _direction){

		return DetectDirectionalCollision(_direction, true, gameObject);
	}
	/// Detections collision in Vector3 direction _rayDirection and changed bool _variable to True or False depending on if a collision is detected or not
	protected bool DetectDirectionalCollision(Vector3 _direction, bool _variable)
	{
		return DetectDirectionalCollision(_direction,_variable, gameObject);
	}
	public bool DetectDirectionalCollision(Vector3 _direction, GameObject _source){

		return DetectDirectionalCollision(_direction, true, _source);
	}

	public bool DetectDirectionalCollision(Vector3 _direction, bool _variable, GameObject _source)
	{
		//Runs Raycasts to detect if there's a collision or not in given direction. Uses a set of raycasts for more accurate edge detection.
		if (Physics.Raycast(_source.transform.position + new Vector3(0, 0, _source.GetComponent<Collider>().bounds.extents.x / 1.5f), _direction, _source.GetComponent<Collider>().bounds.extents.y + 0.05f))
		{
			_variable = true;
		}
		else if (Physics.Raycast(_source.transform.position + new Vector3(0, 0, -_source.GetComponent<Collider>().bounds.extents.x / 1.5f), _direction,  _source.GetComponent<Collider>().bounds.extents.y + 0.05f))
		{
			_variable = true;
		}
		else if (Physics.Raycast(_source.transform.position + new Vector3(_source.GetComponent<Collider>().bounds.extents.x / 1.5f, 0, 0), _direction, _source.GetComponent<Collider>().bounds.extents.y + 0.05f))
		{
			_variable = true;
		}
		else if (Physics.Raycast(_source.transform.position + new Vector3(-_source.GetComponent<Collider>().bounds.extents.x / 1.5f, 0, 0), _direction, _source.GetComponent<Collider>().bounds.extents.y + 0.05f))
		{
			_variable = true;
		}
		else if (Physics.Raycast(_source.transform.position + new Vector3(-_source.GetComponent<Collider>().bounds.extents.x / 1.5f, 0, 0), _direction, _source.GetComponent<Collider>().bounds.extents.y + 0.05f))
		{
			_variable = true;
		}
		else
		{
			_variable = false;
		}
		return _variable;
	}

	public static bool testing(){

		return true;

	}
}