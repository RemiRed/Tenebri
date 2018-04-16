using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraController : NetworkBehaviour
{

	public bool trippy;
    Vector2 mouseLook;
    Vector2 smoothV;
	[SerializeField][Range(1,10)]
    float sensitivity;
	[SerializeField][Range(1,10)]
    float smoothing;
	[SerializeField][Range(0,180)]
    float visualAngleLimiter = 80;

    GameObject character;

    void Start()
    {
        character = transform.parent.gameObject;
    }

    void Update()
    {
        if (character.GetComponent<CharacterScript>().menu)
        {
            character.GetComponent<Rigidbody>().freezeRotation = true;
            return;
        }
        

		var cameraView = new Vector2(Input.GetAxisRaw("Horizontal Camera"), Input.GetAxisRaw("Vertical Camera"));

        cameraView = Vector2.Scale(cameraView, new Vector2(sensitivity * 1.25f, sensitivity));
        smoothV.x = Mathf.Lerp(smoothV.x, cameraView.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, cameraView.y, 1f / smoothing);
        mouseLook += smoothV;

		mouseLook.y = Mathf.Clamp (mouseLook.y, -visualAngleLimiter, visualAngleLimiter);
		if (trippy) {
			mouseLook.y = 180;
		}
		transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
    }

}
