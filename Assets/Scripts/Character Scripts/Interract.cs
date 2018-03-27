using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interract : MonoBehaviour
{
    [SerializeField]
    [Range(0.001f, int.MaxValue)]
    float carryDistance; //The distance between the player and the carried object
    [SerializeField]
    [Range(0.001f, int.MaxValue)]
    float carrySpeed; //The speed the object travels to "catch up" to a player after being stuck
    [SerializeField]
    [Range(0.001f, int.MaxValue)]
    float slow; //Modifier for the force added to the object to simulate throwing

    GameObject carriedObject;
    Transform carriedObjectParent;
    GameObject lastInterractedObject;
    bool oldInterraction = false;
    bool keyUp = true;
    bool carrying = false;

    Vector3 oldPos; //Old position, used to calculate the force to emulate throwing
    Quaternion oldRot; //Old Rotation, used to stop the object from rotating

    float defaultDrag;
    private void Start() //saving the drag on the player component
    {
        defaultDrag = transform.parent.GetComponent<Rigidbody>().drag;
    }

    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Interract") == 1) //Checks if the key has been pressed and picks up, interracts, or drops an object
        {
            if (keyUp)
            {
                keyUp = false;
                if (carrying)
                {
                    Drop();
                }
                else
                {
                    Pickup();
                }
            }
            oldInterraction = Interraction();
        }
        else if (Input.GetAxisRaw("Interract") == 0) //checks for a key release and allows the player to press the button again
        {
            oldInterraction = false;
            keyUp = true;
        }

        if (carrying && carriedObject != null) //Returns the object close to the player should it get stuck
        {
            carriedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            carriedObject.transform.localRotation = oldRot;
            oldPos = carriedObject.transform.position = Vector3.Lerp(carriedObject.transform.position, transform.position + transform.forward * carryDistance, Time.deltaTime * carrySpeed);
        }

        if (lastInterractedObject != null && oldInterraction)
        {
            lastInterractedObject.GetComponent<Interractable>().StopInterract();
        }
    }
		
    void Pickup()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;

        Ray ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, carryDistance,8)) //Finds an object that's within carry distance 
        {

            if (hit.transform.tag == "Movable") //If the object is movable it starts moving the object around
            {
                carrying = true;
                carriedObject = hit.transform.gameObject;
                carriedObject.GetComponent<Rigidbody>().useGravity = false;
                carriedObject.GetComponent<Rigidbody>().freezeRotation = true;
                carriedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                carriedObjectParent = carriedObject.transform.parent;
                carriedObject.transform.parent = transform;
                oldRot = carriedObject.transform.localRotation;
                transform.parent.GetComponent<Rigidbody>().drag += carriedObject.GetComponent<Rigidbody>().mass;
            }
        }
    }

    bool Interraction()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;

        Ray ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, carryDistance)) //Finds an object that's within carry distance 
        {
            if (hit.transform.tag == "Interractable") //If the object is interractable, like a button, it'll interract with the object
            {
                lastInterractedObject = hit.transform.gameObject;
                lastInterractedObject.GetComponent<Interractable>().playerCmd = transform.parent.gameObject.GetComponent<PlayerCommands>();
                lastInterractedObject.GetComponent<Interractable>().Interract();
                return true;
            }
        }
        return false;
    }

    void Drop() //drops a held object
    {
        carrying = false;
        carriedObject.GetComponent<Rigidbody>().freezeRotation = false;
        carriedObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
        carriedObject.transform.parent = carriedObjectParent;
        transform.parent.GetComponent<Rigidbody>().drag = defaultDrag;
        carriedObject.GetComponent<Rigidbody>().AddForce((carriedObject.transform.position - oldPos) / (Time.deltaTime * slow));
        carriedObject = null;
    }
}