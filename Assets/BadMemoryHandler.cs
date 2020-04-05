using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            switch (viewedMemories.Count)
            {
                default:
                    break;
                case 0:                    
                    waterLevel = Mathf.Lerp(0, 0.25f, t);
                    break;
                case 1:
                    waterLevel = Mathf.Lerp(0.25f, 0.45f, t);
                    break;
                case 2:
                    waterLevel = Mathf.Lerp(0.45f, 0.60f, t);
                    break;
                case 3:
                    waterLevel = Mathf.Lerp(0.6f, 1f, t);
                    finalMemory.SetActive(true);
                    checkForGoodMemory = true;
                    
                    break;
            }
        }

        if (checkForGoodMemory)
        {
            foreach(Interactable m in viewedMemories)
            {
                if (!m.isBadMemory)
                {
                    stopTheWater = true;
                    waterLevel = Mathf.Lerp(1, 0, t);
                }
            }
        }

        t = Mathf.Clamp01(t);
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
    }
}
