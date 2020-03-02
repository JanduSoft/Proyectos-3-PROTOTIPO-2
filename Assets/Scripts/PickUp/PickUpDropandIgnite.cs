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
    }

    // Update is called once per frame
    void Update()
    {
        CheckVariables();
        if (Input.GetButtonDown("Interact")  && !cancelledDrop )
        {
            if (!objectIsGrabbed && distanceSuficient && isFacingBox)
                PickUpObject();
            else if (!nearFire && !nearRope)
                DropObject();
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
            grabPlace = player.transform.GetChild(1).gameObject;
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
            grabPlace = player.transform.GetChild(1).gameObject;
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