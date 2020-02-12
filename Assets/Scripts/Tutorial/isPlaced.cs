using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isPlaced : MonoBehaviour
{
    [SerializeField] Transform placePosition;
    [SerializeField] string name;
    [SerializeField] UpandDownSecondPuzzle lever;
    Transform skullTransform = null;
    private bool canPlace = false;
    bool placed = false;

    void Update()
    {
        if (canPlace && Input.GetButtonDown("Interact"))
        {
            lever.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPlace = true;
        }
        if (other.name == name)
        {
            //other.SendMessage("DropObject");
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.name == name)
        {
            Debug.Log(other.name);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPlace = false;
        }
        else if (other.name == "Skull")
        {
            skullTransform = null;
        }
    }
}
