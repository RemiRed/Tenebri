using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPlane : MonoBehaviour
{
    private void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            SceneManager.LoadScene(2);
        }
    }
    
}
