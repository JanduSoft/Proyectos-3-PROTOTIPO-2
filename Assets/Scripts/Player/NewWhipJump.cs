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
    [Header("DRAW WHIP")]
    [SerializeField] LineRenderer whip;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Transform hookSpawner;
    bool startWhip = false;
    bool isRelatedToThis = false;

    [Header("PARABOLA")]
    [SerializeField] Transform startPosition;
    [SerializeField] Transform midlePosition;
    [SerializeField] Transform endPosition;
    [SerializeField] Transform sphere;
    [SerializeField] ParabolaController objetToDoParabola;

    #endregion

    #region START
    private void Start()
    {
        //////CROSSHAIR
        player = GameObject.Find("Character").gameObject.transform;
        crosshairColor = markSprite.color;
        originalScale = spriteIndicateObject.transform.localScale;

        /////WHIP
        whip.SetPosition(0, player.position);
        whip.SetPosition(1, player.position);
        whip.startColor = Color.blue;
        whip.endColor = Color.blue;

        //PARABOLA
        endPosition.position = destination.position;
    }
    #endregion

    #region UPDATE
    private void Update()
    {
        ///////controlling line renderer
        if (!playerMovement.isInWhipJump && isPlayerInside)
        {
            whip.SetPosition(0, player.position);
            whip.SetPosition(1, player.position);

            startPosition.position = player.position;
            sphere.position = player.position;

            float xDiference =  startPosition.position.x - endPosition.position.x;
           
            midlePosition.position = new Vector3(startPosition.position.x  - xDiference/2,
                                                 startPosition.position.y + 1.5f, 
                                                 toWhipObject.transform.position.z);

        }
        else if (startWhip)
        {
            whip.SetPosition(0, player.position);
            whip.SetPosition(1, hookSpawner.position);           
            
        }

        if (playerMovement.isInWhipJump && isRelatedToThis)
        {
            player.position = sphere.transform.position;
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

            //playerMovement.isInWhipJump = true;
            Invoke("WhipJump", timeImpulse);
        }

        /////CHECKING IF IS IN WHIP JUMP FOR DE LINE RENDERER
        if (playerMovement.isInWhipJump)
        {
            Vector3 aux = new Vector3(player.position.x, player.position.y + 3f, player.position.z);
            whip.SetPosition(0, aux);
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

        //CHECKING SOTP TO DRAW WHIP
        if (playerMovement.isInWhipJump && !startWhip && isRelatedToThis)
        {
            whip.SetPosition(1, player.position);
            whip.SetPosition(0, player.position);
        }

    }
    #endregion

    #region WHIP JUMP
    void WhipJump()
    {
        playerMovement.isInWhipJump = true;
        //Start Movement
        objetToDoParabola.FollowParabola();

        whip.SetPosition(1, toWhipObject.position);
        //player.DOJump(destination.position, jump, 1, speed);
        //player.DOMove(destination.position, speed);
        
        Invoke("StopWhipDrawing", speed / 2);
        Invoke("RestartVariables", speed);
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
        //playerMovement.isInWhipJump = false;
        startWhip = false;
        whip.SetPosition(1, player.position);
        whip.SetPosition(0, player.position);
    }
    #endregion

    #region RELATING SPHERE
    void RelatingSphere()
    {
        isRelatedToThis = true;
    }
    #endregion

    #region RESTART VARIABLES
    void RestartVariables()
    {
        startWhip = false;
        playerMovement.isInWhipJump = false;
        isRelatedToThis = false;
    }
    #endregion

    #region TRIGGER ENTER
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            //spriteIndicateObject.transform.position = whipableObject.position;
            spriteIndicateObject.SetActive(true);
            //canWhip = true;

            if (!playerMovement.isInWhipJump)
            {
                isRelatedToThis = true;
            }
            else if (playerMovement.isInWhipJump)
            {
                Invoke("RelatingSphere", speed/2);
            }
        }
    }
    #endregion

    #region TRIGGER EXIT
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            spriteIndicateObject.SetActive(false);
            canWhip = false;
            startedAnimation = false;

            //RESTART HOOKSPAWNER POSITION
            whip.SetPosition(1, player.position);
            whip.SetPosition(0, player.position);

            if (!playerMovement.isInWhipJump)
            {
                isRelatedToThis = false;
            }
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
}
