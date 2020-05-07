using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PickUpandDrop : PickUp
{
    protected Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingRotation = transform.localRotation;


        playerMovement = GameObject.Find("Character").GetComponent<PlayerMovement>();
        objectIsGrabbed = false;
        grabPlace = GameObject.Find("Hand_R_PickUp");
    } 

    // Update is called once per frame
    void Update()
    {
        CheckVariables();
        if (InputManager.ActiveDevice.Action3.WasPressed && !cancelledDrop )
        {
            if (!objectIsGrabbed && distanceSuficient && isFacingBox)
            {
                playerAnimator.SetBool("PickUp", true);
                playerAnimator.SetFloat("Distance", Mathf.Abs((transform.position.y - distanceChecker.transform.position.y)));
                player.SendMessage("StopMovement", true);
                if (Mathf.Abs((transform.position.y - distanceChecker.transform.position.y)) < 0.6)
                {
                    StartCoroutine(PickUpCoroutine(0.35f));
                    StartCoroutine(AnimationsCoroutine(0.5f));
                }
                else
                {
                    StartCoroutine(PickUpCoroutine(0.5f));
                    StartCoroutine(AnimationsCoroutine(0.65f));
                }
            }
            else if(objectIsGrabbed)
            {
                playerAnimator.SetBool("DropObject", true);
                player.SendMessage("StopMovement", true);
                StartCoroutine(DropObjectCoroutine(0.5f));
                StartCoroutine(AnimationsCoroutine(0.65f));
            }
        }

    }

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
        if (other.CompareTag("Death"))
        {
            ResetPosition();
        }
    }
    protected void ObjectDrop()
    {
        playerMovement.ableToWhip = true;
        transform.SetParent(null);
        objectIsGrabbed = false;
    }
    public void DropObject()
    {
        playerMovement.ableToWhip = true;
        player.SendMessage("StopMovement", true);
        StartCoroutine(DropObjectCoroutine(0.5f));
        StartCoroutine(AnimationsCoroutine(0.5f));
    }
    public bool GetObjectIsGrabbed()
    {
        return objectIsGrabbed;
    }

    protected IEnumerator AnimationsCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        player.SendMessage("StopMovement", false);
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