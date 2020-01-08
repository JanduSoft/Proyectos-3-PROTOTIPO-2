using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSkull : MonoBehaviour
{
    Transform placePosition;
    Transform skullTransform = null;
    private bool canPlace = false;
    // Update is called once per frame

    private void Start()
    {
        placePosition = transform.GetChild(0).transform;
    }

    void Update()
    {
        if (canPlace && Input.GetButtonDown("Interact"))
        {
            skullTransform.position = placePosition.position;
            skullTransform.rotation = this.transform.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPlace = true;
        }
        if (other.name == "Skull")
        {
            skullTransform = other.transform;
            Debug.Log(other.name);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Skull")
        {
            skullTransform = other.transform;
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
}
