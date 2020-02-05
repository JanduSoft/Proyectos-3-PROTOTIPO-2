using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DragAndDropObject : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player = null;
    [SerializeField] float MinDistanceToGrabObject;
    [SerializeField] float grabPointHeight = 0.5f;
    [SerializeField] float stoneSpeed=150;
    Vector3[] grabPoints = new Vector3[4];
    Vector3 closestPoint;
    int minPoint = -1;

    Rigidbody rb;


    bool lerping = false;
    bool rockGrabbed = false;
    bool lockVertical, lockHorizontal = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //calculate the grab points every frame in case the rock moves
        grabPoints[0] = transform.position + transform.forward * 3;
        grabPoints[1] = transform.position - transform.forward * 3;
        grabPoints[2] = transform.position + transform.right * 3;
        grabPoints[3] = transform.position - transform.right * 3;

        //put the points in the desired height
        grabPoints[0].y = transform.position.y - grabPointHeight;
        grabPoints[1].y = transform.position.y - grabPointHeight;
        grabPoints[2].y = transform.position.y - grabPointHeight;
        grabPoints[3].y = transform.position.y - grabPointHeight;

        //if minpoint isn't null, grab the closest point to the player
        if (minPoint != -1)
            closestPoint = grabPoints[minPoint];

        if (player != null)
        {
            if (Input.GetButtonDown("Interact") && Vector3.Distance(transform.position, player.transform.position) < MinDistanceToGrabObject)
            {
                if (!rockGrabbed)
                {
                    //FIND CLOSEST POINT
                    closestPoint = FindClosestPoint();
                    player.transform.DOMove(closestPoint,0.5f,false);
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

                bool inputActive = horizontalMove!=0 || verticalMove!=0;

                if (closestPoint == grabPoints[0])
                {
                    Vector3 movingDirection = grabPoints[1] - player.transform.position;

                    if (Vector3.Angle(movingDirection, player.GetComponent<PlayerMovement>().movePlayer) <= 30 && inputActive)
                    {
                        rb.velocity = movingDirection.normalized * Time.deltaTime * stoneSpeed;
                    }
                    else if (Vector3.Angle(-movingDirection, player.GetComponent<PlayerMovement>().movePlayer) < 30 && inputActive)
                    {
                        rb.velocity = -movingDirection.normalized * Time.deltaTime * stoneSpeed;
                    }
                }
                else if (closestPoint == grabPoints[1])
                {
                    Vector3 movingDirection = grabPoints[0] - player.transform.position;

                    if (Vector3.Angle(movingDirection, player.GetComponent<PlayerMovement>().movePlayer) <= 30 && inputActive)
                    {
                        rb.velocity = movingDirection.normalized * Time.deltaTime * stoneSpeed;
                    }
                    else if (Vector3.Angle(-movingDirection, player.GetComponent<PlayerMovement>().movePlayer) < 30 && inputActive)
                    {
                        rb.velocity = -movingDirection.normalized * Time.deltaTime * stoneSpeed;
                    }
                }
                else if (closestPoint == grabPoints[2])
                {
                    Vector3 movingDirection = grabPoints[3] - player.transform.position;

                    if (Vector3.Angle(movingDirection, player.GetComponent<PlayerMovement>().movePlayer) <= 30 && inputActive)
                    {
                        rb.velocity = movingDirection.normalized * Time.deltaTime * stoneSpeed;
                    }
                    else if (Vector3.Angle(-movingDirection, player.GetComponent<PlayerMovement>().movePlayer) < 30 && inputActive)
                    {
                        rb.velocity = -movingDirection.normalized * Time.deltaTime * stoneSpeed;
                    }
                }
                else if (closestPoint == grabPoints[3])
                {
                    Vector3 movingDirection = grabPoints[2] - player.transform.position;

                    if (Vector3.Angle(movingDirection, player.GetComponent<PlayerMovement>().movePlayer) <= 30 && inputActive)
                    {
                        rb.velocity = movingDirection.normalized * Time.deltaTime * stoneSpeed;
                    }
                    else if (Vector3.Angle(-movingDirection, player.GetComponent<PlayerMovement>().movePlayer) < 30 && inputActive)
                    {
                        rb.velocity = -movingDirection.normalized * Time.deltaTime * stoneSpeed;
                    }
                }


            }

            if (GetComponent<Animation>().isPlaying)
            {
                //player.GetComponent<PlayerMovement>().StopMovement(false);
                lerping = false;
                rockGrabbed = false;
            }
        }
    }

    void DoLerp()
    {
        player.GetComponent<PlayerMovement>().StopMovement(true);
        //lerp player towards closest point
        //player.transform.position = Vector3.Lerp(player.transform.position, closestPoint, 0.1f);


        //lerp rotation to face object
        Quaternion targetRotation = Quaternion.LookRotation(transform.position - player.transform.position);
        player.transform.rotation = targetRotation;

        if (Vector3.Distance(player.transform.position, closestPoint) < 0.8f)
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
        for (int i = 0; i < 4; i++)
        {
            float dist = Vector3.Distance(player.transform.position, grabPoints[i]);
            if (minDistance == -1 || dist < minDistance)
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
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 10);
    }
}
