using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorCam : MonoBehaviour
{ Camera cam;
    public Rect thisRect;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("l")) { ScissorRect.SetScissorRect(cam, thisRect); }
    }
}
