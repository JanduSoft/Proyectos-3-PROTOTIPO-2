using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObject : MonoBehaviour
{
    public GameObject placePosition;
    Transform _objectTransform = null;
    bool canPlace = false;
    protected Animator playerAnimator;
    [SerializeField] GameObject _object = null;

    public bool isActivated = false;
    [SerializeField] bool faceOppositeDirection = false;
    [SerializeField] bool quarterRotation = false;

    private void Start()
    {
    }


    void LateUpdate()
    {
        if (_object != null)
        {
            if (canPlace && _object.GetComponent<PickUpandDrop>().GetObjectIsGrabbed() && !isActivated && Input.GetButtonDown("Interact") && _object != null)
            {
                playerAnimator.SetBool("PlaceObject", true);
                _object.GetComponent<PickUpandDrop>().DropObject();
                StartCoroutine(PlaceObject());
                
            }
            else if (canPlace && !_object.GetComponent<PickUpandDrop>().GetObjectIsGrabbed() && isActivated && Input.GetButtonDown("Interact"))
            {
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
            playerAnimator = other.GetComponentInChildren<Animator>();
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
    IEnumerator PlaceObject()
    {
        yield return new WaitForSeconds(0.55f);
        _objectTransform.position = placePosition.transform.position;
        _objectTransform.rotation = transform.rotation;
        if (faceOppositeDirection) _objectTransform.Rotate(0, 180, 0);   //this is in case you want to make the skull face the oposite direction
        else if(quarterRotation) _objectTransform.Rotate(90, 90, 0);
        isActivated = true;
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
