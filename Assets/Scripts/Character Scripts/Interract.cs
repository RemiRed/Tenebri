using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interract : MonoBehaviour
{
    [SerializeField]
    GameObject interractionMessage;
    [SerializeField]
    [Range(0.001f, int.MaxValue)]
    float carryDistance; //The distance between the player and the carried object
    [SerializeField]
    [Range(0.001f, int.MaxValue)]
    float carrySpeed; //The speed the object travels to "catch up" to a player after being stuck
    [SerializeField]
    [Range(0.001f, int.MaxValue)]
    float slow; //Modifier for the force added to the object to simulate throwing

    GameObject carriedObject, lastInterractedObject;
    Transform carriedObjectParent;
    bool oldInterraction = false;
    bool keyUp = true;

    Vector3 oldPos; //Old position, used to calculate the force to emulate throwing
    Quaternion oldRot; //Old Rotation, used to stop the object from rotating

    float defaultDrag;

    private void Start() //saving the drag on the player component
    {
        interractionMessage = GameObject.FindGameObjectWithTag("InterractionMessage");
        interractionMessage.SetActive(false);
        defaultDrag = transform.parent.GetComponent<Rigidbody>().drag;
    }

    void FixedUpdate()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;

        Ray ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, carryDistance))
        { //Finds an object that's within carry distance 

            if (hit.transform.tag == "Interractable")
            { //If the object is interractable, show the interract message
                if (interractionMessage != null)
                {
                    interractionMessage.SetActive(true);
                }
            }
            else if (interractionMessage != null) //Reset interractionMessage
            {
                interractionMessage.SetActive(false);
            }
        }
        else if (interractionMessage != null) //Reset interractionMessage
        {
            interractionMessage.SetActive(false);
        }

        if (Input.GetAxisRaw("Interract") == 1) //Checks if the key has been pressed and picks up, interracts, or drops an object
        {
            if (keyUp)
            {
                keyUp = false;
            }
            oldInterraction = Interraction();
        }
        else if (Input.GetAxisRaw("Interract") == 0) //checks for a key release and allows the player to press the button again
        {
            oldInterraction = false;
            keyUp = true;
        }

        if (lastInterractedObject != null && !oldInterraction)
        {
            lastInterractedObject.GetComponent<Interractable>().StopInterract();
            lastInterractedObject = null;
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
}