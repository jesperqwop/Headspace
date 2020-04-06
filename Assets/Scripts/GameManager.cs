using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{
    public enum WaterStatus { Dry, Water, Underwater };
    public WaterStatus waterStatus = WaterStatus.Dry;
    public static bool gameOver;

    public float waterRiseSpeed = 100;

    public float minimumDissolveRange;
    public float maximumDissolveRange;

    Camera mainCam;
    Camera waterCam;

    [Range(0, 1)]
    public float camSlider = 0;
    Scissor scissor2;
    public Transform apoint;

    public float x, y, z;
    public GameObject waterSource, waterSource1, waterSource2;

    AudioReverbZone reverbZone;
    FirstPersonController controller;

    AudioClip[] dryFootsteps;
    public AudioClip[] wetFootsteps;
    float maxVal;
    //  public firrs

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        dryFootsteps = controller.m_FootstepSounds;
        mainCam = Camera.main;
        waterCam = mainCam.transform.GetChild(0).GetComponent<Camera>();
        waterCam.gameObject.SetActive(true);
        scissor2 = waterCam.GetComponent<Scissor>();
        reverbZone = GetComponentInChildren<AudioReverbZone>();

        z = mainCam.nearClipPlane;
        x = mainCam.fieldOfView / 2 / 100;
        y = x / (Screen.currentResolution.width / Screen.currentResolution.height);
        UpdateSounds();
    }

    // Update is called once per frame
    void Update()
    {
        if (apoint.position.y > (mainCam.transform.position.y + y) && waterStatus != WaterStatus.Underwater && !gameOver)
        {
            camSlider = 1;
            print("Underwater boys");

            waterStatus = WaterStatus.Underwater;
            UpdateSounds();
        }
        else if (apoint.position.y > (mainCam.transform.position.y - y) && waterStatus != WaterStatus.Underwater || gameOver)
        {
            //print(camSlider);
            waterStatus = WaterStatus.Water;
            camSlider = mainCam.WorldToViewportPoint(new Vector3(mainCam.transform.position.x, apoint.position.y, mainCam.transform.position.z) + new Vector3(mainCam.transform.forward.x, 0, mainCam.transform.forward.z).normalized * mainCam.nearClipPlane).y;


        }

        if (waterStatus == WaterStatus.Water && !gameOver) {
            print(apoint.localPosition.y);
            if (apoint.localPosition.y > 2) { waterSource1.SetActive(enabled); }
            if (apoint.localPosition.y > 2.6) { waterSource2.SetActive(enabled); }
        }
   

        camSlider = Mathf.Clamp(camSlider, 0, 1);
        if (gameOver)
        {
            waterSource.GetComponentInChildren<FadeInAudio>().FadeOut();
            waterSource1.GetComponentInChildren<FadeInAudio>().FadeOut();
            waterSource2.GetComponentInChildren<FadeInAudio>().FadeOut();

            maxVal = camSlider;
            camSlider = Mathf.Clamp(camSlider, 0, maxVal);
            if (camSlider <= 0 && waterStatus != WaterStatus.Dry) {
                waterStatus = WaterStatus.Dry;
                UpdateSounds();
            }
            

        }


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

    void UpdateSounds()
    {
        if (waterStatus == WaterStatus.Water)
        {
            reverbZone.reverbPreset = AudioReverbPreset.StoneCorridor;
            controller.m_FootstepSounds = wetFootsteps;
        }
        else if (waterStatus == WaterStatus.Underwater)
        {
            reverbZone.reverbPreset = AudioReverbPreset.Underwater;
            controller.m_FootstepSounds = dryFootsteps;
            transform.GetChild(0).GetComponent<AudioSource>().Play();
        }
        else 
        {
            reverbZone.reverbPreset = AudioReverbPreset.Livingroom;
            controller.m_FootstepSounds = dryFootsteps;
            transform.GetChild(0).GetComponent<AudioSource>().Stop();
        }
    }
    
    public void GetThoseFeetWet()
    {
        waterStatus = WaterStatus.Water;
        UpdateSounds();
    }
}
