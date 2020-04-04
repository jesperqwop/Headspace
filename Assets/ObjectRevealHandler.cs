﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRevealHandler : MonoBehaviour
{
    public static ObjectRevealHandler instance;
    public static Camera cam;
    public Transform player;
    public List<Memory> memories = new List<Memory>();
    public float minDistance = 3f;

    void Awake()
    {
        instance = this;
        Interactable.onInteract += OnInteraction;
        cam = Camera.main;
    }

    private void OnInteraction()
    {
        UpdateListOfMemories();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateListOfMemories();
    }

    // Update is called once per frame
    void Update()
    {
        if (memories.Count > 0)
        {
            foreach (Memory m in memories)
            {
                if (m.gObject)
                    m.ViewTest();
            }
        }
    }

    public void UpdateListOfMemories()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Memory"))
        {
            if (memories.Count > 0)
            {
                bool contained = false;
                foreach(Memory m in memories)
                {
                    if (m.gObject == go)
                    {
                        contained = true;
                    }
                }
                if (!contained)
                {
                    memories.Add(new Memory(go, player));
                }
            }
            else
            {
                memories.Add(new Memory(go, player));
            }
        }
    }

    public void LockReveal(GameObject memory)
    {
        foreach (Memory m in memories)
        {
            if (m.gObject == memory)
            {
                m.revealLocked = true;
            }
        }
    }
}

public class Memory
{
    public GameObject gObject;
    public Transform player;
    public bool isWithinView;
    public bool isVisible = false;
    public MeshRenderer mr;
    public Collider col;
    public bool revealLocked = false;
    private bool nextState = false;
    private bool tooClose = false;
    
    public Memory (GameObject _gObject, Transform _player)
    {
        gObject = _gObject;
        player = _player;
        mr = gObject.GetComponent<MeshRenderer>();
        col = gObject.GetComponent<Collider>();
    }

    public void ViewTest()
    {
        Vector3 viewPos = ObjectRevealHandler.cam.WorldToViewportPoint(gObject.transform.position);
        bool x, y, z;
        x = viewPos.x <= 1.5f && viewPos.x >= -0.5f;
        y = viewPos.y <= 1.5f && viewPos.y >= -0.5f;
        z = viewPos.z > 0;
        isWithinView = x && y && z;
        float distance = Vector3.Distance(gObject.transform.position, player.position);
        if (!revealLocked)
            RevealOrHide(distance);
    }

    public void RevealOrHide(float distance)
    {
        tooClose = distance<ObjectRevealHandler.instance.minDistance;
        if (isWithinView && isVisible && !tooClose)
        {
            nextState = false;
        }
        else if (isWithinView && !isVisible && distance > 1f)
        {
            nextState = true;
        }
        else if (!isWithinView)
        {
            mr.enabled = nextState;
            col.enabled = nextState;
            isVisible = nextState;
        }
    }
}
