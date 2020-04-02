using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSource : MonoBehaviour
{
    AudioSource AS;
    GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponentInChildren<AudioSource>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (manager.waterStatus == GameManager.WaterStatus.Underwater)
        {
            AS.spatialBlend = 1;
        }
        else
        {
            AS.spatialBlend = 0;
        }
    }
}
