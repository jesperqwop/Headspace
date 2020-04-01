using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupOverlay : MonoBehaviour
{
    public static PickupOverlay instance;
    public GameObject folder;
    public Image icon;
    public new Text name;
    public Text description;
    public float skipDelay = 0.5f;
    float t = 0;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (folder.activeSelf)
        {
            t += Time.deltaTime;
            if (Input.anyKey && t > skipDelay)
            {
                folder.SetActive(false);
                t = 0;
            }
        }
    }

    public void SetInfo(Sprite _image, string _name, string _description)
    {
        icon.sprite = _image ? _image : null;
        name.text = !string.IsNullOrEmpty(_name) ? _name : "name is null";
        description.text = !string.IsNullOrEmpty(_description) ? _description : "description is null";
        folder.SetActive(true);
    }
}
