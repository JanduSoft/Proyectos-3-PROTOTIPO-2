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
    private Vector3 movePlayer;
    private bool stopped = false;

    [SerializeField] CharacterController player;
    [SerializeField] float playerSpeed;
    [SerializeField] Whip whip;
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
    private Transform portal1;
    private bool moveToPortal1 = false;
    private Transform portal2;
    private bool moveToPortal2 = false;
    private Transform portal3;
    private bool moveToPortal3 = false;
    private Transform portal4;
    private bool moveToPortal4 = false;
    private Transform portal5;
    private bool moveToPortal5 = false;
    private Transform portal6;
    private bool moveToPortal6 = false;

    #endregion


    #region START
    void Start()
    {
        player = GetComponent<CharacterController>();
        fallVelocity = -10;
    }
    #endregion

    #region UPDATE
    // Update is called once per frame
    void Update()
    {
        if (moveToPortal1)
        {
            transform.position = portal1.position;
            moveToPortal1 = false;
        }
        else if (moveToPortal2)
        {
            transform.position = portal2.position;
            moveToPortal2 = false;
        }
        else if (moveToPortal3)
        {
            transform.position = portal3.position;
            moveToPortal3 = false;
        }
        else if (moveToPortal4)
        {
            transform.position = portal4.position;
            moveToPortal4 = false;
        }
        else if (moveToPortal5)
        {
            transform.position = portal5.position;
            moveToPortal5 = false;
        }
        else if (moveToPortal6)
        {
            transform.position = portal6.position;
            moveToPortal6 = false;
        }
        else
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

            if (!stopped)
            {
                // CHARACTER ROTATION (LOOK AT)
                player.transform.LookAt(player.transform.position + movePlayer);

                // GRAVITY
                SetGravity();

                // JUMP
                PlayerSkills();

                // MOVING CHARACTER

                player.Move(movePlayer * Time.deltaTime);
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
       if (other.CompareTag("Portal1"))
       {
            Debug.Log("Entrando al portal 1");
            moveToPortal1 = true;
       }
       else if (other.CompareTag("Portal2"))
       {
            Debug.Log("Entrando al portal 2");
            moveToPortal2 = true;
       }
       else if (other.CompareTag("Portal3"))
       {
            Debug.Log("Entrando al portal 3");
            moveToPortal3 = true;
       }
       else if (other.CompareTag("Portal4"))
       {
            Debug.Log("Entrando al portal 4");
            moveToPortal4 = true;
       }
       else if (other.CompareTag("Portal5"))
       {
           Debug.Log("Entrando al portal 5");
           moveToPortal5 = true;
       }
       else if (other.CompareTag("Portal6"))
       {
           Debug.Log("Entrando al portal 6");
           moveToPortal6 = true;
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
