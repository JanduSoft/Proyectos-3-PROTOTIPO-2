using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PickUpandDrop : PickUp
{
    [SerializeField] protected Animator playerAnimator;
    // Start is called before the first frame update

    // Update is called once per frame

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            distanceChecker = player.transform.GetChild(1).gameObject;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            playerAnimator = player.GetComponentInChildren<Animator>();
        }
        if (other.CompareTag("Death") && !transform.CompareTag("Stone"))
        {
            ResetPosition();
        }
    }
    protected void ObjectDrop()
    {
        playerMovement.ableToWhip = true;
        transform.SetParent(null);
        Debug.Log("Set parent to null");

        objectIsGrabbed = false;
    }
    public void DropObject()
    {
        playerMovement.ableToWhip = true;
        StartCoroutine(DropObjectCoroutine(0f));
        StartCoroutine(AnimationsCoroutine(0.5f));
    }

    protected IEnumerator AnimationsCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        if (player != null)
            player.SendMessage("StopMovement", false);
        else
            Debug.LogError("Couldn't stop player movement because player is null");
        Debug.Log("Animations now false");
        playerAnimator.SetBool("PickUp", false);
        playerAnimator.SetBool("DropObject", false);
        playerAnimator.SetBool("PlaceObject", false);

    }

    protected IEnumerator PickUpCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        
        playerMovement.ableToWhip = false;
        PickUpObject();
    }
    protected IEnumerator DropObjectCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        playerMovement.ableToWhip = true;
        ObjectDrop();
    }
}