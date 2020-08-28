using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using InControl;

public class PickUpDragandDrop : PickUpandDrop
{
    static bool rockAlreadyPushed = false;

    [SerializeField] float grabPointHeight = 0.5f;
    [SerializeField] float stoneSpeed = 5;
    [SerializeField] Animator animator;
    [HideInInspector] public bool playSound = false;
    Vector3[] grabPoints = new Vector3[4];
    [SerializeField] float grabPointsDistance = 4.2f;
    int minPoint = -1;
    [HideInInspector] public Rigidbody rb;
    [SerializeField] public AudioSource dragSound;
    static GameObject currentRock;
    float sensitivityAngle = 90;
    [SerializeField] bool isGrounded;
    Vector3 closestPos;
    [HideInInspector] public GameObject touchedTrigger = null;
    Vector3 positionGrabbed;
    Transform playerGraphics;

    bool DoingSlide = false;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingRotation = transform.localRotation;


        playerMovement = GameObject.Find("Character").GetComponent<PlayerMovement>();
        playerGraphics = GameObject.Find("Character_Explorer_Male_01").GetComponent<Transform>();


        rb = GetComponent<Rigidbody>();
        startingPosition = transform.position;
        positionGrabbed = startingPosition;

    }
    private void OnDisable()
    {
        DoingSlide = false;
        minPoint = -1;
        closestPos = -Vector3.one;

        if (currentRock == gameObject)
            currentRock = null;
    }

    // Update is called once per frame

    void lookAtRock()
    {
        player.transform.DOLookAt(new Vector3(currentRock.transform.position.x, player.transform.position.y, currentRock.transform.position.z), 0.25f);
        Vector3 rockPos = currentRock.transform.position;
        rockPos.y = player.transform.position.y;
        playerGraphics.LookAt(rockPos);
    }
    void LateUpdate()
    {

        CheckVariables();

        //Check if the rock is grounded
        isGrounded = IsGrounded();

        //If there's a player detected
        if (player != null && touchedTrigger==null)
        {
            //bool isPressingButton = InputManager.ActiveDevice.Action3;
            bool isPressingButton = GeneralInputScript.Input_isKeyPressed("Interact");

            //If you can press the button again and you press it
            if (isPressingButton)
            {
                //If current rock is not null, look at it
                if (currentRock != null)
                {
                    lookAtRock();
                }

                //If the rock is not grabbed and you're facing it, grab it
                if (isFacingBox && !animator.GetBool("Attached") && currentRock == null && !DoingSlide)
                {
                    Vector3 auxPos= FindClosestPoint();
                    if (auxPos == -Vector3.one) return;

                    closestPos = auxPos;


                    playerMovement.grabbedToRock = true;
                    positionGrabbed = transform.position;
                    currentRock = gameObject;
                    rb.isKinematic = false;

                    DoingSlide = true;

                    player.transform.DOMove(closestPos, 0.5f).OnComplete(() => {

                        player.transform.position = closestPos;
                        animator.SetBool("Attached", true);

                    });

                }

                //If the rock is grabbed and you're facing it
                if (RockMiddleVariables.CanDrag && currentRock == gameObject)
                {
                    //Look at it
                    Debug.DrawRay(player.transform.position, player.transform.forward);

                    Vector3 lookDir = transform.position - player.transform.position;
                    lookDir.y = 0;

                    player.transform.rotation = Quaternion.LookRotation(lookDir);
                    player.transform.position = closestPos;

                    //float horizontalMove = Input.GetAxisRaw("Horizontal");
                    //float verticalMove = Input.GetAxisRaw("Vertical");

                    float horizontalMove = GeneralInputScript.Input_GetAxis("MoveHorizontal");
                    float verticalMove = GeneralInputScript.Input_GetAxis("MoveVertical");

                    bool inputActive = horizontalMove != 0 || verticalMove != 0;

                    float movementAngle = Vector3.Angle(player.transform.forward, playerMovement.movePlayer);

                    if (RockMiddleVariables.CanDrag)
                    {
                        if (movementAngle <= sensitivityAngle && inputActive)
                        {
                            PushRock(player.transform.forward);
                        }
                        else if (movementAngle > sensitivityAngle && inputActive)
                        {
                            RaycastHit hito;
                            Vector3 newPlayerPos = player.transform.position + new Vector3(0, 2.5f, 0);
                            if (Physics.Raycast(newPlayerPos, -player.transform.forward.normalized, out hito, 2.0f))
                            {
                                //something is behind the player and can't pull
                                Debug.DrawRay(newPlayerPos, -player.transform.forward.normalized * hito.distance, Color.red);
                            }
                            else
                            {
                                PullRock(player.transform.forward);
                            }
                        }
                        else
                        {
                            StartCoroutine(other());
                        }
                    }
                }

            }
            else if (!isPressingButton && currentRock == gameObject)
            {
                LetGoRock();

                if (isGrounded)
                    rb.isKinematic = true;

            }

        }

        //If this rock isn't the currentRock grabbed
        if (currentRock != gameObject)
        {
            if (isGrounded) rb.isKinematic = true;

            playSound = false;

        }

        //If there's no rock grabbed
        if (currentRock == null)
        {
            playerMovement.grabbedToRock = false;

            playerGraphics.DOKill();
            animator.SetBool("Attached", false);
            animator.SetBool("Push", false);
            animator.SetBool("Pulling", false);

            dragSound.Stop();
        }

        //SOUNDS AND ANIMATIONS
        //If this rock is being pushed
        if (playSound && !dragSound.isPlaying && currentRock == gameObject)
        {
            dragSound.Play();
        }
        else if (!playSound && currentRock == gameObject)
        {
            dragSound.Stop();
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

    void PushRock(Vector3 movingDirection)
    {
        if (!rockAlreadyPushed)
        {
            rockAlreadyPushed = true;
            Logros.numberOfRocksPushed++;
            PlayerPrefs.SetInt("NumberOfRocksPushed", Logros.numberOfRocksPushed);
            if (Logros.numberOfRocksPushed==5)
            {
                Logros.CallAchievement(7);
            }
        }

            playerGraphics.position = player.transform.position;
        animator.SetBool("Pulling", false);
        animator.SetBool("Push", true);
        rb.velocity = movingDirection.normalized * stoneSpeed;
        rb.position = new Vector3(transform.position.x, positionGrabbed.y + 0.1f, transform.position.z);
        playSound = true;
    }
    IEnumerator other()
    {
        if (animator.GetBool("Push") || animator.GetBool("Pulling"))
        {
            if (animator.GetBool("Pulling"))
            {
                playerGraphics.DOMove(player.transform.position - playerGraphics.forward.normalized, 0.6f);
            }
            else if (animator.GetBool("Push"))
            {
                playerGraphics.DOMove(player.transform.position + playerGraphics.forward.normalized * 0.5f, 0.6f);
            }
            animator.SetBool("Push", false);
            animator.SetBool("Pulling", false);
            playSound = false;

        }
        else
            playerGraphics.position = player.transform.position - playerGraphics.forward.normalized * 0.5f;


        yield return null;
    }
    void PullRock(Vector3 movingDirection)
    {
        if (!rockAlreadyPushed)
        {
            rockAlreadyPushed = true;
            Logros.numberOfRocksPushed++;
            PlayerPrefs.SetInt("NumberOfRocksPushed", Logros.numberOfRocksPushed);
            if (Logros.numberOfRocksPushed == 5)
            {
                Logros.CallAchievement(7);
            }
        }


        playerGraphics.position = player.transform.position;

        animator.SetBool("Push", false);
        animator.SetBool("Pulling", true);
        rb.velocity = -movingDirection.normalized * stoneSpeed;
        rb.position = new Vector3(transform.position.x, positionGrabbed.y + 0.1f, transform.position.z);
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
            closestPos = grabPoints[minPoint];
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
        dragSound.Stop();
        currentRock = null;
        DoingSlide = false;
        minPoint = -1;

    }
    Vector3 FindClosestPoint()
    {
        //FINDS CLOSEST POINT
        float minDistance = -1;
        Vector3 pointToPlayer = Vector3.zero;
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
            Debug.DrawRay(grabPoints[minPoint] + Vector3.up, pointToPlayer, Color.cyan,5f);

            if (hit.transform.tag != "Player")
                return -Vector3.one;

            else if (hit.transform.tag == "Player")
            {
                return grabPoints[minPoint];
            }
        }
        else if (grabPoints[minPoint]==closestPos)
        {
            return grabPoints[minPoint];
        }

        return -Vector3.one;
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
            if (currentRock == null)
            {
                animator.SetBool("Attached", false);
                player = null;

            }
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

        if (touchedTrigger != null && !collision.transform.CompareTag("Player"))
        {
            touchedTrigger.GetComponent<OnTriggerPlayAnim>().playSound();
            touchedTrigger = null;
        }

    }

}