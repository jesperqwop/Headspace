using System.Collections;
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
                    if (go.GetComponent<MeshRenderer>())
                        memories.Add(new Memory(go, player));
                    else
                    {
                        GameObject[] contents = new GameObject[go.transform.childCount];
                        for (int i = 0; i < go.transform.childCount; i++)
                        {
                            contents[i] = go.transform.GetChild(i).gameObject;
                        }
                        memories.Add(new Memory(go, contents, player));
                    }
                }
            }
            else
            {
                if (go.GetComponent<MeshRenderer>())
                    memories.Add(new Memory(go, player));
                else
                {
                    GameObject[] contents = new GameObject[go.transform.childCount];
                    for (int i = 0; i < go.transform.childCount; i++)
                    {
                        contents[i] = go.transform.GetChild(i).gameObject;
                    }
                    memories.Add(new Memory(go, contents, player));
                }
            }
        }
        print(memories.Count);
    }

    public void LockReveal(GameObject memory)
    {
        foreach (Memory m in memories)
        {
            if (m.gObject == memory)
            {
                m.revealLocked = true;
            }
            else if (m.contents != null)
            {
                if (m.contents.Length > 0)
                {
                    for (int i = 0; i < m.contents.Length; i++)
                    {
                        if (m.contents[i] == memory)
                        {
                            m.revealLocked = true;
                        }
                    }
                }
            }
        }
    }
}

public class Memory
{
    public GameObject gObject;
    public GameObject[] contents;
    public Transform player;
    public bool isWithinView;
    public bool isVisible = false;
    public bool revealLocked = false;
    private bool nextState = false;
    private bool tooClose = false;
    private bool isContainer = false;
    
    public Memory (GameObject _gObject, Transform _player)
    {
        gObject = _gObject;
        player = _player;
    }
    
    public Memory (GameObject _gObject, GameObject[] _contents, Transform _player)
    {
        isContainer = true;
        gObject = _gObject;
        contents = _contents;
        player = _player;
    }

    public void ViewTest()
    {
        ViewTestSingle();
    }

    public void ViewTestSingle()
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
            if (nextState != isVisible)
            {
                if (isContainer)
                {
                    foreach (GameObject go in contents)
                    {
                        MeshRenderer[] mrs = go.transform.GetComponentsInChildren<MeshRenderer>();
                        Collider[] cols = go.transform.GetComponentsInChildren<Collider>();

                        foreach(MeshRenderer mr in mrs)
                        {
                            mr.enabled = nextState;
                        }
                        foreach(Collider col in cols)
                        {
                            col.enabled = nextState;
                        }
                    }
                }
                else
                {
                    gObject.GetComponent<MeshRenderer>().enabled = nextState;
                    gObject.GetComponent<Collider>().enabled = nextState;
                }
                isVisible = nextState;
            }
            Color debugColor = nextState ? Color.green : Color.red;
            if (tooClose)
                debugColor = Color.blue;
            Debug.DrawLine(player.transform.position, gObject.transform.position, debugColor);
        }
    }
}
