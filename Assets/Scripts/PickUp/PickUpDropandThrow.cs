using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PickUpDropandThrow : PickUpandDrop
{
    double timeKeyDown = 0f;
    bool keyDown = false;
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
    [Header("ONLY IF NOT IMPORTANT OBJECT")]
    [SerializeField] GameObject dustParticles;
    [SerializeField] GameObject brokenVase;
    [Header("ONLY IF HAS OBJECT INSIDE")]
    [SerializeField] GameObject objectInside;
    bool useGravity = true;
    Vector3 playerToEnemy;
    // Start is called before the first frame update
    void Start()
    {
        _thisRB.useGravity = false;
        useGravity = false;
        startingPosition = transform.position;
        _thisRB.constraints = RigidbodyConstraints.FreezePosition;
        grabPlace = GameObject.Find("GrabObjectPos");
    }

    // Update is called once per frame
    void Update()
    {
        CheckVariables();
        nearEnemy = playerWhip.attackMode;
        if (nearEnemy && player != null)
        {
            enemy = playerWhip.getEnemy();
            Debug.DrawRay(transform.position, enemy.position, Color.red);
            playerToEnemy = new Vector3(enemy.transform.position.x - player.transform.position.x, enemy.transform.position.y - player.transform.position.y, enemy.transform.position.z - player.transform.position.z);
        }
        if (Input.GetButtonDown("Interact"))
        {
            keyDown = true;
        }
        else if ((Input.GetButtonUp("Interact")))
        {
            if ( !cancelledDrop )
            {
                if (isFacingBox && !objectIsGrabbed && distanceSuficient)
                {
                    playerAnimator.SetBool("PickUp", true);
                    playerAnimator.SetFloat("Distance", Mathf.Abs((transform.position.y - distanceChecker.transform.position.y)));
                    player.SendMessage("StopMovement", true);
                    if (Mathf.Abs((transform.position.y - distanceChecker.transform.position.y)) < 0.6)
                    {
                        _thisRB.constraints = RigidbodyConstraints.FreezePosition;
                        StartCoroutine(PickUpCoroutine(0.4f));
                        StartCoroutine(AnimationsCoroutine(0.5f));
                    }
                    else
                    {
                        _thisRB.constraints = RigidbodyConstraints.FreezePosition;
                        StartCoroutine(PickUpCoroutine(0.4f));
                        StartCoroutine(AnimationsCoroutine(0.5f));
                    }
                }
                else if (timeKeyDown > 0f && timeKeyDown < 0.3f && objectIsGrabbed)
                {
                    playerAnimator.SetBool("DropObject", true);
                    player.SendMessage("StopMovement", true);
                    StartCoroutine(DropObjectCoroutine(0.5f));
                    StartCoroutine(AnimationsCoroutine(0.65f));
                }
                else if (timeKeyDown > 0.5f && objectIsGrabbed)
                {
                    StartCoroutine(ResetMovement(0.7f));
                    ThrowObject();
                    useGravity = true;
                }
                else if (timeKeyDown > 0.5f && objectIsGrabbed && nearEnemy && (Vector3.Angle(player.transform.forward, playerToEnemy) < 70))
                {
                    StartCoroutine(ResetMovement(0.7f));
                    player.transform.LookAt(enemy);
                    ThrowObjectToEnemy();
                    useGravity = true;
                    timeKeyDown = 0;
                }
            }
            timeKeyDown = 0;
            keyDown = false;
        }
        if (keyDown)
        {
            timeKeyDown += Time.deltaTime;
            if (timeKeyDown > 0.5f && objectIsGrabbed && !nearEnemy)
            {
                StartCoroutine(ResetMovement(0.7f));
                ThrowObject();
                useGravity = true;
                timeKeyDown = 0;
            }
            else if (timeKeyDown > 0.5f && objectIsGrabbed && nearEnemy && (Vector3.Angle(player.transform.forward, playerToEnemy) < 70))
            {
                StartCoroutine(ResetMovement(0.7f));
                player.transform.LookAt(enemy);
                ThrowObjectToEnemy();
                useGravity = true;
                timeKeyDown = 0;
            }
            else if (timeKeyDown > 0.5f && objectIsGrabbed && nearEnemy && (Vector3.Angle(player.transform.forward, playerToEnemy) > 70))
            {
                StartCoroutine(ResetMovement(0.7f));
                ThrowObject();
                useGravity = true;
                timeKeyDown = 0;
            }
        }

    }
    private void FixedUpdate()
    {
        if (useGravity) _thisRB.AddForce(Physics.gravity * (_thisRB.mass * _thisRB.mass));
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            distanceChecker = player.transform.GetChild(1).gameObject;
            player = other.gameObject;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            playerAnimator = player.GetComponentInChildren<Animator>();
            distanceChecker = player.transform.GetChild(1).gameObject;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (tag == "Destroyable")
        {
            if(hasObjectInside)
            {
                objectInside.SetActive(true);
                objectInside.transform.SetParent(null);
            }

            dustParticles.SetActive(true);
            dustParticles.transform.SetParent(null);
            brokenVase.transform.SetParent(null);
            brokenVase.SetActive(true);
            gameObject.SetActive(false);
        }
        if(collision.transform.tag == "WhipEnemy" && (tag == "Destroyable" || tag == "Place"))
        {
            enemy.SendMessage("Die");
        }
    }
    protected void ObjectDrop()
    {
        transform.SetParent(null);
        objectIsGrabbed = false;
    }

    protected void ThrowObject()
    {
        playerAnimator.SetBool("Throw", true);
        player.SendMessage("StopMovement", true);
        StartCoroutine(ThrowCoroutine(0.4f));
    }
    protected void ThrowObjectToEnemy()
    {
        playerAnimator.SetBool("Throw", true);
        player.SendMessage("StopMovement", true);
        StartCoroutine(ThrowToEnemyCoroutine(0.4f));
    }
    IEnumerator ResetMovement(float time)
    {
        yield return new WaitForSeconds(time);
        player.SendMessage("StopMovement", false);
    }
    protected IEnumerator PickUpCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        PickUpObject();
    }
    protected IEnumerator ThrowCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        DropObject();
        _thisRB.constraints = RigidbodyConstraints.FreezeRotation;
        if(!isImportantObject)
            transform.tag = "Destroyable";
        else
            transform.tag = "Place";
        _thisSC.enabled = false;
        Vector3 temp = player.transform.forward * (18000 * ((float)timeKeyDown / 0.5f));
        _thisRB.AddForce(temp);
        playerAnimator.SetBool("PickUp", false);
        playerAnimator.SetBool("DropObject", false);
        playerAnimator.SetBool("PlaceObject", false);
        playerAnimator.SetBool("Throw", false);
        player.SendMessage("StopMovement", false);
    }
    protected IEnumerator ThrowToEnemyCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        DropObject();
        _thisRB.constraints = RigidbodyConstraints.FreezeRotation;
        if (!isImportantObject)
            transform.tag = "Destroyable";
        else
            transform.tag = "Place";
        _thisSC.enabled = false;
        Vector3 temp = new Vector3();
        Vector3 thisToEnemy = new Vector3(enemy.transform.position.x - transform.position.x, enemy.transform.position.y + 1.5f - transform.position.y, enemy.transform.position.z - transform.position.z);
        temp = thisToEnemy.normalized * (18000 * ((float)timeKeyDown / 0.5f));
        _thisRB.AddForce(temp);
        playerAnimator.SetBool("PickUp", false);
        playerAnimator.SetBool("DropObject", false);
        playerAnimator.SetBool("PlaceObject", false);
        playerAnimator.SetBool("Throw", false);
        player.SendMessage("StopMovement", false);
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
        Debug.Log(transform.name + " " + objectIsGrabbed);
        return objectIsGrabbed;
    }
}