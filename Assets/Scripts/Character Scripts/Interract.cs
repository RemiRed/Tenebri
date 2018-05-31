using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interract : MonoBehaviour
{
    //By Andreas Halldin
    //Handles Interracting with objects
    [SerializeField]
    GameObject interractionMessage;
    [SerializeField]
    [Range(0.001f, int.MaxValue)]
    float maxDistance; //The distance between the player and the object

    GameObject lastInterractedObject;
    bool oldInterraction = false;
    bool keyUp = true;

    private void Start() //find interractionMessage
    {
        interractionMessage = GameObject.FindGameObjectWithTag("InterractionMessage");
        interractionMessage.SetActive(false);
    }

    void FixedUpdate()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;

        Ray ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance))
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

        if (Input.GetAxisRaw("Interract") == 1) //Checks if the key has been pressed interracts with an object
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
        if (Physics.Raycast(ray, out hit, maxDistance)) //Finds an object that's within carry distance 
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