using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{

    public enum Type {Pickup, Switch, PuzzleTrigger, Lock, Examine };
    public Type type;

    [Header("Pickup Settings")]
    public bool destroyOnPickup = true;
    public string itemName;
    public string itemDescription;
    public Sprite itemThumbnail;
    public int KeyID;
    public GameObject pickupSFX;

    [Header("Switch Settings")]
    public bool singleUse;
    public bool used;
    public GameObject switchEffect;
    public GameObject onSFX;
    public GameObject offSFX;

    [Header("Puzzler Trigger Settings")]
    public GameObject puzzle;

    [Header("Lock Settings")]
    public int lockID;
    public GameObject[] lockEffect;
    public string acceptText;
    public string rejectText;
    public bool unlocked;
    public GameObject unlockSFX;
    Inventory playerInventory;
    GameObject interactMessage;

    [Header("Examine Settings")]
    public string objectName;
    public string objectDescription;
    public Sprite objectIcon;
    public bool isBadMemory;
    public bool seen;
    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        interactMessage = GameObject.FindGameObjectWithTag("Interact message");
        if (isBadMemory)
        {
            BadMemoryHandler.instance.unviewedMemories.Add(this);
        }
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
            case Type.Lock:
                Lock();
                break;
            case Type.Examine:
                Examine();
                break;
        }
    }

    void Pickup()
    {
        if (destroyOnPickup)
            Destroy(gameObject);
        if(playerInventory.inventoryIDs.Count < playerInventory.maxSize)
        {
            Instantiate(pickupSFX, transform.position, Quaternion.identity);

            // Information du skal sende nedenfor er et billede af item, dets navn, og en beskrivelse af det
            PickupOverlay.instance.SetInfo(itemThumbnail ? itemThumbnail : null, itemName, itemDescription);
            playerInventory.inventoryThumbnails.Add(itemThumbnail ? itemThumbnail : null);
            playerInventory.inventoryIDs.Add(KeyID);
            interactMessage.GetComponent<Text>().text = "Picked up " + itemName;
            interactMessage.GetComponent<Animator>().SetTrigger("Display");
        }
    }

    void PuzzleTrigger()
    {
        puzzle.SendMessage("Trigger");
    }

    void Switch()
    {
        if (!used)
        {
            switchEffect.GetComponent<Animator>().SetTrigger("Switch");
            Instantiate(onSFX, transform.position, Quaternion.identity);
            if (singleUse)
            {
                used = true;
            }
        }
    }

    void Lock()
    {
        if (playerInventory.inventoryIDs.Contains(lockID) && unlocked != true)
        {
            foreach(GameObject effect in lockEffect)
            {
                effect.GetComponent<Animator>().SetTrigger("Unlock");
            }
            unlocked = true;
            interactMessage.GetComponent<Text>().text = acceptText;
            Instantiate(unlockSFX, transform.position, Quaternion.identity);
        }
        else
        {
            if (unlocked)
            {
                interactMessage.GetComponent<Text>().text = "It's already unlocked";
            }
            else
            {
                interactMessage.GetComponent<Text>().text = rejectText;
            }
        }
        interactMessage.GetComponent<Animator>().SetTrigger("Display");
    }

    public void Examine()
    {
        PickupOverlay.instance.SetInfo(objectIcon, objectName, objectDescription);
        if (!seen)
        {
            BadMemoryHandler.instance.t = 0;
            if (BadMemoryHandler.instance.unviewedMemories.Contains(this))
            {
                BadMemoryHandler.instance.unviewedMemories.Remove(this);
            }
            if (!BadMemoryHandler.instance.viewedMemories.Contains(this))
            {
                BadMemoryHandler.instance.viewedMemories.Add(this);
            }
            seen = true;            
        }
    }
}
