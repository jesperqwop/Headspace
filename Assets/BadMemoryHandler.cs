using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BadMemoryHandler : MonoBehaviour
{
    public static BadMemoryHandler instance;
    public List<Interactable> unviewedMemories = new List<Interactable>();
    public List<Interactable> viewedMemories = new List<Interactable>();
    public float delayBeforeHighlight = 10f;
    public float waterLevel = 0f;
    public GameObject finalMemory;

    bool listPopulated = false;
    public bool triggered = false;
    public float t;

    bool checkForGoodMemory = false;
    public bool stopTheWater = false;

    public GameObject waterSplashSFX;
    public GameObject[] badMemories;

    public FadeIn fadeToWhite;
    bool startedLoad = false;
    bool lowerWater = false;
    AsyncOperation ao;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (unviewedMemories.Count == 3)
        {
            listPopulated = true;
        }
        if (listPopulated && triggered)
        {
            t += Time.deltaTime * 0.5f;
            t = Mathf.Clamp01(t);
            switch (viewedMemories.Count)
            {
                default:
                    break;
                case 0:                    
                    waterLevel = Mathf.Lerp(0, 0.25f, t);
                    break;
                case 1:
                    waterLevel = Mathf.Lerp(0.25f, 0.40f, t);
                    break;
                case 2:
                    waterLevel = Mathf.Lerp(0.40f, 0.55f, t);
                    break;
                case 3:
                    waterLevel = Mathf.Lerp(0.55f, 1f, t);
                    finalMemory.SetActive(true);
                    checkForGoodMemory = true;
                    
                    break;
            }
        }
        if (stopTheWater)
        {
            if (Input.anyKeyDown && !lowerWater)
            {
                lowerWater = true;
                t = 0;
            }
            if (lowerWater)
            {
                waterLevel = Mathf.Lerp(1, 0, t);
                FadeOut();
            }
        }
        if (checkForGoodMemory)
        {
            if (!startedLoad)
            {
                LoadTheEnd();
            }
            foreach (Interactable m in viewedMemories)
            {
                if (!m.isBadMemory)
                {
                    if (!stopTheWater)
                    {
                        stopTheWater = true;
                    }
                        
                }
            }
        }
        

    }

    public void LoadTheEnd()
    {
        startedLoad = true;
        ao = SceneManager.LoadSceneAsync(1);
        ao.allowSceneActivation = false;
    }

    public void FadeOut()
    {
        fadeToWhite.start = true;
        if (fadeToWhite.t >= 1)
        {
            ao.allowSceneActivation = true;
        }
    }

    public void Trigger()
    {
        triggered = true;
        foreach(Transform memory in transform)
        {
            memory.gameObject.SetActive(true);
        }
        GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().GetThoseFeetWet();
        waterSplashSFX.GetComponent<AudioSource>().Play();

        foreach(GameObject memory in badMemories)
        {
            memory.GetComponent<Collider>().enabled = true;
        }

    }
}
