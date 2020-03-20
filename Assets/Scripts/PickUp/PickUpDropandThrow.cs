using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PickUpDropandThrow : PickUpandDrop
{
    double timeKeyDown = 0f;
    bool keyDown = false;
    int force = 10;
    [SerializeField] bool nearEnemy = false;
    Transform enemy;
    [Header("OBJECT COMPONENTS")]
    [SerializeField] Rigidbody _thisRB;
    [SerializeField] SphereCollider _thisSC;
    [Header("EXTERNAL OBJECTS")]
    [SerializeField] Whip playerWhip;
    [SerializeField] GameObject dustParticles;
    [SerializeField] GameObject objectInside;
    [SerializeField] GameObject brokenVase;
    bool useGravity = true;
    // Start is called before the first frame update
    void Start()
    {
        _thisRB.useGravity = false;
        useGravity = false;
        startingPosition = transform.position;
        _thisRB.constraints = RigidbodyConstraints.FreezePosition;
        grabPlace = GameObject.Find("Hand_R_PickUp");
    }

    // Update is called once per frame
    void Update()
    {
        nearEnemy = playerWhip.attackMode;
        CheckVariables();
        if (nearEnemy)
            enemy = playerWhip.getEnemy();
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
                        StartCoroutine(PickUpCoroutine(0.4f));
                        StartCoroutine(AnimationsCoroutine(0.5f));
                    }
                    else
                    {
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
                else if (timeKeyDown > 0.5f && objectIsGrabbed && nearEnemy && (Vector3.Angle(player.transform.position, enemy.position) < 91))
                {
                    player.transform.LookAt(enemy);
                    StartCoroutine(ResetMovement(0.7f));
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
            else if (timeKeyDown > 0.5f && objectIsGrabbed && nearEnemy && (Vector3.Angle(player.transform.position, enemy.position) < 75))
            {
                StartCoroutine(ResetMovement(0.7f));
                ThrowObjectToEnemy();
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
            objectInside.SetActive(true);
            dustParticles.SetActive(true);
            dustParticles.transform.SetParent(null);
            objectInside.transform.SetParent(null);
            brokenVase.transform.SetParent(null);
            brokenVase.SetActive(true);
            gameObject.SetActive(false);
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
        StartCoroutine(ThrowCoroutine(0.3f));
    }
    protected void ThrowObjectToEnemy()
    {
        playerAnimator.SetBool("Throw", true);
        player.SendMessage("StopMovement", true);
        StartCoroutine(ThrowToEnemyCoroutine(0.3f));
    }
    protected IEnumerator AnimationsCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        playerAnimator.SetBool("PickUp", false);
        playerAnimator.SetBool("DropObject", false);
        playerAnimator.SetBool("PlaceObject", false);
        playerAnimator.SetBool("Throw", false);
        player.SendMessage("StopMovement", false);

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
        Debug.Log("A");
        DropObject();
        StartCoroutine(AnimationsCoroutine(0.2f));
        _thisRB.constraints = RigidbodyConstraints.FreezeRotation;
        transform.tag = "Destroyable";
        _thisSC.enabled = false;
        Vector3 temp = player.transform.forward * (18000 * ((float)timeKeyDown / 0.5f)) + player.transform.up * (5000 * ((float)timeKeyDown / 0.5f));
        _thisRB.AddForce(temp);
    }
    protected IEnumerator ThrowToEnemyCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("A");
        DropObject();
        StartCoroutine(AnimationsCoroutine(0.2f));
        _thisRB.constraints = RigidbodyConstraints.FreezeRotation;
        transform.tag = "Destroyable";
        _thisSC.enabled = false;
        Vector3 temp = new Vector3();
        Vector3 playerToEnemy = new Vector3(enemy.transform.position.x - player.transform.position.x, enemy.transform.position.y - player.transform.position.y, enemy.transform.position.z - player.transform.position.z);
        temp = playerToEnemy.normalized * (18000 * ((float)timeKeyDown / 0.5f)) + player.transform.up * 5000 * (float)timeKeyDown / 0.5f;
        _thisRB.AddForce(temp);
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