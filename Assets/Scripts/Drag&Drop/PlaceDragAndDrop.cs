using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceDragAndDrop : MonoBehaviour
{
    public GameObject placePosition;
    Transform skullTransform = null;
    bool canPlace = false;

    GameObject skull = null;

    public bool isActivated = false;

    private void Start()
    {
    }


    void LateUpdate()
    {
        if (skull != null)
        {
            if (canPlace && skull.GetComponent<DragAndDrop>().objectIsGrabbed && !isActivated && Input.GetButtonDown("Interact"))
            {
                skull.GetComponent<DragAndDrop>().DropObject();
                skull.GetComponent<DragAndDrop>().enabled = false;
                skullTransform.position = placePosition.transform.position;
                skullTransform.rotation = transform.rotation;
                isActivated = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPlace = true;
        }
        else if (other.CompareTag("PlaceObject"))
        {
            skull = other.transform.gameObject;
            skullTransform = other.transform.gameObject.transform;
            skull.GetComponent<DragAndDrop>().cancelledDrop = (true);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlaceObject"))
        {
            skull = other.transform.gameObject;
            skullTransform = other.transform.gameObject.transform;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPlace = false;
        }
        else if (other.CompareTag("PlaceObject"))
        {
            other.gameObject.transform.GetComponent<DragAndDrop>().cancelledDrop = (false);
            skull = null;
            skullTransform = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GetComponent<SphereCollider>().bounds.center, GetComponent<SphereCollider>().radius * transform.lossyScale.x);
    }
}
