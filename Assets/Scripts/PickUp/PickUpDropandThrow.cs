using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using InControl;

public class PickUpDropandThrow : PickUpandDrop
{
    static bool insideHere = false;
    [SerializeField] bool insideSphere = false;
    [SerializeField] double timeKeyDown = 0f;
    [SerializeField] bool timeKeyDownX = false;
    [SerializeField] bool timeKeyDownY = false;
    [SerializeField] bool changeSC = true;
    int force = 10;
    bool nearEnemy = false;
    [SerializeField] bool isImportantObject = false;
    [SerializeField] bool hasObjectInside = false;
    Transform enemy;
    [Header("OBJECT COMPONENTS")]
    [SerializeField] Rigidbody _thisRB;
    [SerializeField] SphereCollider _thisSC;
    [Header("EXTERNAL OBJECTS")]
    [Header("ALWAYS NEEDED")]
    [SerializeField] Whip playerWhip;
    AudioSource hitSound;
    [Header("IF IS SPECIAL OBJECT")]
    [SerializeField] bool isSpecialObject;
    AudioSource grabSoundeffect;
    [Header("ONLY IF NOT IMPORTANT OBJECT")]
    [SerializeField] GameObject dustParticles;
    [SerializeField] GameObject brokenVase;
    [SerializeField] AudioSource brokenSound;
    [Header("ONLY IF HAS OBJECT INSIDE")]
    [SerializeField] GameObject objectInside;
    [SerializeField] bool useGravity = true;
    bool thrown = false;
    Vector3 playerToEnemy;
    [SerializeField] TutorialSprites tutoSprites;

    AudioSource[] sounds;

    public bool isBroken = false;
    // Start is called before the first frame update

    public void ResetObject()
    {
        ResetPosition();
        objectInside.transform.SetParent(transform);
        objectInside.SetActive(false);
        dustParticles.transform.SetParent(transform);
        dustParticles.SetActive(false);
        brokenVase.transform.SetParent(transform);
        brokenVase.SetActive(false);

        isBroken = false;
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
    void Start()
    {
        startingPosition = transform.position;
        startingRotation = transform.localRotation;

        playerMovement = GameObject.Find("Character").GetComponent<PlayerMovement>();
        _thisRB.useGravity = false;
        useGravity = false;
        _thisRB.constraints = RigidbodyConstraints.FreezeAll;
        if(changeSC)
            _thisSC.radius = 1;

        grabPlace = GameObject.Find("GrabObjectPos");

        sounds = GetComponents<AudioSource>();

        hitSound = sounds[0];
        grabSoundeffect= GameObject.Find("Special Object").GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        CheckVariables();
        nearEnemy = playerWhip.attackMode;
        if (!timeKeyDownX && !timeKeyDownY) timeKeyDown = 0f;
        if (nearEnemy && player != null)
        {
            enemy = playerWhip.getEnemy();
            Debug.DrawRay(transform.position, enemy.position, Color.red);
            playerToEnemy = new Vector3(enemy.transform.position.x - player.transform.position.x, enemy.transform.position.y - player.transform.position.y, enemy.transform.position.z - player.transform.position.z);
        }
        if (InputManager.ActiveDevice.Action3.WasPressed && player != null)
        {
            timeKeyDownX = true;
        }
        if (InputManager.ActiveDevice.Action4.WasPressed&& player != null)
        {
            timeKeyDownY = true;
        }

        else if ((InputManager.ActiveDevice.Action3.WasReleased) && player != null)
        {
            if (!cancelledDrop)
            {
                if (!objectIsGrabbed && insideSphere)
                {
                    useGravity = false;
                    playerAnimator.SetBool("PickUp", true);
                    player.SendMessage("StopMovement", true);
                    _thisRB.constraints = RigidbodyConstraints.FreezeAll;

                    if (isSpecialObject)
                        grabSoundeffect.Play();
                    StartCoroutine(PickUpCoroutine(0f));
                    playerMovement.ableToWhip = false;
                    StartCoroutine(AnimationsCoroutine(0.05f));
                }
                else if (timeKeyDown > 0f && objectIsGrabbed)
                {
                    playerAnimator.SetBool("DropObject", true);
                    _thisRB.constraints = RigidbodyConstraints.FreezeRotation;
                    useGravity = true;
                    StartCoroutine(DropObjectCoroutine(0f));
                    playerMovement.ableToWhip = true;
                    StartCoroutine(AnimationsCoroutine(0.1f));

                }
            }
            timeKeyDown = 0;
            timeKeyDownX = false;
        }


        if (timeKeyDownX)
            timeKeyDown += Time.deltaTime;


        if (timeKeyDownY)
        {
            timeKeyDown += Time.deltaTime;
            if (timeKeyDown > 0f && objectIsGrabbed && !nearEnemy)
            {
                ThrowObject();
                playerMovement.ableToWhip = true;
                useGravity = true;
            }
            else if (timeKeyDown > 0f && objectIsGrabbed && nearEnemy && (Vector3.Angle(player.transform.forward, playerToEnemy) < 70))
            {
                player.transform.LookAt(enemy);
                ThrowObjectToEnemy();
                playerMovement.ableToWhip = true;
                useGravity = true;
            }
            else if (timeKeyDown > 0f && objectIsGrabbed && nearEnemy && (Vector3.Angle(player.transform.forward, playerToEnemy) > 70))
            {
                ThrowObject();
                playerMovement.ableToWhip = true;
                useGravity = true;
            }
            timeKeyDown = 0;
            timeKeyDownX = false;
            timeKeyDownY = false;
        }
        if (useGravity) _thisRB.AddForce(Physics.gravity * (_thisRB.mass * _thisRB.mass));
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!insideHere)
            {
                insideSphere = true;
                insideHere = true;
            }
            distanceChecker = player.transform.GetChild(1).gameObject;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(insideSphere)
            {
                insideSphere = false;
                insideHere = false;
            }
            player = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(!insideHere)
            {
                insideSphere = true;
                insideHere = true;
            }
            player = other.gameObject;
            playerAnimator = player.GetComponentInChildren<Animator>();
            distanceChecker = player.transform.GetChild(1).gameObject;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Death")
        {
            ResetPosition();
        }

        if (tag == "Destroyable")
        {
            if (hasObjectInside)
            {
                objectInside.SetActive(true);
                objectInside.transform.SetParent(null);
            }
            insideHere = false;
            isBroken = true;
            dustParticles.SetActive(true);
            dustParticles.transform.SetParent(null);
            brokenVase.transform.SetParent(null);
            RaycastHit ray;
            if(Physics.Raycast(transform.position, -transform.up, out ray, 2))
            {
                brokenVase.transform.position = ray.point;
                objectInside.transform.position = ray.point;
            }
            brokenVase.SetActive(true);
            brokenSound.Play();
            gameObject.SetActive(false);
        }
        if(tag == "Place" || tag == "Untagged")
        {
            if (thrown)
                hitSound.Play();
            thrown = false;
        }
        if (collision.transform.tag == "WhipEnemy" && (tag == "Destroyable" || transform.GetChild(0).tag == "Place"))
        {
            enemy.SendMessage("Die");
        }


    }
    protected void ObjectDrop()
    {
        playerMovement.ableToWhip = true;
        thrown = true;
        Vector3 temp = player.transform.forward * (2500 * (0.5f));
        temp.y = 750;
        transform.SetParent(null);
        _thisRB.AddForce(temp);
        objectIsGrabbed = false;
    }

