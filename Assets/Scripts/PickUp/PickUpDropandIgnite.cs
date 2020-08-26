using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using InControl;

public class PickUpDropandIgnite : PickUpandDrop
{
    [SerializeField] GameObject fireParticles;
    [SerializeField] public GameObject ObjectToBeBurnt;
    [SerializeField] public GameObject consequence;
    [SerializeField] bool nearFire = false;
    [SerializeField] bool nearRope = false;
    [SerializeField] public bool torchIgnited = false;
    [SerializeField] Rigidbody _thisRB;
    [SerializeField] CamerShake shaking;
    GameObject nearFireObject;
    bool useGravity = false;
    [SerializeField] public GameObject currentFireObject;
    // Start is called before the first frame update

    void Start()
    {
        startingPosition = transform.position;
        startingRotation = transform.localRotation;
        objectIsGrabbed = false;
        cancelledDrop = false;
        playerMovement = GameObject.Find("Character").GetComponent<PlayerMovement>();
        startingPosition = transform.position;
        grabPlace = GameObject.Find("Hand_R_PickUp"); objectIsGrabbed = false;
        cancelledDrop = false;
    }

    // Update is called once per frame
    override protected void Update()
    {
        if(transform.parent == null)
            Debug.Log("Current parent is null");
        else
            Debug.Log("Current parent is" + transform.parent.name);

        CheckVariables();
        if(objectIsGrabbed && player!= null)
        {
            transform.localEulerAngles = new Vector3(180f, 0f, 0f);
        }
        //if (InputManager.ActiveDevice.Action3.WasPressed && !cancelledDrop && player != null)
        if (GeneralInputScript.Input_GetKeyDown("Interact") && !cancelledDrop && player != null)
        {
            if (!objectIsGrabbed)
            {
                playerAnimator.SetFloat("Distance", Mathf.Abs((transform.position.y - distanceChecker.transform.position.y)));
                useGravity = false;
                _thisRB.constraints = RigidbodyConstraints.FreezeAll;
                StartCoroutine(PickUpCoroutine(0f));
            }
            else if (!nearFire && !nearRope)
            {
                _thisRB.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                playerAnimator.SetBool("DropObject", true);
                player.SendMessage("StopMovement", true);
                useGravity = true;
                StartCoroutine(DropObjectCoroutine(0f));
                StartCoroutine(AnimationsCoroutine(0.1f));
            }
            else if (nearFire)
            {
                fireParticles.SetActive(true);
                torchIgnited = true;
                currentFireObject = nearFireObject;
            }
            else if (torchIgnited && nearRope)
            {
                if (ObjectToBeBurnt!=null)
                    ObjectToBeBurnt.SetActive(false);
                consequence.SetActive(true);
                nearRope = false;
                shaking.StartShake(0.75f);
            }
        }
    }

    public void turnOffTorch()
    {
        torchIgnited = false;
        fireParticles.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (useGravity) _thisRB.AddForce(Physics.gravity * (_thisRB.mass * _thisRB.mass));
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
            nearFire = true;
        }
        else if (other.tag == "Rope")
        {
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
            nearFire = false;
        }
        else if (other.tag == "Rope")
        {
            nearRope = false;
        }
    }
}