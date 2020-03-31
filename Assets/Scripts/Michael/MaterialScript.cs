using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class MaterialScript : MonoBehaviour
{
    Transform target;
    GameManager manager;
    public bool autoDissolve;
    [Range(0, 1)]
    public float dissolveValue;
    Renderer matRend;
    Material mat;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        matRend = GetComponent<Renderer>();
        mat = matRend.material;
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

        mat.SetFloat("_DissolveValue", dissolveValue);
    }
}
