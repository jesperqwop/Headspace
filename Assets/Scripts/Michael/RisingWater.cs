using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingWater : MonoBehaviour
{
    GameManager manager;
    public float minY = 1, maxY = 5;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.up * Time.deltaTime * manager.waterRiseSpeed); 
        float newY = Mathf.Lerp(minY, maxY, BadMemoryHandler.instance.waterLevel);
        Vector3 newpos = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
        transform.localPosition = newpos;
    }
}
