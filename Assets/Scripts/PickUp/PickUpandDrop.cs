using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpandDrop : PickUp
{
    Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        objectIsGrabbed = false;
        grabPlace = GameObject.Find("Hand_R_PickUp");
    }

    // Update is called once per frame
    void Update()
    {
        CheckVariables();
        if (Input.GetButtonDown("Interact") && !cancelledDrop )
        {
            if (!objectIsGrabbed && distanceSuficient && isFacingBox)
            {
                playerAnimator.SetBool("PickUp", true);
                playerAnimator.SetFloat("Distance", Mathf.Abs((transform.position.y - distanceChecker.transform.position.y)));
                player.SendMessage("StopMovement", true);
                if (Mathf.Abs((transform.position.y - distanceChecker.transform.position.y)) < 0.6)
                {
                    StartCoroutine(PickUpCoroutine(0.75f));
                    StartCoroutine(AnimationsCoroutine(3f));
                }
                else
                {
                    StartCoroutine(PickUpCoroutine(2f));
                    StartCoroutine(AnimationsCoroutine(3f));
                }
            }
            else if(objectIsGrabbed)
            {
                playerAnimator.SetBool("DropObject", true);
                player.SendMessage("StopMovement", true);
                StartCoroutine(DropObjectCoroutine(1.9f));
                StartCoroutine(AnimationsCoroutine(1.7f));
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

    IEnumerator AnimationsCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        player.SendMessage("StopMovement", false);
        playerAnimator.SetBool("PickUp", false);
        playerAnimator.SetBool("DropObject", false);
        playerAnimator.SetBool("PlaceObject", false);

    }

    IEnumerator PickUpCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        PickUpObject();
    }
    IEnumerator DropObjectCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        DropObject();
    }
}
