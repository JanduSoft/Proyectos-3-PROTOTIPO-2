using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class isPlaced : MonoBehaviour
{

    [SerializeField] UpandDownSecondPuzzle lever;
    [SerializeField] AddSkull killme;
    Transform skullTransform = null;
    private bool canPlace = false;
    bool placed = false;
    void Update()
    {
        //if (canPlace && InputManager.ActiveDevice.Action3.WasPressed)
        if (canPlace && GeneralInputScript.Input_GetKeyDown("Interact"))
        {
            lever.enabled = true;
        }
        if (killme.isActivated) killme.enabled = false;
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
