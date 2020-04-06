using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeTracker : MonoBehaviour
{

    Text text;
    GameObject child1;
    GameObject child2;
    GameObject child3;
    Inventory playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        child1 = transform.GetChild(0).gameObject;
        child2 = transform.GetChild(1).gameObject;
        child3 = transform.GetChild(2).gameObject;

        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInventory.inventoryIDs.Contains(3))
        {
            child1.SetActive(true);
        }
        if (playerInventory.inventoryIDs.Contains(4))
        {
            child2.SetActive(true);
        }
        if (playerInventory.inventoryIDs.Contains(5))
        {
            child3.SetActive(true);
        }
    }
}
