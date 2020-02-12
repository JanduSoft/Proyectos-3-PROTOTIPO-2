using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSkull : MonoBehaviour
{
    public GameObject placePosition;
    Transform skullTransform = null;
    public bool isImportantCup=false;
    bool canPlace = false;

    GameObject skull = null;

    public bool isActivated = false;

    private void Start()
    {
    }


    void LateUpdate()
    {
        if (skull!=null)
        {
            if (canPlace && skull.GetComponent<DragAndDrop>().objectIsGrabbed && !isActivated && Input.GetButtonDown("Interact"))
            {
                skull.GetComponent<DragAndDrop>().DropObject();
                if (isImportantCup)
                {
                    skull.GetComponent<DragAndDrop>().enabled = false;
                }
                skullTransform.position = placePosition.transform.position;
                skullTransform.rotation = transform.rotation;
                isActivated = true;
            }
            else if (!isImportantCup && canPlace && !skull.GetComponent<DragAndDrop>().objectIsGrabbed && isActivated && Input.GetButtonDown("Interact"))
            {
                //skull.GetComponent<DragAndDrop>().CancelledDrop(false);
                skull.GetComponent<DragAndDrop>().GrabObject();
                isActivated = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPlace = true;
        }
        else if (other.CompareTag("Skull"))
        {
            skull = other.transform.parent.gameObject;
            skullTransform = other.transform.parent.gameObject.transform;
            skull.GetComponent<DragAndDrop>().CancelledDrop(true);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Skull"))
        {
            skull = other.transform.parent.gameObject;
            skullTransform = other.transform.parent.gameObject.transform;
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
            other.gameObject.transform.parent.GetComponent<DragAndDrop>().CancelledDrop(false);
            skull = null;
            skullTransform = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GetComponent<SphereCollider>().bounds.center, GetComponent<SphereCollider>().radius * transform.lossyScale.x);
    }
}
