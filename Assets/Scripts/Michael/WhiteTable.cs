using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteTable : MonoBehaviour
{
    public GameObject key;
    Material mat;

    float val;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (key == null)
        {
            val += 0.01F;
            val = Mathf.Clamp(val, 0, 1);
            if (mat != null)
                mat.SetFloat("_DissolveValue", val);
            if (val >= 1)
                gameObject.SetActive(false);
        }
    }
}
