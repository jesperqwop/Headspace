using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keytracker : MonoBehaviour
{

    public int[] keys;
    Inventory playerInventory;
    public bool[] aquiredKeys;
    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(int ID in keys)
        {
            if (playerInventory.inventoryIDs.Contains(ID))
            {
                aquiredKeys[System.Array.IndexOf(keys,ID)] = true;
            }
        }

        if(Unlocked() == true)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public bool Unlocked()
    {
        for (int i = 0; i < aquiredKeys.Length; ++i)
        {
            if (aquiredKeys[i] == false)
            {
                return false;
            }
        }

        return true;
    }

}
