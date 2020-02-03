﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropObject : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player = null;
    public float MinDistanceToGrabObject;
    Vector3[] grabPoints = new Vector3[4];
    Vector3 closestPoint;
    int minPoint = -1;
    public float yOffset=0.5f;

    Rigidbody rb;


    bool lerping = false;
    bool rockGrabbed = false;

    //AUDIO VARIABLES
    bool canDoSound = true;
    bool movingRock = false;
    [HideInInspector] public AudioSource dragSound;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        try
        {
            dragSound = transform.parent.gameObject.transform.Find("Drag sound").GetComponent<AudioSource>();
        }
        catch
        {
            canDoSound = false;
            Debug.Log("Can't find 'Drag sound' object. Make sure it's attached as a DraggableObject sibbling.");
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Recalculate grab points in case the rock has moved

        grabPoints[0] = transform.position + transform.forward * 3;
        grabPoints[1] = transform.position - transform.forward * 3;
        grabPoints[2] = transform.position + transform.right*3;
        grabPoints[3] = transform.position - transform.right*3;

        grabPoints[0].y = transform.position.y - yOffset;
        grabPoints[1].y = transform.position.y - yOffset;
        grabPoints[2].y = transform.position.y - yOffset;
        grabPoints[3].y = transform.position.y - yOffset;

        if (minPoint != -1)
            closestPoint = grabPoints[minPoint];

        if (player!=null)
        {
            if (Input.GetButtonDown("Interact") && Vector3.Distance(transform.position,player.transform.position)<MinDistanceToGrabObject)
            {
                if (!rockGrabbed)
                {
                    //FIND CLOSEST POINT
                    closestPoint = FindClosestPoint();
                    //LERP PLAYER TOWARDS POINT
                    lerping = true;
                }
                else
                {
                    //Let go of rock
                    player.GetComponent<PlayerMovement>().StopMovement(false);
                    lerping = false;
                    rockGrabbed = false;
                }

            }
            else if (lerping)
            {
                DoLerp();
            }
            else if (rockGrabbed)
            {
                player.transform.position = closestPoint;
                float horizontalMove = Input.GetAxis("Horizontal");
                float verticalMove = Input.GetAxis("Vertical");

                bool inputActive = (horizontalMove > 0.1f || horizontalMove<-0.1f) && (verticalMove>0.1f || verticalMove<-0.1f);

                if (closestPoint == grabPoints[0])
                {
                    Vector3 movingDirection = grabPoints[1] - player.transform.position;

                    if (Vector3.Angle(movingDirection, player.GetComponent<PlayerMovement>().movePlayer)<30 && inputActive)
                    {
                        rb.velocity = movingDirection.normalized * Time.deltaTime * 150;
                        movingRock = true;
                    }
                    else if (Vector3.Angle(-movingDirection, player.GetComponent<PlayerMovement>().movePlayer)<30 && inputActive)
                    {
                        rb.velocity = -movingDirection.normalized * Time.deltaTime * 150;
                        movingRock = true;
                    }
                    else
                    {
                        movingRock = false;
                    }
                }
                else if (closestPoint == grabPoints[1])
                {
                    Vector3 movingDirection = grabPoints[0] - player.transform.position;

                    if (Vector3.Angle(movingDirection, player.GetComponent<PlayerMovement>().movePlayer) < 30 && inputActive)
                    {
                        rb.velocity = movingDirection.normalized * Time.deltaTime * 150;
                        movingRock = true;
                    }
                    else if (Vector3.Angle(-movingDirection, player.GetComponent<PlayerMovement>().movePlayer) < 30 && inputActive)
                    {
                        rb.velocity = -movingDirection.normalized * Time.deltaTime * 150;
                        movingRock = true;
                    }
                    else
                    {
                        movingRock = false;
                    }
                }
                else if (closestPoint == grabPoints[2])
                {
                    Vector3 movingDirection = grabPoints[3] - player.transform.position;

                    if (Vector3.Angle(movingDirection, player.GetComponent<PlayerMovement>().movePlayer) < 30 && inputActive)
                    {
                        rb.velocity = movingDirection.normalized * Time.deltaTime * 150;
                        movingRock = true;
                    }
                    else if (Vector3.Angle(-movingDirection, player.GetComponent<PlayerMovement>().movePlayer) < 30 && inputActive)
                    {
                        rb.velocity = -movingDirection.normalized * Time.deltaTime * 150;
                        movingRock = true;
                    }
                    else
                    {
                        movingRock = false;
                    }
                }
                else if (closestPoint == grabPoints[3])
                {
                    Vector3 movingDirection = grabPoints[2] - player.transform.position;

                    if (Vector3.Angle(movingDirection, player.GetComponent<PlayerMovement>().movePlayer) < 30 && inputActive)
                    {
                        rb.velocity = movingDirection.normalized * Time.deltaTime * 150;
                        movingRock = true;
                    }
                    else if (Vector3.Angle(-movingDirection, player.GetComponent<PlayerMovement>().movePlayer) < 30 && inputActive)
                    {
                        rb.velocity = -movingDirection.normalized * Time.deltaTime * 150;
                        movingRock = true;
                    }
                    else
                    {
                        movingRock = false;
                    }
                }


            }

            if (GetComponent<Animation>().isPlaying)
            {
                //player.GetComponent<PlayerMovement>().StopMovement(false);
                lerping = false;
                rockGrabbed = false;
                movingRock = false;
                if (canDoSound) dragSound.Stop();
            }

            if (canDoSound)
            {
                if (movingRock && !dragSound.isPlaying)
                {
                    //play audio
                    dragSound.Play();
                }
                else if (!movingRock)
                {
                    //stop audio
                    dragSound.Stop();
                }
            }
        }
    }

    void DoLerp()
    {
        player.GetComponent<PlayerMovement>().StopMovement(true);
        //lerp player towards closest point
        player.transform.position = Vector3.Lerp(player.transform.position, closestPoint,0.1f);

        //lerp rotation to face object
        Quaternion targetRotation = Quaternion.LookRotation(transform.position- player.transform.position);
        player.transform.rotation = Quaternion.Lerp(player.transform.rotation, targetRotation, 0.3f);

        if (Vector3.Distance(player.transform.position,closestPoint)<0.8f)
        {
            //stop lerping and look at object
            lerping = false;
            rockGrabbed = true;
        }
    }

    Vector3 FindClosestPoint()
    {
        //FINDS CLOSEST POINT
        float minDistance = -1;
        for (int i=0; i<4;i++)
        {
            float dist = Vector3.Distance(player.transform.position, grabPoints[i]);
            if (minDistance==-1 || dist<minDistance)
            {
                minPoint = i;
                minDistance = dist;
            }
        }

        return grabPoints[minPoint];

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
            //player = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, MinDistanceToGrabObject);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(grabPoints[0], 0.5f);
        Gizmos.DrawSphere(grabPoints[1], 0.5f);
        Gizmos.DrawSphere(grabPoints[2], 0.5f);
        Gizmos.DrawSphere(grabPoints[3], 0.5f);

        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward*10);
    }
}
