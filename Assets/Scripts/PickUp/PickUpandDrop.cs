using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpandDrop : PickUp
{
    // Start is called before the first frame update
    void Start()
    {
        objectIsGrabbed = false;
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckVariables();
        if (Input.GetButtonDown("Interact") && isFacingBox && !cancelledDrop)
        {
            if (!objectIsGrabbed)
                PickUpObject();
            else
                DropObject();
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            grabPlace = player.transform.GetChild(2).gameObject;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            grabPlace = player.transform.GetChild(1).gameObject;
        }
    }
    protected void ObjectDrop()
    {
        transform.SetParent(null);
        objectIsGrabbed = false;
    }
    public void DropObject()
    {
        ObjectDrop();
    }
    public bool GetObjectIsGrabbed()
    {
        return objectIsGrabbed;
    }
}