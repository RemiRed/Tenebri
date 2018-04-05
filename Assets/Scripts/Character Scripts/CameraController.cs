using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraController : NetworkBehaviour
{

    Vector2 mouseLook;
    Vector2 smoothV;
    [SerializeField]
    float sensitivity;
    [SerializeField]
    float smoothing;
    [SerializeField]
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
            return;
        }

        var md = new Vector2(Input.GetAxisRaw("Horizontal Camera"), Input.GetAxisRaw("Vertical Camera"));

        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;

        transform.localRotation = Quaternion.AngleAxis(Mathf.Clamp(-mouseLook.y, -visualAngleLimiter, visualAngleLimiter), Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);


        if (mouseLook.y < -visualAngleLimiter)
            mouseLook.y = -visualAngleLimiter;
        if (mouseLook.y > visualAngleLimiter)
            mouseLook.y = visualAngleLimiter;
    }

}
