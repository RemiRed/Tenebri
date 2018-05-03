using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//Placed on the player characters view. Controlls the chatacter's camera motion
public class CameraController : NetworkBehaviour {

	[SerializeField][Tooltip("Sets to the parent gameObject if left empty")]
	GameObject character;	//The camera view's parent character object

	[SerializeField][Range(1,10)]
    float sensitivity;	//Sets the camera motions sensitivity. Higher sensitivity = faster camera movements. 
	[SerializeField][Range(1,10)]
    float smoothing;	//Sets the camera motions smoothness. Higher smoothness = delayed camera movement. 
	[SerializeField][Range(0,180)]	
    float visualAngleLimiter = 80;	//Sets the extreme angle values that limits how much the player can look up and down. 

	Vector2 mouseLook, smoothV, cameraView;	//Vector2 values needed to calculate camera movement

	public bool trippy;	//Want a trippy camera effect? (Flips the camera backwards and upside-down, making the world appear upside-down and all motion reversed)

    void Start()
    {
		if (character == null) character = transform.parent.gameObject;
    }
    void Update()
    {
		//Stops camera movement if pause menu is active
        if (character.GetComponent<CharacterScript>().menu)
        {
            character.GetComponent<Rigidbody>().freezeRotation = true;
            return;
        }
        //Gets Camera View from cursor location
		cameraView = new Vector2(Input.GetAxisRaw("Horizontal Camera"), Input.GetAxisRaw("Vertical Camera"));
		//Updates camera view to incluse mouse sensistivity
		cameraView = Vector2.Scale(cameraView, new Vector2(sensitivity * 1.25f, sensitivity));
        //Adds smooth camera motion
		smoothV.x = Mathf.LerpAngle(smoothV.x, cameraView.x, 1f / smoothing);
        smoothV.y = Mathf.LerpAngle(smoothV.y, cameraView.y, 1f / smoothing);
		//Updates Mouse Look with smoothed Camera View
		mouseLook += smoothV;
		//Clamps Mouse Look angle between set extreme values
		mouseLook.y = Mathf.Clamp (mouseLook.y, -visualAngleLimiter, visualAngleLimiter);
		if (trippy) mouseLook.y = 180;	//Want a trippy camera effect?
		//Executes the smoothed camera view following the cursor movement
		transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
    }
}