using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberAnimator : MonoBehaviour
{
    public Text txt;
    public int displayNumber = 0;
    public float startNumber, endNumber;
    public float animationSpeed = 1f;
    float t;
    public bool start = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = displayNumber.ToString();
        if (start)
        {
            t += animationSpeed * Time.deltaTime;
            t = Mathf.Clamp01(t);
            displayNumber = Mathf.CeilToInt(Mathf.Lerp(startNumber, endNumber, t));
        }
    }
}
