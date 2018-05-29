using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interractable : MonoBehaviour
{
    [SerializeField]
	string interractMethodName = "", stopInterractMethodName = "";
    [SerializeField]
    protected float delay;

    public PlayerCommands playerCmd;
    [SerializeField]
    protected RoomLoader roomLoader;

    bool audioCheck = false;

    public void Interract() //Calls the interractable objects method that happens when it's interracted with after a delay
    {
        if (GetComponent<AudioSource>() && !audioCheck)
        {
            audioCheck = true;
            GetComponent<AudioSource>().PlayDelayed(delay);
        }
        Invoke(interractMethodName, delay);
    }

    public void StopInterract()
    {
        audioCheck = false;
        if (stopInterractMethodName != null && stopInterractMethodName != "")
        {
            Invoke(stopInterractMethodName, delay);
        }
    }
}
