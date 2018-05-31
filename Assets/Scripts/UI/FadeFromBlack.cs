using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeFromBlack : MonoBehaviour
{
    //By Andreas Halldin
    //Handles the fade from black at the start of the game

    [SerializeField]
    Image instructions, black; //The images to fade

    [SerializeField]
    float fadeSpeed = .1f; //The speed of the fade, 


    public IEnumerator Fade() //Fade the images
    {
        while (black.color.a > 0) //Fade the black background
        {
            Color temp = black.color;
            temp.a -= 1 / 255f;
            temp.r += .5f / 255f;
            temp.g += .5f / 255f;
            temp.b += .5f / 255f;
            black.color = temp;
            yield return new WaitForSecondsRealtime(fadeSpeed);
        }
        black.gameObject.SetActive(false);
        while (instructions.color.a > 0) //Fade the instructional text
        {
            Color temp = instructions.color;
            temp.a -= 1 / 255f;
            instructions.color = temp;
            yield return new WaitForSecondsRealtime(fadeSpeed);
        }
        instructions.gameObject.SetActive(false);
    }

}
