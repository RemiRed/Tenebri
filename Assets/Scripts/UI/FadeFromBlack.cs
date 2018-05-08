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


    void Start()
    {
        print("STARTED");
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        print("FADECOROUTINE");
        while (black.color.a > 0)
        {
            print("LOOP1 " + black.color.a);
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
            print("LOOP2");
            Color temp = instructions.color;
            temp.a -= 1 / 255f;
            instructions.color = temp;
            yield return new WaitForSecondsRealtime(fadeSpeed);
        }
    }

}
