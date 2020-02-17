using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ActivateDoor : MonoBehaviour
{
    [SerializeField] Transform objectoToMove;
    [SerializeField] Transform finalPosition;
    [SerializeField] float speed;
    [Header("PARTS TO ACTIVATE")]
    [SerializeField] EnablePartsOfMap toDisable1;
    [SerializeField] EnablePartsOfMap toDisable2;
    [SerializeField] EnablePartsOfMap toEnable;

    [SerializeField] SectionMapVariables myPart;
    [SerializeField] Transform skullPlace;


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Skull"))
        {
            if (!other.GetComponent<PickUpandDrop>().GetObjectIsGrabbed())
            {
                objectoToMove.DOMove(finalPosition.position, speed);
                other.gameObject.transform.position = skullPlace.position;
                other.gameObject.GetComponent<PickUpandDrop>().enabled = false;
                myPart.isActive = true;

            }
        }
    }
}
