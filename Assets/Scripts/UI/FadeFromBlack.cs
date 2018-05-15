using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeFromBlack : MonoBehaviour
{

    [SerializeField]
    Image instructions, black;

    [SerializeField]
    float fadeSpeed = 1;


    public IEnumerator Fade()
    {
        print("FADECOROUTINE");
        while (black.color.a > 0)
        {
            Color temp = black.color;
            temp.a -= 1 / 255f;
            temp.r += .5f / 255f;
            temp.g += .5f / 255f;
            temp.b += .5f / 255f;
            black.color = temp;
            yield return new WaitForSecondsRealtime(fadeSpeed);
        }
        while (instructions.color.a > 0)
        {
            Color temp = instructions.color;
            temp.a -= 1 / 255f;
            instructions.color = temp;
            yield return new WaitForSecondsRealtime(fadeSpeed);
        }
    }

}
