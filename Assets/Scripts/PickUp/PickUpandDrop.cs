using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpandDrop : PickUp
{
    [SerializeField] Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        objectIsGrabbed = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckVariables();
        if (Input.GetButtonDown("Interact") && isFacingBox && !cancelledDrop && distanceSuficient)
        {
            if (!objectIsGrabbed)
            {
                playerAnimator.SetBool("PickUp", true);
                playerAnimator.SetFloat("Distance", Mathf.Abs((transform.position.y - grabPlace.transform.position.y)));
                player.SendMessage("StopMovement", true);
                StartCoroutine(PickUpCoroutine(0.75f));
                StartCoroutine(AnimationsCoroutine(2.75f));
            }
            else
            {
                playerAnimator.SetBool("DropObject", true);
                player.SendMessage("StopMovement", true);
                StartCoroutine(DropObjectCoroutine(0.75f));
                StartCoroutine(AnimationsCoroutine(2.5f));
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            grabPlace = player.transform.GetChild(1).gameObject;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            playerAnimator = player.GetComponentInChildren<Animator>();
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

    IEnumerator AnimationsCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        player.SendMessage("StopMovement", false);
        playerAnimator.SetBool("PickUp", false);
        playerAnimator.SetBool("DropObject", false);

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
