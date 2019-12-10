using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    ////////////////////////////
    /// /////////////////////////////------------------------------VARIABLES
    ////////////////////////////
           
       [Header("Player")]
        ///---Player---\\\
    [SerializeField] float speedMovement;
    [SerializeField] Rigidbody myRB;
    [SerializeField] Vector3 movement;
    [SerializeField] float jumpForce;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] CapsuleCollider myCollider;
    private Vector3 playerInput;
    private Vector3 playerDirection;
    private bool previousIsGrounded;
    private bool grounded;
    [SerializeField] bool onLadder = false;
    public bool isInside = false;
    [SerializeField] Climbing climbinDirection = Climbing.NONE;
    [SerializeField] float speedClimbing;

    public bool onWhip = false;

    enum Climbing
    {
        NONE,
        UP,
        DOWN
    }

        [Header("Whip")]
        ///---Whip---\\\
    [SerializeField] GameObject whip;
    [SerializeField] Transform whipHandler;

        [Header("Camer")]
        ///---Camera---\\\
    [SerializeField] Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;
    private Vector3 previousCamForward;
    private Vector3 previousCamRight;

    ////////////////////////////
    /// /////////////////////////////------------------------------METHODS
    ////////////////////////////

    /// /////////////////---- START
    void Start()
    {
        myRB = this.GetComponent<Rigidbody>();
        myCollider = this.GetComponent<CapsuleCollider>();
        previousIsGrounded = true;
    }

    /// /////////////////---- UPDATE
    void Update()
    {

        ///movement
        #region Movement
        //if (grounded)
        //{
        //    if (!previousIsGrounded)
        //    {
        //        movement = new Vector3(0, 0, 0);
        //    }
        //    else
        //    {
               movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //}

            //previousIsGrounded = true;

            playerInput = new Vector3(movement.x, 0, movement.z);
            playerInput = Vector3.ClampMagnitude(playerInput, 1);

        //}
        //else
        //{
        //    previousIsGrounded = false;
        //}
        #endregion
        ///CheckingCamera Direction
        #region Checking Camera Direction
        CamDirection();
        if (grounded)
        {
            playerDirection = playerInput.x * camRight + playerInput.z * camForward;
            previousCamForward = camForward;
            previousCamRight = camRight;
        }
        else
        {
            playerDirection = playerInput.x * previousCamRight + playerInput.z * previousCamForward;
        }
        transform.LookAt(transform.position + playerDirection);
        #endregion
        ///Jump
        #region Jump
        if (grounded && Input.GetButtonDown("Jump") && onLadder == false )
        {
            Jump();
        }
        #endregion
        ///Whip
        #region Whip
        if (Input.GetMouseButtonDown(0))
        {            
            //Destroy(Instantiate(whip, whipHandler.position, transform.rotation), 0.2f);            
        }
        #endregion
        //Ladder
        #region Ladder
        if (isInside)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (onLadder) onLadder = false;
                else onLadder = true;
            }
        }
        else
        {
            onLadder = false;
        }

        if (onLadder)
        {
            if (Input.GetKey(KeyCode.W))
            {
                climbinDirection = Climbing.UP;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                climbinDirection = Climbing.DOWN;
            }
            else
            {
                climbinDirection = Climbing.NONE;
            }
        }
        #endregion
        //Restarts
        #region Restart
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("MainScene");
        }
        #endregion
    }

    /// /////////////////----FIXED UPDATE
    private void FixedUpdate()
    {
        if (!onLadder && !onWhip)
        {
            MoveCharacter(playerDirection);
        }
        else
        {
            ClimbLadder();
        }
    }

    /// /////////////////----MOVE CHARACTER
    void MoveCharacter(Vector3 _direction)
    {
        if (onWhip == false)
        {
            myRB.MovePosition(transform.position + (_direction * 10 * Time.deltaTime));
        }
        
    }
    /// /////////////////----MOVE CHARACTER
    void ClimbLadder()
    {
        Vector3 climbDirection;
        switch (climbinDirection)
        {

            case Climbing.NONE:
                {
                    climbDirection = new Vector3(0, 0, 0);
                    break;
                }
            case Climbing.UP:
                {
                    climbDirection = new Vector3(0, 2, 0);
                    break;
                }
            case Climbing.DOWN:
                {
                    climbDirection = new Vector3(0, -1, 0);
                    break;
                }
            default:
                {
                    climbDirection = new Vector3(0, 0, 0);
                    Debug.Log(" SOMETHING IS WRONG");
                    break;
                }
        }

        myRB.MovePosition(transform.position + (climbDirection * speedClimbing * Time.deltaTime));
    }
    /// /////////////////----JUMP
    public void Jump()
    {
        grounded = false;
        myRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    /// /////////////////---- CAMERA DIRECTION 
    void CamDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }
    /// /////////////////---- IS GROUNDED 
    private bool IsGrounded()
    {
       return Physics.CheckCapsule( myCollider.bounds.center, 
                                    new Vector3(myCollider.center.x, myCollider.bounds.min.y, myCollider.bounds.center.z),
                                    myCollider.radius * 1.1f, 
                                    groundLayer);
       
    }
    /// /////////////////----JUMP
    void JumpCharacter()
    {
        myRB.AddForce(new Vector3(0, jumpForce,0 ));
    }

    /// /////////////////----CHECK GROUND
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            if (previousIsGrounded == false)
            {
                previousIsGrounded = true;
                grounded = true;
            }
            else
            {                
                grounded = true;
            }
        }
    }

}
