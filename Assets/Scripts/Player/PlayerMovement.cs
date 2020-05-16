using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using InControl;
public class PlayerMovement : MonoBehaviour
{
    const float maxSpeedWalking = 4.85f;
    const float maxSpeedJogging = 9f;
    const float maxSpeedRunning = 14;
    [SerializeField] public bool ableToWhip = true;

    InputDevice inputDevice;
    private void Awake()
    {
        inputDevice = InputManager.ActiveDevice;
    }

    #region VARIABLES
    [Header("MOVEMENT")]
    public bool canMove = true;
    [SerializeField] float horizontalMove;
    [SerializeField] float magnitudeInput;
    [SerializeField] float coyoteTime = 0.2f;
    [SerializeField] float auxCoyote = 0;
    [SerializeField] float verticalMove;
    [SerializeField] private Vector3 playerInput;
    Vector3 lastPlayerInput;
    public bool grabbedToRock = false;
    public Vector3 movePlayer;  //made public for DragAndDropObject.cs to use
    private bool stopped = false;
    [SerializeField] GameObject walkinParticles;
    [SerializeField] Transform walkinParticlesSpawner;
    public bool grounded = true;
    [Header("ACCELERATION")]
    [SerializeField] public CharacterController player;
    [SerializeField] float playerSpeed;
    float maxSpeed;
    [SerializeField] float finalSpeed = 0;
    [SerializeField] float stateSpeed = 0;
    [SerializeField] float acceleration;
    [SerializeField] Whip whip;
    float lookAtSpeed;
    [SerializeField] float normalLookAtSpeed;
    [SerializeField] float changingDirectionLookAtSpeed;
    [Header("JUMP")]
    [SerializeField] float jumpForce;
    [SerializeField] AudioSource jumpSound;
    float minPitch = 0.9f;
    float maxPitch = 1.1f;
    [SerializeField] float percentRestriction;
    Vector3 destineInput;
    [Header("GRAVITY")]
    [SerializeField] float gravity = 9.8f;
    [SerializeField] public float fallVelocity;
    [SerializeField] float regularGravityMultipliyer = 1f;
    [SerializeField] float gravityMultipliyerFalling = 2;
    [SerializeField] float timeOnAir;
    private bool isOnAir = true;
    private float timer = 0.0f;

    [Header("CAMERA")]
    [SerializeField] Camera mainCamera;
    [SerializeField] private Vector3 camForward;
    [SerializeField] private Vector3 camRight;

    AudioSource playerSteps;
    Vector3 prevPos;
    
    [Header("WHIP JUMP")]
    public bool isInWhipJump = false;

    [Header("ANIMATIONS")]
    [SerializeField] public Animator animatorController;
    [SerializeField] GameObject model;
    float timeIdle = 0;
    public bool inRespawn=false;
    #endregion


    #region START
    void Start()
    {
        auxCoyote = coyoteTime;
        player = GetComponent<CharacterController>();
        fallVelocity = -10;
        DOTween.Clear(true);
        playerSteps = GameObject.Find("Player walking").GetComponent<AudioSource>();
        prevPos = transform.position;
    }
    #endregion

    #region UPDATE
    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (player.isGrounded)
            {
                grounded = true;
            }
            if (isInWhipJump)
                grounded = false;
            //GET AXIS
            horizontalMove = inputDevice.LeftStickX;
            verticalMove = inputDevice.LeftStickY;

            playerInput = new Vector3(horizontalMove, 0, verticalMove);
            playerInput = Vector3.ClampMagnitude(playerInput, 1);

            //ACCELERATION
            if (playerInput == Vector3.zero)
            {
                playerSpeed = Mathf.Lerp(playerSpeed, 0, 0.1f);
                lookAtSpeed = changingDirectionLookAtSpeed;
            }
            else
            {
                lookAtSpeed = normalLookAtSpeed;
                playerSpeed = Mathf.Lerp(playerSpeed,maxSpeed, acceleration);
            }

            //GETTING CAMERA DIRECTION
            CamDirection();

            magnitudeInput = playerInput.magnitude;

