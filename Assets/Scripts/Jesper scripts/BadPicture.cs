using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BadPicture : MonoBehaviour
{

    public bool goneBad;
    public Material badVersion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BadMemoryHandler.instance.triggered)
        {
            GetComponent<Renderer>().material = badVersion;
            if(GetComponent<Interactable>()!= null)
            {
                transform.localScale = new Vector3(4, 0.75f, 4);
            }
        }
    }
}
