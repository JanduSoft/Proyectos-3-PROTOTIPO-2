using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSkull : MonoBehaviour
{
    public GameObject placePosition;
    Transform skullTransform = null;
    private bool canPlace = false;
    public bool isImportantCup=false;

    GameObject skull = null;

    public bool isActivated = false;
    bool isCloseEnough = false;

    private void Start()
    {
    }


    void Update()
    {
        if (canPlace && Input.GetButtonDown("Interact"))
        {
            skullTransform.position = placePosition.transform.position;
            skullTransform.rotation = this.transform.rotation;
            isActivated = true;
            if (isImportantCup)
            {
                skull.GetComponent<DragAndDrop>().enabled = false;
                skull.GetComponent<DragAndDrop>().transform.SetParent(null);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPlace = true;
            isCloseEnough = true;
        }
        if (other.CompareTag("Skull"))
        {
            skull = other.gameObject;
            skullTransform = other.gameObject.transform;
            Debug.Log(other.name);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Skull"))
        {
            skull = other.gameObject;
            skullTransform = other.gameObject.transform;
            Debug.Log(other.name);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPlace = false;
            isCloseEnough = false;
        }
        else if (other.CompareTag("Skull"))
        {
            skull = null;
            skullTransform = null;
        }
    }
}
