using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSkull : MonoBehaviour
{
    public GameObject placePosition;
    [SerializeField] Transform skullTransform = null;
    [SerializeField] private bool canPlace = false;
    public bool isImportantCup=false;

    [SerializeField] GameObject skull = null;

    public bool isActivated = false;
    [SerializeField] bool isCloseEnough = false;

    private void Start()
    {
    }


    void Update()
    {
        if (canPlace && Input.GetButtonDown("Interact") && skullTransform != null)
        {
            skullTransform.position = placePosition.transform.position;
            skullTransform.rotation = this.transform.rotation;
            isActivated = true;

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
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Skull"))
        {
            skull = other.gameObject;
            skullTransform = other.gameObject.transform;
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
            canPlace = false;
            skullTransform = null;
        }
    }
}
