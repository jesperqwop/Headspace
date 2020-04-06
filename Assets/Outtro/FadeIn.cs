using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public Image img;
    public Text txt;
    public float fadeSpeed = 1f;
    public bool start = false;
    public bool fadeOut = false;
    public float t = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            t += fadeSpeed * Time.deltaTime;
            t = Mathf.Clamp01(t);
            if (img)
                img.color = new Color(img.color.r, img.color.g, img.color.b, t);
            else if (txt)
                txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, t);
        }
        if (fadeOut)
        {
            start = false;
            t -= fadeSpeed * 2 * Time.deltaTime;
            t = Mathf.Clamp01(t);
            if (img)
                img.color = new Color(img.color.r, img.color.g, img.color.b, t);
            else if (txt)
                txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, t);
        }
    }
}
