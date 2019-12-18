using System.Collections;
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

    Rigidbody rb;


    bool lerping = false;
    bool rockGrabbed = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Recalculate grab points in case the rock has moved
        grabPoints[0] = transform.position + new Vector3(2.5f, -0.5f, 0);
        grabPoints[1] = transform.position + new Vector3(-2.5f, -0.5f, 0);
        grabPoints[2] = transform.position + new Vector3(0, -0.5f, 2.5f);
        grabPoints[3] = transform.position + new Vector3(0, -0.5f, -2.5f);
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
                Vector3 dir = transform.position - player.transform.position;
                float vert = Input.GetAxis("Vertical");
                if (vert>0)
                {
                    rb.velocity = dir.normalized * 150 * Time.deltaTime;
                }
                else if (vert<0)
                {
                    rb.velocity = -dir.normalized * 200 * Time.deltaTime;
                }
                else
                {
                    rb.velocity = Vector3.zero;
                }
            }

            if (GetComponent<Animation>().isPlaying)
            {
                player.GetComponent<PlayerMovement>().StopMovement(false);
                lerping = false;
                rockGrabbed = false;
            }
        }
    }

    void DoLerp()
    {
        //lerp player towards closest point
        player.transform.position = Vector3.Lerp(player.transform.position, closestPoint,0.1f);

        //lerp rotation to face object
        Quaternion targetRotation = Quaternion.LookRotation(transform.position- player.transform.position);
        player.transform.rotation = Quaternion.Lerp(player.transform.rotation, targetRotation, 0.3f);

        if (Vector3.Distance(player.transform.position,closestPoint)<0.8f)
        {
            //stop lerping and look at object
            player.GetComponent<PlayerMovement>().StopMovement(true);
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
    }
}
