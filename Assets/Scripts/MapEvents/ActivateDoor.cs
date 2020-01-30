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


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Aqui entra? FUERA");
        if (other.CompareTag("Skull"))
        {
            Debug.Log("Aqui entra?");
            objectoToMove.DOMove(finalPosition.position, speed);

            myPart.isActive = true;
        }
    }
}
