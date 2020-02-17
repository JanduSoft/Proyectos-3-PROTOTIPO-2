using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ActivateDoor : MonoBehaviour
{
    [SerializeField] Transform objectoToMove;
    [SerializeField] Transform finalPosition;
    [SerializeField] GameObject objectToBePlaced;
    [SerializeField] float speed;
    [Header("PARTS TO ACTIVATE")]
    [SerializeField] EnablePartsOfMap toDisable1;
    [SerializeField] EnablePartsOfMap toDisable2;
    [SerializeField] EnablePartsOfMap toEnable;

    [SerializeField] SectionMapVariables myPart;
    [SerializeField] Transform skullPlace;
    bool isObjectPlaced = false;


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Place"))
        {
            if (!other.GetComponent<PickUpandDrop>().GetObjectIsGrabbed())
            {
                other.gameObject.transform.position = skullPlace.position;
                isObjectPlaced = true;
            }
            if(isObjectPlaced && other.name == objectToBePlaced.name)
            {
                objectoToMove.DOMove(finalPosition.position, speed);
                myPart.isActive = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Place"))
            isObjectPlaced = false;

    }
}
