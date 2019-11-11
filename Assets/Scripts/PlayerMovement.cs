using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region VARIABLES
    [Header("MOVEMENT")]
    [SerializeField] float horizontalMove;
    [SerializeField] float verticalMove;
    private Vector3 playerInput;
    private Vector3 movePlayer;

    [SerializeField] CharacterController player;
    [SerializeField] float playerSpeed;

    [Header("JUMP")]
    [SerializeField] float jumpForce;
    [SerializeField] AudioSource jumpSound;

    [Header("GRAVITY")]
    [SerializeField] float gravity = 9.8f;
    [SerializeField] float fallVelocity;

    [Header("CAMERA")]
    [SerializeField] Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;

    [Header("PORTALS")]
    [SerializeField] Transform portal1;

    #endregion


    #region START
    void Start()
    {
        player = GetComponent<CharacterController>();
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
        playerInput = Vector3.ClampMagnitude( playerInput, 1);

        //GETTING CAMERA DIRECTION
        CamDirection();

        // CALCULATING CHARACTER MOVEMENT
        movePlayer = playerInput.x * camRight + playerInput.z * camForward;
        movePlayer *= playerSpeed; 

        // CHARACTER ROTATION (LOOK AT)
        player.transform.LookAt(player.transform.position + movePlayer);

        // GRAVITY
        SetGravity();

        // JUMP
        PlayerSkills();

        // MOVING CHARACTER
        player.Move(movePlayer * Time.deltaTime);
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
        if (player.isGrounded && Input.GetButtonDown("Jump"))
        {
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
            jumpSound.Play();
        }
    }
    #endregion

    #region SET GRAVITY
    void SetGravity()
    {
        if (player.isGrounded)
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
        //if (other.CompareTag("Portal1"))
        //{
        //    movePlayer = new Vector3(0,0,0);
        //    this.transform.position = portal1.position;
        //}
    }
}
