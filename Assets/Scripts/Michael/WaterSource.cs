using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSource : MonoBehaviour
{
    AudioSource AS;
    GameManager manager;
    ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        AS = GetComponentInChildren<AudioSource>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!ps.isPlaying && BadMemoryHandler.instance.triggered)
        {
            ps.Play();
        }
        if (manager.waterStatus == GameManager.WaterStatus.Underwater)
        {
            AS.spatialBlend = 0.9F;
        }
        else
        {
            AS.spatialBlend = 0;
        }
    }
}