            if (playerInput.magnitude > 0.7 || playerInput.magnitude < -0.7)
            {
                maxSpeed = maxSpeedRunning;
            }
            else if (playerInput.magnitude > 0.5 && playerInput.magnitude < 0.7 || playerInput.magnitude < -0.5 && playerInput.magnitude > -0.7)
            {
                maxSpeed = maxSpeedJogging;
            }
            else if (playerInput.magnitude > 0 && playerInput.magnitude < 0.5 || playerInput.magnitude < -0 && playerInput.magnitude > -0.5)
            {
                maxSpeed = maxSpeedWalking;
            }
            if (playerInput != Vector3.zero)
            {
                movePlayer = playerInput.x * camRight + playerInput.z * camForward;
                movePlayer *= playerSpeed;
                lastPlayerInput = playerInput;
            }
            else
            {
                movePlayer = lastPlayerInput.x * camRight + lastPlayerInput.z * camForward;
                movePlayer *= playerSpeed;
            }
            if (playerInput == Vector3.zero)
            {
                animatorController.SetBool("walking", false);
                timeIdle += Time.deltaTime;
                //animatorController.SetFloat("idle", timeIdle);

                if (timeIdle > 4)
                {
                    animatorController.SetFloat("randomIdle", Random.Range(0, 2));
                    timeIdle = 0;
                }
            }
            else if (!grabbedToRock && playerInput != Vector3.zero && !stopped)
            {
                // LOOK AT IF IS ON AIR OR GROUNDED
                if (player.isGrounded || grounded)
                {
                    player.transform.DOLookAt(player.transform.position + movePlayer, lookAtSpeed);
                    model.transform.position = player.transform.position;
                    model.transform.DOLookAt(player.transform.position + movePlayer, lookAtSpeed);
                }
                else if (!player.isGrounded && !grounded)
                {
                    player.transform.DOLookAt(player.transform.position + movePlayer, lookAtSpeed);
                    model.transform.position = player.transform.position;
                    model.transform.DOLookAt(player.transform.position + movePlayer, lookAtSpeed);
                }
                animatorController.SetBool("walking", true);
                animatorController.SetFloat("velocity", finalSpeed);
            }



            // GRAVITY
            SetGravity();

            if (!stopped)
            {
                // JUMP
                PlayerSkills();

                //WALKING SOUND & PARTICLES
                if (player.isGrounded && !playerSteps.isPlaying && player.velocity != Vector3.zero)
                {
                    prevPos = transform.position;
                    playerSteps.Play();
                    Destroy(Instantiate(walkinParticles, walkinParticlesSpawner.position, Quaternion.identity), 0.55f);
                }

                // MOVING CHARACTER

                if (player.isGrounded || grounded)
                {
                    animatorController.SetBool("Jumping", false);
                    Vector3 go = player.transform.forward * playerSpeed;
                    go.y = movePlayer.y;
                    player.Move(go * Time.deltaTime);
                    auxCoyote = coyoteTime;
                    animatorController.SetFloat("velocity", playerSpeed);
                }
                else if (!player.isGrounded && !grounded)
                {
                    player.Move((movePlayer * (percentRestriction / 100)) * Time.deltaTime);
                    animatorController.SetBool("Jumping", true);
                }
                if ((!player.isGrounded || !grounded) && auxCoyote > 0)
                {
                    auxCoyote -= Time.deltaTime;
                }
            }
        }
           

    }
    #endregion

    #region CAMERA DIRECTION
    void CamDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }
    #endregion

    #region  PLAYER SKILLS
    void PlayerSkills()
    {
        if ((player.isGrounded || auxCoyote > 0) && inputDevice.Action1.WasPressed && !jumpSound.isPlaying)
        {
            grounded = false;
            animatorController.SetBool("Jumping", true);
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
            jumpSound.pitch = Random.Range(minPitch, maxPitch);
            jumpSound.Play();
        }
    }
    #endregion

    #region SET GRAVITY
    void SetGravity()
    {

        if (player.isGrounded || inRespawn)
        {
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
            isOnAir = true;
        }
        else
        {
            if (fallVelocity >= 0)
            {
                if (!inputDevice.Action1.IsPressed)
                {
                    fallVelocity -= gravity * gravityMultipliyerFalling * Time.deltaTime;
                    movePlayer.y = fallVelocity;
                }
                else
                {
                    fallVelocity -= gravity * Time.deltaTime;
                    movePlayer.y = fallVelocity;
                }
                isOnAir = true;
            }
            else if (fallVelocity < 0)
            {
                //if (isOnAir)
                //{
                //    fallVelocity = -0.01f;
                //    movePlayer.y = fallVelocity;

                    
                //    //contando el tiempo en el aire
                //    timer += Time.deltaTime;

                //    if (timer >= timeOnAir)
                //    {
                //        timer = 0;
                //        isOnAir = false;
                //    }

                //}
                //else
                //{
                    fallVelocity -= gravity * Time.deltaTime * gravityMultipliyerFalling;
                    movePlayer.y = fallVelocity;
                //}
                
            }
        }

    }
    #endregion

    public bool isOnPressurePlate = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            grounded = false;
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Renderer renderer = hit.gameObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            if (renderer.material.color.a == 0)
            {
                iTween.FadeTo(hit.gameObject, 0.5f, 1f);
                StartCoroutine(fadeBack(hit.gameObject));
            }
        }
    }

    
    public void StopMovement(bool _tof)
    {
        stopped = _tof;
        animatorController.SetBool("walking", false);
    }

    public void Whip(Transform destination)
    {
        transform.DOJump(destination.position, -0.5f, 1, 0.5f);
    }

    IEnumerator fadeBack(GameObject gameObject)
    {
        yield return new WaitForSeconds(2f);
        iTween.FadeTo(gameObject, 0f, 1f);

    }
}
