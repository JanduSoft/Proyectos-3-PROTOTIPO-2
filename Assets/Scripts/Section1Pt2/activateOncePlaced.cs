using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class activateOncePlaced : MonoBehaviour
{ 
    [SerializeField] GameObject fire;
    [SerializeField] string name;
    Transform skullTransform = null;
    private bool canPlace = false;
    private bool isObject  =false;
    // Update is called once per frame
    void Update()
    {
        if (canPlace && InputManager.ActiveDevice.Action3.WasPressed && isObject)
        {
            StartCoroutine(Activate());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPlace = true;
        }
        if (other.name == name)
        {
            skullTransform = other.transform;
            isObject = true;
            //other.SendMessage("DropObject");
            Debug.Log(other.name);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPlace = false;
        }
        else if (other.name == "Skull")
        {
            skullTransform = null;
        }
    }
    IEnumerator Activate()
    {
        yield return new WaitForSeconds(0.55f);
            fire.SetActive(true);
    }
}