using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using InControl;

public class PickUpDragandDrop : PickUpandDrop
{
    [SerializeField] float grabPointHeight = 0.5f;
    [SerializeField] float stoneSpeed = 5;
    [SerializeField] Animator animator;
    [HideInInspector] public bool playSound = false;
    [SerializeField] CharacterController playerController;
    [SerializeField] Quaternion rotation;
    Vector3[] grabPoints = new Vector3[4];
    [SerializeField] float grabPointsDistance = 4.2f;
    Vector3 closestPoint;
    int minPoint = -1;
    [HideInInspector] public Rigidbody rb;
    [SerializeField] bool rockGrabbed = false;
    public bool thisRock = false;
    [SerializeField] public AudioSource dragSound;
    static GameObject currentRock;
    float distToGround;
    bool canPressAgain = true;
    float sensitivityAngle = 50;
    [SerializeField] bool isGrounded;
    Vector3 closestPos;
    bool closestPointAvailable = false;


    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingRotation = transform.localRotation;


        playerMovement = GameObject.Find("Character").GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
        distToGround = transform.GetChild(0).GetComponent<Collider>().bounds.extents.y;
        startingPosition = transform.position;

    }
    private void OnDisable()
    {
        currentRock = null;
    }

    // Update is called once per frame
    void Update()
    {
        CheckVariables();

        //Check if the rock is grounded
        isGrounded = IsGrounded();

        //If there's a player detected
        if (player != null)
        {
            bool isPressingButton = InputManager.ActiveDevice.Action3;

            //If you can press the button again and you press it
            if (isPressingButton && canPressAgain) 
            {
                //If current rock is not null, look at it
                if (currentRock != null)
                    player.transform.DOLookAt(new Vector3(currentRock.transform.position.x, player.transform.position.y, currentRock.transform.position.z),0.25F);
                
                //If the rock is not grabbed and you're facing it, grab it
                if (!rockGrabbed && isFacingBox && !animator.GetBool("Attached") && currentRock==null)
                {
                    closestPos = FindClosestPoint();
                    if (!closestPointAvailable) return;

                    playerMovement.grabbedToRock = true;
                    currentRock = gameObject;
                    rb.isKinematic = false;
                    playerMovement.StopMovement(true);
                    player.transform.DOMove(closestPos, 0.5f, false).OnComplete
                        (
                        () => {
                            player.transform.position = closestPos;
                            rockGrabbed = true;
                        }
                        );
                    animator.SetBool("Attached", true);
                    Vector3 targetPostition = new Vector3(currentRock.transform.position.x, player.transform.position.y, currentRock.transform.position.z);
                    player.transform.DOLookAt(targetPostition, 0.25F);
                }

                //If the rock is grabbed and you're facing it
                if (rockGrabbed && isFacingBox && currentRock == gameObject)
                {
                    //Look at it
                    player.transform.DOLookAt(new Vector3(currentRock.transform.position.x, player.transform.position.y, currentRock.transform.position.z), 0.25F);
                    player.transform.position = closestPoint;

                    thisRock = true;
                    float horizontalMove = Input.GetAxis("Horizontal");
                    float verticalMove = Input.GetAxis("Vertical");

                    bool inputActive = horizontalMove != 0 || verticalMove != 0;

                    Vector3 movingDirection = Vector3.zero;
                    if (closestPoint==grabPoints[0])
                        movingDirection = grabPoints[1] - player.transform.position;
                    else if (closestPoint==grabPoints[1])
                        movingDirection = grabPoints[0] - player.transform.position;
                    else if (closestPoint==grabPoints[2])
                        movingDirection = grabPoints[3] - player.transform.position;
                    else if (closestPoint==grabPoints[3])
                        movingDirection = grabPoints[2] - player.transform.position;

                    if (Vector3.Angle(movingDirection, playerMovement.movePlayer) <= sensitivityAngle && inputActive)
                    {
                        PushRock(movingDirection);
                    }
                    else if (Vector3.Angle(-movingDirection, playerMovement.movePlayer) < sensitivityAngle && inputActive)
                    {
                        RaycastHit hito;
                        Vector3 newPlayerPos = player.transform.position + new Vector3(0, 2.5f, 0);
                        if (Physics.Raycast(newPlayerPos, -movingDirection.normalized, out hito, 2.0f))
                        {
                            //something is behind the player and can't pull
                            Debug.DrawRay(newPlayerPos, -movingDirection.normalized * hito.distance, Color.red);
                        }
                        else
                        {
                            PullRock(movingDirection);
                        }
                    }
                    else
                    {
                        other();
                    }
                }

            }
            else if (!isPressingButton && currentRock == gameObject)
            {
                currentRock = null;
                if (isGrounded)
                    rb.isKinematic = true;
                thisRock = false;
                rockGrabbed = false;
                playerMovement.grabbedToRock = false;
                animator.SetBool("Attached", false);
                animator.SetBool("Push", false);
                animator.SetBool("Pulling", false);
                playerMovement.StopMovement(false);
            }

        }

        //If this rock isn't the currentRock grabbed
        if (currentRock!=gameObject)
        {
            if (isGrounded) rb.isKinematic = true;
            thisRock = false;
            rockGrabbed = false;
        }

        //If there's no rock grabbed
        if (currentRock==null)
        {
            playerMovement.grabbedToRock = false;
            animator.SetBool("Attached", false);
            animator.SetBool("Push", false);
            animator.SetBool("Pulling", false);
            dragSound.Stop();
        }

        //SOUNDS AND ANIMATIONS
        //If this rock is being pushed
        if (playSound && !dragSound.isPlaying && thisRock)
        {
            dragSound.Play();
        }
        else if (!playSound && thisRock)
        {
            dragSound.Stop();
        }

        //If the fall rock animation is playing
        if (GetComponent<Animation>().isPlaying)
        {
            Debug.Log("AnimationPlaying");
            canPressAgain = false;
            playSound = false;
            dragSound.Stop();
            rockGrabbed = false;
            thisRock = false;
            currentRock = null;
        }
        else
        {
            canPressAgain = true;
        }
    }

    bool IsGrounded()
    {
        RaycastHit hit;
        Vector3 auxPosition = transform.position;
        if (Physics.Raycast(auxPosition, Vector3.down, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(auxPosition, Vector3.down * hit.distance, Color.yellow);
        }

        float distance = Vector3.Distance(hit.point, auxPosition);

        return (distance < 0.1f);
    }


    protected override void PickUpObject()
    {
        if (!rockGrabbed)
        {
            //FIND CLOSEST POINT
            closestPoint = closestPos;
            player.transform.DOMove(closestPoint, 0.5f, false);
            //LERP PLAYER TOWARDS POINT
            playSound = false;
            dragSound.Stop();
        }
        else
        {
            //Let go of rock
            playerMovement.StopMovement(false);
            rockGrabbed = false;
            playSound = false;
            dragSound.Stop();
        }
    }
    void PushRock(Vector3 movingDirection)
    {
        animator.SetBool("Push", true);
        rb.velocity = movingDirection.normalized * stoneSpeed;
        playSound = true;
    }
    void other()
    {
        animator.SetBool("Push", false);
        animator.SetBool("Pulling", false);
        playSound = false;
    }
    void PullRock(Vector3 movingDirection)
    {
        animator.SetBool("Pulling", true);
        rb.velocity = -movingDirection.normalized * stoneSpeed;
        playSound = true;
    }
    protected override void CheckVariables()
    {
        //calculate the grab points every frame in case the rock moves
        grabPoints[0] = transform.position + transform.forward * grabPointsDistance;
        grabPoints[1] = transform.position - transform.forward * grabPointsDistance;
        grabPoints[2] = transform.position + transform.right * grabPointsDistance;
        grabPoints[3] = transform.position - transform.right * grabPointsDistance;

        //put the points in the desired height
        grabPoints[0].y = transform.position.y - grabPointHeight;
        grabPoints[1].y = transform.position.y - grabPointHeight;
        grabPoints[2].y = transform.position.y - grabPointHeight;
        grabPoints[3].y = transform.position.y - grabPointHeight;

        //if minpoint isn't null, grab the closest point to the player
        if (minPoint != -1)
            closestPoint = grabPoints[minPoint];
        if (player != null)
        {
            isFacingBox = false;
            //checks if the distance from the player to the rock is close enough
            distancePlayerObject = Vector3.Distance(player.transform.position, transform.position);
            //checks if the player is staring in the direction of the rock
            Vector2 playerForward2d = new Vector2(player.transform.forward.x, player.transform.forward.z);
            Vector2 dirToObject2d = new Vector2(transform.position.x - player.transform.position.x, transform.position.z - player.transform.position.z);
            //we do the dot product of X and Z, to ignore the Y in case the object is placed above or below
            float dot = Vector3.Dot(playerForward2d, dirToObject2d);
            if (dot > 0.5f) { isFacingBox = true; }
        }
    }
    public void LetGoRock()
    {
        animator.SetBool("Attached", false);
        animator.SetBool("Push", false);
        animator.SetBool("Pulling", false);
        playerMovement.grabbedToRock = false;
        player.transform.DORotateQuaternion(rotation, 0.5f);
        playSound = false;
        dragSound.Stop();
        rockGrabbed = false;
        currentRock = null;
        thisRock = false;
    }
    Vector3 FindClosestPoint()
    {
        //FINDS CLOSEST POINT
        float minDistance = -1;
        Vector3 pointToPlayer = Vector3.zero;
        closestPointAvailable = true;
        float dist = 1;

        for (int i = 0; i < 4; i++)
        {
            dist = Vector3.Distance(player.transform.position, grabPoints[i]);
            if (minDistance == -1 || dist < minDistance)
            {
                minPoint = i;
                minDistance = dist;
            }

        }

        RaycastHit hit;
        pointToPlayer = player.transform.position - grabPoints[minPoint];

        if (Physics.Raycast(grabPoints[minPoint] + Vector3.up, pointToPlayer, out hit, Mathf.Infinity))
        {
            if (hit.transform.tag == "Player")
            {
                return grabPoints[minPoint];
            }
        }
        else if (pointToPlayer.magnitude<0.1f)
        {
            return grabPoints[minPoint];
        }


        closestPointAvailable = false;
        return new Vector3(-1,-1,-1);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Attached", false);
            player = null;
            thisRock = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minDistanceToGrabObject);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(grabPoints[0], 0.5f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(grabPoints[1], 0.5f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(grabPoints[2], 0.5f);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(grabPoints[3], 0.5f);

        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 10);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Death"))
        {
            if (respawn)
                ResetPosition();
        }

    }

}