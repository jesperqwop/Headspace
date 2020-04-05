using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingColorBlocks : MonoBehaviour
{
    public Vector2 topPos, botPos;
    public AnimationCurve curve;
    public float animationSpeed = 1f;
    float t, ct;
    public bool startedTop = false;
    public bool flip = false;
    RectTransform rt;

    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (flip)
        {
            t += animationSpeed * Time.deltaTime;
        }
        else
        {
            t -= animationSpeed * Time.deltaTime;
        }
        t = Mathf.Clamp01(t);
        ct = curve.Evaluate(t);
        float defaultVal = startedTop ? topPos.y : botPos.y;
        float flipVal = startedTop ? botPos.y : topPos.y;
        rt.anchoredPosition = new Vector2(0, Mathf.Lerp(defaultVal, flipVal, ct));
    }
}
