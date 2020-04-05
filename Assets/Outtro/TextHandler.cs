using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHandler : MonoBehaviour
{
    public NumberAnimator number;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn()
    {
        foreach(Transform t in transform)
        {
            FadeIn f = t.GetComponent<FadeIn>();
            if (f)
            {
                f.start = true;
            }
        }
    }
    public void FadeOut()
    {
        foreach (Transform t in transform)
        {
            FadeIn f = t.GetComponent<FadeIn>();
            if (f)
            {
                f.fadeOut = true;
            }
        }
    }

    public void StartNumber()
    {
        number.start = true;
    }
}
