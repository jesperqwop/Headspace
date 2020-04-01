using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{

    public enum Type {Pickup, Switch, PuzzleTrigger, Lock };
    public Type type;

    [Header("Pickup Settings")]
    public string itemName;
    public Sprite itemThumbnail;
    public int KeyID;
    public GameObject pickupSFX;

    [Header("Switch Settings")]
    public bool singleUse;
    public bool used;
    public GameObject switchEffect;
    public GameObject OnSFX;
    public GameObject OffSFX;

    [Header("Puzzler Trigger Settings")]
    public GameObject puzzle;

    [Header("Lock Settings")]
    public int lockID;
    public GameObject[] lockEffect;
    public string acceptText;
    public string rejectText;
    public bool unlocked;

    Inventory playerInventory;
    GameObject interactMessage;
    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        interactMessage = GameObject.FindGameObjectWithTag("Interact message");
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
        }
    }

    void Pickup()
    {
        Destroy(gameObject);
        if(playerInventory.inventoryIDs.Count < playerInventory.maxSize)
        {
            Instantiate(pickupSFX, transform.position, Quaternion.identity);

            // Information du skal sende nedenfor er et billede af item, dets navn, og en beskrivelse af det
            PickupOverlay.instance.SetInfo(itemThumbnail ? itemThumbnail : null, itemName, "");
            playerInventory.inventoryThumbnails.Add(itemThumbnail ? itemThumbnail : null);
            playerInventory.inventoryIDs.Add(KeyID);
            interactMessage.GetComponent<Text>().text = "Picked up " + itemName;
            interactMessage.GetComponent<Animator>().SetTrigger("Display");
        }
    }

    void PuzzleTrigger()
    {

    }

    void Switch()
    {
        if (!used)
        {
            switchEffect.GetComponent<Animator>().SetTrigger("Switch");
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

}
