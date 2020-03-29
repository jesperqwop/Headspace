using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int maxSize;
    public List<Sprite> inventoryThumbnails;
    public List<int> inventoryIDs;

    public GameObject[] thumbnails;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inventoryThumbnails.Count >= 1)
        {
            thumbnails[0].GetComponent<Image>().sprite = inventoryThumbnails[0];
        }
        if (inventoryThumbnails.Count >= 2)
        {
            thumbnails[1].GetComponent<Image>().sprite = inventoryThumbnails[1];
        }
        if (inventoryThumbnails.Count >= 3)
        {
            thumbnails[2].GetComponent<Image>().sprite = inventoryThumbnails[2];
        }
        if (inventoryThumbnails.Count >= 4)
        {
            thumbnails[3].GetComponent<Image>().sprite = inventoryThumbnails[3];
        }
        if (inventoryThumbnails.Count >= 5)
        {
            thumbnails[4].GetComponent<Image>().sprite = inventoryThumbnails[4];
        }
        if (inventoryThumbnails.Count >= 6)
        {
            thumbnails[5].GetComponent<Image>().sprite = inventoryThumbnails[5];
        }
    }

}