    protected void ThrowObject()
    {
        if (tutoSprites != null)
        {
            tutoSprites.isPlayerInside = false;
            tutoSprites.DeactivateSprites();
        }

        playerAnimator.SetBool("Throw", true);
        player.SendMessage("StopMovement", true);
        StartCoroutine(ThrowCoroutine(0.4f));
    }
    protected void ThrowObjectToEnemy()
    {
        if (tutoSprites != null)
        {
            tutoSprites.isPlayerInside = false;
            tutoSprites.DeactivateSprites();
        }

        playerAnimator.SetBool("Throw", true);
        player.SendMessage("StopMovement", true);
        StartCoroutine(ThrowToEnemyCoroutine(0.4f));
    }
    
    protected IEnumerator PickUpCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        PickUpObject();
    }
    protected IEnumerator ThrowCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        _thisRB.constraints = RigidbodyConstraints.FreezeRotation;
        DropObject();
        if (!isImportantObject)
            transform.tag = "Destroyable";
        else
            transform.GetChild(0).tag = "Place";
        thrown = true;
        Vector3 temp = player.transform.forward * (15000 * 0.3f);
        _thisRB.AddForce(temp);
        playerAnimator.SetBool("PickUp", false);
        playerAnimator.SetBool("DropObject", false);
        playerAnimator.SetBool("PlaceObject", false);
        playerAnimator.SetBool("Throw", false);
        player.SendMessage("StopMovement", false);
        objectIsGrabbed = false;
    }
    protected IEnumerator ThrowToEnemyCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        _thisRB.constraints = RigidbodyConstraints.FreezeRotation;
        DropObject();
        if (!isImportantObject)
            transform.tag = "Destroyable";
        else
            transform.GetChild(0).tag = "Place";
        thrown = true;
        Vector3 temp = new Vector3();
        Vector3 thisToEnemy = new Vector3(enemy.transform.position.x - transform.position.x, enemy.transform.position.y + 1.5f - transform.position.y, enemy.transform.position.z - transform.position.z);
        temp = thisToEnemy.normalized * (18000 * 0.3f);
        _thisRB.AddForce(temp);
        playerAnimator.SetBool("PickUp", false);
        playerAnimator.SetBool("DropObject", false);
        playerAnimator.SetBool("PlaceObject", false);
        playerAnimator.SetBool("Throw", false);
        player.SendMessage("StopMovement", false);
        objectIsGrabbed = false;
    }
    protected IEnumerator DropObjectCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        ObjectDrop();
    }
    public void DropObject()
    {
        ObjectDrop();
    }
    public bool GetObjectIsGrabbed()
    {
        //Debug.Log(transform.name + " " + objectIsGrabbed);
        return objectIsGrabbed;
    }
    override protected void ForcePickUpObject()
    {
        if (!objectIsGrabbed)
        {
            _thisRB.constraints = RigidbodyConstraints.FreezeAll;
            useGravity = false;
            playerAnimator.SetBool("PickUp", true);
            transform.SetParent(grabPlace.transform);
            transform.localPosition = Vector3.zero;
            objectIsGrabbed = true;
            player.GetComponent<playerDeath>().objectGrabbed = gameObject;
        }
    }
}