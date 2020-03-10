using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PickUpDropandIgnite : PickUpandDrop
{
    [SerializeField] GameObject fireParticles;
    [SerializeField] GameObject ObjectToBeBurnt;
    [SerializeField] GameObject consequence;
    [SerializeField] bool nearFire = false;
    [SerializeField] bool nearRope = false;
    [SerializeField] bool torchIgnited = false;
    [SerializeField] CamerShake shaking;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        grabPlace = GameObject.Find("Hand_R_PickUp");
    }

    // Update is called once per frame
    void Update()
    {
        CheckVariables();
        if (Input.GetButtonDown("Interact")  && !cancelledDrop && player != null)
        {
            if (!objectIsGrabbed && distanceSuficient )
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
            else if (!nearFire && !nearRope)
            {
                playerAnimator.SetBool("DropObject", true);
                player.SendMessage("StopMovement", true);
                StartCoroutine(DropObjectCoroutine(0.5f));
                StartCoroutine(AnimationsCoroutine(0.65f));
            }
            else if (nearFire)
            {
                Debug.Log("Torch Ignited");
                fireParticles.SetActive(true);
                torchIgnited = true;
            }
            else if (torchIgnited && nearRope)
            {
                Debug.Log("Rope Burnt");
                ObjectToBeBurnt.SetActive(false);
                consequence.SetActive(true);
                nearRope = false;
                shaking.StartShake(0.75f);
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
        else if (other.tag == "Fire")
        {
            Debug.Log("Near Fire");
            nearFire = true;
        }
        else if (other.tag == "Rope")
        {
            Debug.Log("Near Rope");
            nearRope = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            playerAnimator = player.GetComponentInChildren<Animator>();
        }
        else if (other.tag == "Fire")
        {
            Debug.Log("Near Fire");
            nearFire = true;
        }
        else if (other.tag == "Rope")
        {
            Debug.Log("Near Rope");
            nearRope = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
            grabPlace = null;
        }
        else if (other.tag == "Fire")
        {
            Debug.Log("Near Fire");
            nearFire = false;
        }
        else if (other.tag == "Rope")
        {
            Debug.Log("Near Rope");
            nearRope = false;
        }
    }
}