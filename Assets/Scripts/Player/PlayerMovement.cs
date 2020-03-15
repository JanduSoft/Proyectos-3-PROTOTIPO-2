using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    #region VARIABLES
    [Header("MOVEMENT")]
    [SerializeField] float horizontalMove;
    [SerializeField] float verticalMove;
    private Vector3 playerInput;
    public bool grabbedToRock = false;
    public Vector3 movePlayer;  //made public for DragAndDropObject.cs to use
    private bool stopped = false;
    [SerializeField] GameObject walkinParticles;
    [SerializeField] Transform walkinParticlesSpawner;
    public bool grounded = true;
    [SerializeField] public CharacterController player;
    [SerializeField] float playerSpeed;
    [SerializeField] Whip whip;
    [Header("JUMP")]
    [SerializeField] float jumpForce;
    [SerializeField] AudioSource jumpSound;
    float minPitch = 0.9f;
    float maxPitch = 1.1f;
    [SerializeField] float percentRestriction;

    [Header("GRAVITY")]
    [SerializeField] float gravity = 9.8f;
    [SerializeField] public float fallVelocity;

    [Header("CAMERA")]
    [SerializeField] Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;

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
            //GET AXIS
            horizontalMove = Input.GetAxis("Horizontal");
            verticalMove = Input.GetAxis("Vertical");

            playerInput = new Vector3(horizontalMove, 0, verticalMove);
            playerInput = Vector3.ClampMagnitude(playerInput, 1);

            //GETTING CAMERA DIRECTION
            CamDirection();

            // CALCULATING CHARACTER MOVEMENT
            movePlayer = playerInput.x * camRight + playerInput.z * camForward;
            movePlayer *= playerSpeed;
            if (movePlayer == Vector3.zero)
            {
                animatorController.SetBool("walking", false);
                timeIdle += Time.deltaTime;
                animatorController.SetFloat("idle", timeIdle);
                if (timeIdle > 4)
                {
                    animatorController.SetInteger("randomIdle", Random.Range(0, 2));
                    timeIdle = 0;
                }
            }
            else if(!grabbedToRock && movePlayer != Vector3.zero && !stopped)
            {
                

                // LOOK AT IF IS ON AIR OR GROUNDED
                if (player.isGrounded || grounded)
                {
                    player.transform.DOLookAt(player.transform.position + movePlayer, 0.5f);
                    model.transform.position = player.transform.position;
                    model.transform.DOLookAt(player.transform.position + movePlayer, 0.5f);
                }
                else if (!player.isGrounded && !grounded)
                {
                    player.transform.DOLookAt(player.transform.position + movePlayer, 1.5f);
                    model.transform.position = player.transform.position;
                    model.transform.DOLookAt(player.transform.position + movePlayer, 1.5f);
                }

                animatorController.SetBool("walking", true);
                timeIdle = 0;
                animatorController.SetFloat("velocity", Mathf.Abs(Vector3.Dot(movePlayer, Vector3.one)));
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
                    player.Move(movePlayer * Time.deltaTime);

                    Debug.Log("MovePlayer en el suelo : " + movePlayer);
                }
                else if (!player.isGrounded && !grounded)
                {
                    player.Move((movePlayer *(percentRestriction/100)) * Time.deltaTime);
                    animatorController.SetBool("Jumping", true);

                    Debug.Log("MovePlayer en el aire : " + movePlayer/2);
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
        if ((player.isGrounded || grounded) && Input.GetButtonDown("Jump"))
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
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
        }

    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void OnTriggerStay(Collider other)
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
    public void StopMovement(bool _tof)
    {
        stopped = _tof;
    }

    public void Whip(Transform destination)
    {
        transform.DOJump(destination.position, -0.5f, 1, 0.5f);
    }
}
