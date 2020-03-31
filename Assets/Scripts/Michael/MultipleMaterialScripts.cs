using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class MultipleMaterialScripts : MonoBehaviour
{
    Transform target;
    GameManager manager;
    public bool autoDissolve;
    [Range(0, 1)]
    public float dissolveValue;
    Renderer[] matRends;
    Material[] mats;
    public int numberOfMaterials;
    public int extraCounter;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        matRends = GetComponentsInChildren<Renderer>();


        for (int i = 0; i < matRends.Length; i++)
        {
            numberOfMaterials += matRends[i].materials.Length;
        }
        mats = new Material[numberOfMaterials];

        for (int i = 0; i < matRends.Length; i++)
        {
            for (int j = 0; j < matRends[i].materials.Length; j++)
            {
                mats[extraCounter] = matRends[i].materials[j];
                extraCounter++;
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (autoDissolve)
        {
            if ((target.position - transform.position).magnitude < manager.minimumDissolveRange)
            {
                dissolveValue = 0;
            }
            else if ((target.position - transform.position).magnitude > manager.maximumDissolveRange)
            {
                dissolveValue = 1;
            }
            else
            {
                dissolveValue = Mathf.Abs((manager.maximumDissolveRange - manager.minimumDissolveRange) - (target.position - transform.position).magnitude - manager.minimumDissolveRange) / (manager.maximumDissolveRange - manager.minimumDissolveRange);
            }
        }

        foreach (Material mat in mats) { mat.SetFloat("_DissolveValue", dissolveValue); }

    }
}
