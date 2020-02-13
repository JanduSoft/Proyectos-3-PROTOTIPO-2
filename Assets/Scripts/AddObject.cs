using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObject : MonoBehaviour
{
    public GameObject placePosition;
    Transform skullTransform = null;
    public bool isImportantCup = false;
    bool canPlace = false;

    [SerializeField] GameObject skull = null;

    public bool isActivated = false;
    [SerializeField] bool faceOppositeDirection = false;

    private void Start()
    {
    }


    void LateUpdate()
    {
        if (skull != null)
        {
            if (canPlace && skull.GetComponent<PickUpandDrop>().GetObjectIsGrabbed() && !isActivated && Input.GetButtonDown("Interact"))
            {
                Debug.Log("Tries to place skull");
                skull.GetComponent<PickUpandDrop>().DropObject();
                skullTransform.position = placePosition.transform.position;
                skullTransform.rotation = transform.rotation;
                if (faceOppositeDirection) skullTransform.Rotate(0, 180, 0);   //this is in case you want to make the skull face the oposite direction
                isActivated = true;
            }
            else if (!isImportantCup && canPlace && !skull.GetComponent<PickUpandDrop>().GetObjectIsGrabbed() && isActivated && Input.GetButtonDown("Interact"))
            {
                //skull.GetComponent<DragAndDrop>().CancelledDrop(false);
                skull.GetComponent<PickUpandDrop>().GrabObject();
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
        else if (other.CompareTag("Place"))
        {
            skull = other.transform.parent.gameObject;
            skullTransform = other.transform.parent.gameObject.transform;
            skull.GetComponent<PickUpandDrop>().SetCancelledDrop(true);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Place"))
        {
            Debug.Log("detects skull");
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
        else if (other.CompareTag("Place"))
        {
            other.gameObject.transform.parent.GetComponent<PickUpandDrop>().SetCancelledDrop(false);
            skull = null;
            skullTransform = null;
        }
    }


}
