using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInAudio : MonoBehaviour
{
    AudioSource AS;
    // Start is called before the first frame update
    void Awake()
    {
        AS = GetComponent<AudioSource>();
        AS.volume = 0;
    }


    private void OnEnable()
    {
        FadeIn();
    }

    public void FadeIn() { StartCoroutine("FadeInStart"); }

    public void FadeOut() { StartCoroutine("FadeOutStart"); }
    IEnumerator FadeInStart() {
        while (AS.volume < 1) {
            AS.volume += 0.01F;

            yield return new WaitForEndOfFrame();
        }
       
    }
    IEnumerator FadeOutStart()
    {
        while (AS.volume > 0)
        {
            AS.volume -= 0.01F;

            yield return new WaitForEndOfFrame();
        }

    }

}
