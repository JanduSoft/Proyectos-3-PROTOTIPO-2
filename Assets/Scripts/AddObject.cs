using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObject : MonoBehaviour
{
    public GameObject placePosition;
    Transform _objectTransform = null;
    bool canPlace = false;

    [SerializeField] GameObject _object = null;

    public bool isActivated = false;
    [SerializeField] bool faceOppositeDirection = false;

    private void Start()
    {
    }


    void LateUpdate()
    {
        if (_object != null)
        {
            if (canPlace && _object.GetComponent<PickUpandDrop>().GetObjectIsGrabbed() && !isActivated && Input.GetButtonDown("Interact") && _object != null)
            {
                Debug.Log("Tries to place obejct");
                _object.GetComponent<PickUpandDrop>().DropObject();
                _objectTransform.position = placePosition.transform.position;
                _objectTransform.rotation = transform.rotation;
                if (faceOppositeDirection) _objectTransform.Rotate(0, 180, 0);   //this is in case you want to make the skull face the oposite direction
                isActivated = true;
            }
            else if (canPlace && !_object.GetComponent<PickUpandDrop>().GetObjectIsGrabbed() && isActivated && Input.GetButtonDown("Interact"))
            {
                Debug.Log("Tries to pick up object");
                _object.GetComponent<PickUpandDrop>().ForceGrabObject();
                isActivated = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Player"))
        {
            canPlace = true;
        }
        else if (other.CompareTag("Place"))
        {
            _object = other.transform.parent.gameObject;
            _objectTransform = other.transform.parent.gameObject.transform;
            _object.GetComponent<PickUpandDrop>().SetCancelledDrop(true);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Place"))
        {
            _object = other.transform.parent.gameObject;
            _objectTransform = other.transform.parent.gameObject.transform;
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
            _object = null;
            _objectTransform = null;
        }
    }


}
