using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSkull : MonoBehaviour
{
    public GameObject placePosition;
    Transform skullTransform = null;
    private bool canPlace = false;

    private void Start()
    {
    }


    void Update()
    {
        if (canPlace && Input.GetButtonDown("Interact"))
        {
            skullTransform.position = placePosition.transform.position;
            skullTransform.rotation = this.transform.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPlace = true;
        }
        if (other.CompareTag("Skull"))
        {
            skullTransform = other.gameObject.transform;
            Debug.Log(other.name);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Skull"))
        {
            skullTransform = other.gameObject.transform;
            Debug.Log(other.name);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPlace = false;
        }
        else if (other.CompareTag("Skull"))
        {
            skullTransform = null;
        }
    }
}
