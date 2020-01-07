using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FollowingCharacter : MonoBehaviour
{
    ////////////////////////////
    /// /////////////////////////////------------------------------VARIABLES
    ////////////////////////////

    [SerializeField] Transform player;
    [SerializeField] Rigidbody targetRB;
    [SerializeField] float diferenceHeight;
    [SerializeField] float cameraSpeed;

    private float angleRotation = 90f;
    [SerializeField] float rotatioTime = 1f;
    [SerializeField] AudioSource rotationAS;

    [Header("WALLS TO HIDE")]
    [SerializeField] GameObject wall1;
    [SerializeField] GameObject wall2;
    [SerializeField] GameObject wall3;
    [SerializeField] GameObject wall4;

    private int corner = 1;
    private bool isRotating = false;
     public bool onLevel1 = true;

    ////////////////////////////
    /// /////////////////////////////------------------------------METHODS
    ////////////////////////////

    /// /////////////////---- START
    private void Start()
    {
        if (onLevel1)
        {
            wall1.SetActive(false);
            wall4.SetActive(false);
        }
    }

    /// /////////////////---- UPDATE
    void Update()
    {
        Vector3 newPosition = new Vector3(player.position.x, player.position.y , player.position.z);
        //targetRB.MovePosition(newPosition);
        transform.DOMove(newPosition, 1f);
        //transform.position = Vector3.Lerp(transform.position, player.position, 0.25f);
        //Commented this because E is now used to interact with levers
        /*if ((Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Trigger1") && !isRotating)
        {
            rotationAS.Play();
            isRotating = true;
            Invoke("StopRotation", rotatioTime);
            //transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y - 90, transform.rotation.z));
            transform.DORotate(new Vector3(0, -angleRotation, 0), rotatioTime, RotateMode.WorldAxisAdd);
            corner++;            
            if (corner > 4)
            {
                corner = 1;
            }
        }
        if ((Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("Trigger2")) && !isRotating)
        {
            rotationAS.Play();
            isRotating = true;
            Invoke("StopRotation", rotatioTime);
            //transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y + 90, transform.rotation.z));
            transform.DORotate(new Vector3( 0, transform.rotation.y + angleRotation, 0), rotatioTime, RotateMode.WorldAxisAdd);
            corner--;
            if (corner <= 0)
            {
                corner = 4;
            }
        }

        /*if (onLevel1)
        {
            switch (corner)
            {
                case 1:
                    {
                        wall1.SetActive(false);
                        wall4.SetActive(false);

                        wall2.SetActive(true);
                        wall3.SetActive(true);

                        break;
                    }
                case 2:
                    {
                        wall2.SetActive(false);
                        wall4.SetActive(false);

                        wall1.SetActive(true);
                        wall3.SetActive(true);

                        break;
                    }
                case 3:
                    {
                        wall2.SetActive(false);
                        wall3.SetActive(false);

                        wall1.SetActive(true);
                        wall4.SetActive(true);

                        break;
                    }
                case 4:
                    {
                        wall1.SetActive(false);
                        wall3.SetActive(false);

                        wall2.SetActive(true);
                        wall4.SetActive(true);

                        break;
                    }

                default:
                    {
                        Debug.Log("ALGO FALLA");
                        break;
                    }
            }

        }*/

        //diferenceHeight = transform.position.y - player.position.y;
        //Debug.Log(diferenceHeight);
        //if (diferenceHeight < 0 && diferenceHeight < -maxHeight)
        //{
        //    transform.Translate(transform.position.x, transform.position.y - cameraSpeed, transform.position.z);
        //}
        //else if (diferenceHeight > 0 && diferenceHeight > maxHeight)
        //{
        //    transform.Translate(transform.position.x, (transform.position.y - cameraSpeed), transform.position.z);
        //}
    }

    /// /////////////////---- STOP ROTATION
    void StopRotation()
    {
        isRotating = false;
    }
}
