using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using InControl;

public class PickUpDropandIgnite : PickUpandDrop
{
    [SerializeField] GameObject fireParticles;
    [SerializeField] GameObject ObjectToBeBurnt;
    [SerializeField] GameObject consequence;
    [SerializeField] bool nearFire = false;
    [SerializeField] bool nearRope = false;
    [SerializeField] public bool torchIgnited = false;
    [SerializeField] CamerShake shaking;
    GameObject nearFireObject;
    [SerializeField] public GameObject currentFireObject;
    // Start is called before the first frame update

    void Start()
    {
        startingPosition = transform.position;
        startingRotation = transform.localRotation;

        playerMovement = GameObject.Find("Character").GetComponent<PlayerMovement>();
        startingPosition = transform.position;
        grabPlace = GameObject.Find("Hand_R_PickUp");
    }

    // Update is called once per frame
    override protected void Update()
    {
        if(transform.parent == null)
            Debug.Log("Current parent is null");
        else
            Debug.Log("Current parent is" + transform.parent.name);

        CheckVariables();
        if(objectIsGrabbed)
        {
            transform.localEulerAngles = new Vector3(180f, 0f, 0f);
        }
        if (InputManager.ActiveDevice.Action3.WasPressed && !cancelledDrop && player != null)
        {
            if (!objectIsGrabbed && distanceSuficient )
            {
                playerAnimator.SetFloat("Distance", Mathf.Abs((transform.position.y - distanceChecker.transform.position.y)));
                if (Mathf.Abs((transform.position.y - distanceChecker.transform.position.y)) < 0.6)
                {
                    StartCoroutine(PickUpCoroutine(0f));
                }
                else
                {
                    StartCoroutine(PickUpCoroutine(0f));
                }
            }
            else if (!nearFire && !nearRope)
            {
                playerAnimator.SetBool("DropObject", true);
                player.SendMessage("StopMovement", true);
                StartCoroutine(DropObjectCoroutine(0f));
                StartCoroutine(AnimationsCoroutine(0.65f));
            }
            else if (nearFire)
            {
                Debug.Log("Torch Ignited");
                fireParticles.SetActive(true);
                torchIgnited = true;
                currentFireObject = nearFireObject;
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
            nearFire = true;
            nearFireObject = other.gameObject;
        }
        else if (other.tag == "Rope")
        {
            nearRope = true;
        }
        if (other.CompareTag("Death"))
        {
            ResetPosition();
        }

    }

    override public bool GetObjectIsGrabbed()
    {
        return objectIsGrabbed;
    }

    override public void ResetPosition()
    {
        if (!objectIsGrabbed)
        {
            try
            {
                AudioSource auxRespawn = GameObject.Find("ObjectRespawn").GetComponent<AudioSource>();
                if (!auxRespawn.isPlaying)
                    auxRespawn.Play();

                if (UseManualRespawn)
                    transform.position = manualRespawnPoint.position;
                else
                    transform.position = startingPosition;

                transform.localRotation = startingRotation;


                if (transform.CompareTag("Destroyable"))
                {
                    transform.tag = "Place";
                }
                GetComponent<Rigidbody>().velocity = Vector3.zero;

            }
            catch
            {
                Debug.Log("Can't respawn object");
            }
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