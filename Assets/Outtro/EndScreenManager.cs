using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenManager : MonoBehaviour
{
    [Header("Dependencies")]
    public TextHandler firstText;
    public TextHandler secondText;
    public TextHandler thirdText;
    public FadeIn logo;
    public MovingColorBlocks block1;
    public MovingColorBlocks block2;
    public FadeIn lastText;
    public Camera cam;

    public float time;
    bool fadeCam = false;
    float t;
    Color camCurrent;

    // Start is called before the first frame update
    void Start()
    {
        camCurrent = cam.backgroundColor;
    }

    // Update is called once per frame
    void Update()
    {
        time = Mathf.RoundToInt(Time.time);

        switch (time)
        {
            default:
                break;
            case 1:
                firstText.FadeIn();
                break;
            case 3:
                firstText.StartNumber();
                break;
            case 5:
                
                break;
            case 6:
                firstText.FadeOut();
                block1.flip = true;
                block2.flip = true;
                break;
            case 7:
                secondText.FadeIn();
                break;
            case 9:
                secondText.StartNumber();
                break;
            case 11:
                
                break;
            case 12:
                secondText.FadeOut();
                block1.flip = false;
                block2.flip = false;
                break;
            case 13:
                thirdText.FadeIn();
                break;
            case 16:
                thirdText.StartNumber();
                break;
            case 18:
                
                break;
            case 19:
                thirdText.FadeOut();
                block1.flip = true;
                block2.flip = true;
                break;
            case 20:
                fadeCam = true;
                logo.start = true;
                break;
            case 21:
                lastText.start = true;
                break;
        }
        CamFade();
    }

    public void CamFade()
    {
        if (fadeCam)
        {
            t += Time.deltaTime;
            t = Mathf.Clamp01(t);
            cam.backgroundColor = Color.Lerp(camCurrent, Color.white, t);
        }
    }

    public void ClickedLink()
    {
        print("enter website");
        Application.OpenURL("https://dignity.dk/en/dignitys-work/facts-about-torture/");
    }
}
