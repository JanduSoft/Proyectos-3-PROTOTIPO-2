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
    [SerializeField] bool canIgniteMoreThanOnce = true;
    bool ignited = false;
    bool objectBurnt = false;
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
        CheckVariables();
        if(objectIsGrabbed && player!= null)
        {
            transform.localEulerAngles = new Vector3(180f, 0f, 0f);
        }
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
                DropTorch();
            }
            else if (nearFire)
            {
                if (!canIgniteMoreThanOnce) ignited = true;
                if(!ignited)
                {
                    fireParticles.SetActive(true);
                    torchIgnited = true;
                    currentFireObject = nearFireObject;
                }
                else
                {
                    DropTorch();
                }

                if (PlayerPrefs.GetInt("LitYourFirstTorch", 0) == 0) 
                {
                    Logros.litYourFirstTorch = 1;
                    PlayerPrefs.SetInt("LitYourFirstTorch", Logros.litYourFirstTorch);
                    Logros.CallAchievement(8);
                }
            }
            else if (torchIgnited && nearRope)
            {
                if (!objectBurnt)
                {
                    if (ObjectToBeBurnt != null)
                        ObjectToBeBurnt.SetActive(false);
                    nearRope = false;
                    objectBurnt = true;
                    shaking.StartShake(0.75f);
                    consequence.SetActive(true);
                }
                else
                {
                    Debug.Log("hlasdoa");

                    DropTorch();
                }
            }
        }
    }

    void DropTorch()
    {
        useGravity = true;
        _thisRB.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        playerAnimator.SetBool("DropObject", true);
        player.SendMessage("StopMovement", true);
        StartCoroutine(DropObjectCoroutine(0f));
        StartCoroutine(AnimationsCoroutine(0.1f));
    }
    public void turnOffTorch()
    {
        torchIgnited = false;
        objectBurnt = false;
        ignited = false;
        currentFireObject = null;
        fireParticles.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (useGravity)
        {
            _thisRB.AddForce(Physics.gravity * (_thisRB.mass * _thisRB.mass));
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