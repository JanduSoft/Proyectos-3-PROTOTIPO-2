using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (canPlace && Input.GetButtonDown("Interact")  && isObject)
        {
            fire.SetActive(true);
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
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "")
        {
            skullTransform = other.transform;
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
}