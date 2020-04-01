using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float waterRiseSpeed = 100;

    public float minimumDissolveRange;
    public float maximumDissolveRange;

    public Camera mainCam;
    public Camera waterCam;

    [Range(0, 1)]
    public float camSlider = 0;
    Scissor scissor2;

    public Transform waterPlane;
    public Transform plane2;

    public Transform apoint;
    public float nearPlane;
    public Vector3 thing;

    float x, y, z;
    // Start is called before the first frame update
    void Start()
    {
        scissor2 = waterCam.GetComponent<Scissor>();

        z = mainCam.nearClipPlane;
        x = mainCam.fieldOfView / 2 / 100;
        y = x / (Screen.currentResolution.width / Screen.currentResolution.height);
    }

    // Update is called once per frame
    void Update()
    {
        if (apoint.position.y > (mainCam.transform.position.y + y))
        { camSlider = 1;

        }
        else
        {
            camSlider = mainCam.WorldToViewportPoint(new Vector3(mainCam.transform.position.x, apoint.position.y, mainCam.transform.position.z) + new Vector3(mainCam.transform.forward.x, 0, mainCam.transform.forward.z).normalized * nearPlane).y;
        }

        camSlider = Mathf.Clamp(camSlider, 0, 1);

        // print(mainCam.WorldToViewportPoint(lpoint));
        camSlider += Input.GetAxis("Mouse ScrollWheel") * 0.1F;


        if (waterCam == null) return;
        if (camSlider <= 1 && camSlider > 0.01)
        {
            waterCam.enabled = true;

            //   scissor2.scissorRect.y = 1 - camSlider;
       //  Mathf.Clamp(camSlider)
          scissor2.scissorRect.height = camSlider;
        }
        else if (camSlider <= 0)
        {
            waterCam.enabled = false;
        }
    }

   
}
