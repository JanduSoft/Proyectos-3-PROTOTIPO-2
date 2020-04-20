using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using InControl;

public class NewWhipJump : MonoBehaviour
{
    #region VARIABLES
    [Header("MOVEMENT")]
    [SerializeField] Transform destination;
    [SerializeField] float speed;
    [SerializeField] float jump;

    [SerializeField] float impulse;
    [SerializeField] float timeImpulse;
    bool canWhip = false;
    bool isPlayerInside = false;

    [Header("EXTERN VARIABLES")]
    [SerializeField] Transform toWhipObject;
    [SerializeField] Transform player;

    [Header("MARK")]
    [SerializeField] float maxDistance;
    [SerializeField] GameObject spriteIndicateObject;
    [SerializeField] SpriteRenderer markSprite;
    [Header("ANIMATION CRESSHAIR")]
    bool startedAnimation = false;
    [SerializeField] float speedAnimation;
    [SerializeField] float minScale;
    [SerializeField] float maxScale;
    [SerializeField] AudioSource crosshairSound;
    Color crosshairColor;
    Vector3 originalScale;
    Vector3 originalPosition;
    [Header("DRAW WHIP")]
    [SerializeField] Animator animator;
    [SerializeField] LineRenderer whip;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Transform hookSpawner;
    bool startWhip = false;
    #endregion

    private void Start()
    {
        DOTween.SetTweensCapacity(500, 500);
        //////CROSSHAIR
        player = GameObject.Find("Character").gameObject.transform;
        crosshairColor = markSprite.color;
        originalScale = spriteIndicateObject.transform.localScale;
        originalPosition = whip.gameObject.transform.position;
        /////WHIP
        whip.SetPosition(0, originalPosition);
        whip.SetPosition(1, player.position);
        whip.startColor = Color.blue;
        whip.endColor = Color.blue;
    }

    #region UPDATE
    private void Update()
    {
        ///////controlling line renderer
        if (!playerMovement.isInWhipJump)
        {
            whip.SetPosition(0, whip.gameObject.transform.position);
            whip.SetPosition(1, whip.gameObject.transform.position);
        }
        else if (startWhip)
        {
            whip.SetPosition(0, whip.gameObject.transform.position);
            whip.SetPosition(1, hookSpawner.position);
        }

        ///UPDATE HOOKSPAWNER POSITION
        if (isPlayerInside && !startWhip)
        {
            hookSpawner.position = new Vector3(player.position.x, player.position.y + 2f, player.position.z);
        }
        

        ////CHECK INPUT
        if (InputManager.ActiveDevice.Action4.WasPressed && canWhip && !playerMovement.isInWhipJump)
        {
            Vector3 toLookAt = new Vector3(toWhipObject.position.x, player.position.y, toWhipObject.position.z);

            player.DOLookAt(toLookAt, timeImpulse + 0.2f);
            player.DOMoveY(player.position.y + impulse, timeImpulse);

            //mover el hookSpawner
            startWhip = true;
            hookSpawner.DOMove(toWhipObject.position, timeImpulse);

            playerMovement.isInWhipJump = true;
            animator.SetBool("Whip", true);
            animator.SetBool("Jumping", true);
            playerMovement.grounded = false;
            Invoke("WhipJump", timeImpulse);
        }

        /////CHECKING IF IS IN WHIP JUP FOR DE LINE RENDERER
        if (playerMovement.isInWhipJump)
        {
            //Vector3 aux = new Vector3(player.position.x, player.position.y + 3f, player.position.z);
            whip.SetPosition(0, whip.gameObject.transform.position);
        }



        //////CHECK IF YOU CAN WHIP
        if (Vector3.Distance(player.position, toWhipObject.position) <= maxDistance && isPlayerInside)
        {
            spriteIndicateObject.SetActive(true);
            canWhip = true;

            crosshairColor.a = 1f;
            markSprite.color = crosshairColor;

            if (!startedAnimation)
            {
                if (!playerMovement.isInWhipJump)
                {
                    crosshairSound.Play();
                }

                startedAnimation = true;
                IncreasetAnimationCrosshair();
            }
        }
        else if (Vector3.Distance(player.position, toWhipObject.position) >= maxDistance && isPlayerInside)
        {
            canWhip = false;
            startedAnimation = false;
        }

        //////CHECKING ALPHA OF CROSSHAIR
        if (isPlayerInside && !canWhip)
        {
            crosshairColor.a = 0.2f;
            markSprite.color = crosshairColor;
            spriteIndicateObject.transform.localScale = originalScale;
        }

    }
    #endregion

    #region WHIP JUMP
    void WhipJump()
    {
        whip.SetPosition(1, toWhipObject.position);
        player.DOJump(destination.position, -3 , 1, speed);
        //player.DOMove(destination.position, speed);
        StartCoroutine(WhipAnimationFalse());
        Invoke("StopWhipDrawing", speed );
    }
    #endregion

    #region RESET TIME SACALE
    void ResetTimeSacale()
    {
        Time.timeScale = 1f;
    }
    #endregion

    #region STOP WHIP DRAWING
    void StopWhipDrawing()
    {
        playerMovement.isInWhipJump = false;
        startWhip = false;
        //whip.SetPosition(1, player.position);
        whip.SetPosition(0, player.position);
    }
    #endregion

    #region WHIP JUMP ACTIVATE
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            //spriteIndicateObject.transform.position = whipableObject.position;
            spriteIndicateObject.SetActive(true);
            //canWhip = true;
        }
    }
    #endregion

    #region WHIP JUMP DEACTIVATE
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            spriteIndicateObject.SetActive(false);
            canWhip = false;
            startedAnimation = false;

            //RESTART HOOKSPAWNER POSITION
        }
    }
    #endregion
    
    #region INCREASE ANIMATION CROSSHAIR
    void IncreasetAnimationCrosshair()
    {
        spriteIndicateObject.transform.DOScale(maxScale,speedAnimation);
        if (canWhip)
        {
            Invoke("DecreaseAnimationCrosshair", speedAnimation);
        }
    }
    #endregion

    #region DECREASE ANIMATION CROSSHAIR
    void DecreaseAnimationCrosshair()
    {
        spriteIndicateObject.transform.DOScale(minScale, speedAnimation);
        if (canWhip)
        {
            Invoke("IncreasetAnimationCrosshair", speedAnimation);
        }
    }
    #endregion

    IEnumerator WhipAnimationFalse()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Whip", false);

    }
}
