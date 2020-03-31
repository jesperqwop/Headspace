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

    Scissor scissor1;
    Scissor scissor2;

    public Transform waterPlane;
    public Transform plane2;
    public Vector3 lpoint;
    public Vector3 lline;

    // Start is called before the first frame update
    void Start()
    {
        scissor1 = mainCam.GetComponent<Scissor>();
        scissor2 = waterCam.GetComponent<Scissor>();
    }

    // Update is called once per frame
    void Update()
    {
        //camSlider = mainCam.WorldToViewportPoint(new Vector3(transform.position.x, waterPlane.position.y, transform.position.z)).y;

        // print(mainCam.WorldToViewportPoint(lpoint));
        camSlider += Input.GetAxis("Mouse ScrollWheel") * 0.1F;

        if (Input.GetKeyDown("o")) { planePlaneIntersection(out lpoint, out lline, waterPlane.gameObject, plane2.gameObject); }


        if (waterCam == null) return;
        if (camSlider < 1 && camSlider > 0)
        {

            mainCam.enabled = true;
            waterCam.enabled = true;

         //   scissor2.scissorRect.y = 1 - camSlider;
            scissor2.scissorRect.height =  camSlider;
        }
        else if (camSlider <= 0)
        {
          waterCam.enabled = false;
        }
    }

    void planePlaneIntersection(out Vector3 linePoint, out Vector3 lineVec, GameObject plane1, GameObject plane2)
    {

        linePoint = Vector3.zero;
        lineVec = Vector3.zero;

        //Get the normals of the planes.
        Vector3 plane1Normal = plane1.transform.up;
        Vector3 plane2Normal = plane2.transform.up;

        //We can get the direction of the line of intersection of the two planes by calculating the
        //cross product of the normals of the two planes. Note that this is just a direction and the line
        //is not fixed in space yet.
        lineVec = Vector3.Cross(plane1Normal, plane2Normal);

        //Next is to calculate a point on the line to fix it's position. This is done by finding a vector from
        //the plane2 location, moving parallel to it's plane, and intersecting plane1. To prevent rounding
        //errors, this vector also has to be perpendicular to lineDirection. To get this vector, calculate
        //the cross product of the normal of plane2 and the lineDirection.      
        Vector3 ldir = Vector3.Cross(plane2Normal, lineVec);

        float numerator = Vector3.Dot(plane1Normal, ldir);

        //Prevent divide by zero.
        if (Mathf.Abs(numerator) > 0.000001f)
        {

            Vector3 plane1ToPlane2 = plane1.transform.position - plane2.transform.position;
            float t = Vector3.Dot(plane1Normal, plane1ToPlane2) / numerator;
            linePoint = plane2.transform.position + t * ldir;
        }
    }
}
