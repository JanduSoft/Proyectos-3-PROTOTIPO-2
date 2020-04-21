﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class AddObject : MonoBehaviour
{
    public GameObject placePosition;
    public Transform _objectTransform = null;
    bool canPlace = false;
    bool objectIsGrabbed = false;
    protected Animator playerAnimator;
    [SerializeField] GameObject targetObject = null;
    [SerializeField] GameObject _object = null;

    public bool isActivated = false;
    [SerializeField] bool faceOppositeDirection = false;
    [SerializeField] bool quarterRotation = false;

    private void Start()
    {
    }


    void LateUpdate()
    {
        if (_object != null )
        {
            if (canPlace && (objectIsGrabbed) && !isActivated && InputManager.ActiveDevice.Action3.WasPressed)
            {
                Debug.Log("object placed:"+_object.name);
                playerAnimator.SetBool("PlaceObject", true);
                _object.GetComponent<PickUpDropandThrow>().DropObject();
                StartCoroutine(PlaceObject());
                
            }
            else if (canPlace && !objectIsGrabbed && InputManager.ActiveDevice.Action3.WasPressed)
            {
                _object.GetComponent<PickUpDropandThrow>().ForceGrabObject();
                if (isActivated)isActivated = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPlace = true;
            playerAnimator = other.GetComponentInChildren<Animator>();
        }
        else if (other.CompareTag("Place"))
        {
            _object = other.transform.parent.gameObject;
            _objectTransform = other.transform.parent.gameObject.transform;
            objectIsGrabbed = _object.GetComponent<PickUpDropandThrow>().GetObjectIsGrabbed();
            _object.GetComponent<PickUpDropandThrow>().SetCancelledDrop(true);

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
    IEnumerator PlaceObject()
    {
        objectIsGrabbed = false;
        _objectTransform.position = placePosition.transform.position;
        _objectTransform.rotation = transform.rotation;
        _object.transform.SetParent(transform);
        if (faceOppositeDirection) _objectTransform.Rotate(0, 180, 0);   //this is in case you want to make the skull face the oposite direction
        else if(quarterRotation) _objectTransform.Rotate(90, 90, 0);
        if(_object.name == targetObject.name)
            isActivated = true;
        yield return new WaitForSeconds(0.3f);
        playerAnimator.SetBool("PlaceObject", false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPlace = false;
        }
        else if (other.CompareTag("Place"))
        {
            other.gameObject.transform.parent.GetComponent<PickUpDropandThrow>().SetCancelledDrop(false);
            _object = null;
            _objectTransform = null;
        }
    }


}
