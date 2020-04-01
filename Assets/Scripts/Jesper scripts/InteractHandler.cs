using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractHandler : MonoBehaviour
{

    public float interactRange;
    public GameObject interactUI;
    public LayerMask rayMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * interactRange, Color.green);

        Ray ray = new Ray(transform.position, transform.forward * interactRange);

        if (Physics.Raycast(ray, out RaycastHit hit, interactRange ,rayMask))
        {

            if(hit.transform.GetComponent<Interactable>() != null)
            {
                interactUI.SetActive(true);
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
                {
                    Interact(hit.transform.gameObject);
                }
            }
            else
            {
                interactUI.SetActive(false);
            }
        }
        else
        {
            interactUI.SetActive(false);
        }

    }

    void Interact(GameObject interactable)
    {
        interactable.GetComponent<Interactable>().Interaction();
    }

}
