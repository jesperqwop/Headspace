using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public enum Type {Pickup, Switch, PuzzleTrigger, Lock };
    public Type type;

    [Header("Pickup Settings")]
    public int KeyID;

    [Header("Switch Settings")]
    public bool singleUse;
    public bool used;
    public GameObject switchEffect;

    [Header("Puzzler Trigger Settings")]
    public GameObject puzzle;

    [Header("Lock Settings")]
    public int[] lockID;
    public GameObject lockEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interaction()
    {
        switch (type)
        {
            case Type.Pickup:
                Pickup();
                break;
            case Type.PuzzleTrigger:
                PuzzleTrigger();
                break;
            case Type.Switch:
                Switch();
                break;
        }
    }

    void Pickup()
    {
        Destroy(gameObject);
    }

    void PuzzleTrigger()
    {

    }

    void Switch()
    {

    }

}
