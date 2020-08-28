using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlatformMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Vector3 position1, position2;
    [SerializeField] Vector3 lastCheckpoint;
    [SerializeField] float speed;
    [SerializeField] bool NeverStop = false;
    [SerializeField] bool isActive = false;
    Rigidbody rb;
    bool toPos2 = true;
    GameObject player;
    playerDeath playerDeathScript;
    [Header("Smart respawn")]
    [SerializeField] bool smartRespawn = true;
    [SerializeField] bool doAffect = true;
    [Header("Optional - Lever")]
    [SerializeField] GameObject lever;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Character");
        playerDeathScript = player.GetComponent<playerDeath>();
    }

    void ResetToLastPosition()
    {

        if (!NeverStop || !isActive || smartRespawn)
        {
            player.transform.SetParent(null);
            transform.DOKill();
            lastCheckpoint = playerDeathScript.lastSpawnPointTouched;

            transform.position = Vector3.Distance(position1, lastCheckpoint) < Vector3.Distance(position2, lastCheckpoint) ? position1 : position2;
            if (lever!=null)
            {
                lever.GetComponent<LeverScript>().ResetLeverToPos1();
                isActive = false;
            }
                
        }
        else if (!smartRespawn)
        {
            transform.position = position2;
        }
    }

    void Update()
    {

        if (player.GetComponent<playerDeath>().isDead && doAffect) 
            ResetToLastPosition();
        else
        {
            //Use this to check if the platform has reached its destination

            //If the platform has reached position 2, set position1 as new destination
            //If the platform never stops, just make the platform go to position 1 automatically
            //If the platform does stop when reaching the end, just set velocity to zero
            if (NeverStop && isActive)
            {
                //When reaches the destination, sends to the other destination
                if (toPos2 && CheckHasReachedDestination(position2))
                {
                    toPos2 = false;
                    isActive = true;
                    MovePlatform();
                }
                else if (!toPos2 && CheckHasReachedDestination(position1))
                {
                    toPos2 = true;
                    isActive = true;
                    MovePlatform();
                }
            }
            else
            {
                //When reaches the destination, stops
                if (toPos2 && CheckHasReachedDestination(position2))
                {
                    toPos2 = false;
                    isActive = false;
                    StopPlatform();
                }
                else if (!toPos2 && CheckHasReachedDestination(position1))
                {
                    toPos2 = true;
                    isActive = false;
                    StopPlatform();
                }
            }

            float distanceToPlatform = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlatform <2.8f)
            {
            }
            else if (distanceToPlatform >= 2.8f && distanceToPlatform < 3.5f)
            {
            }
        }

    }

    bool CheckHasReachedDestination(Vector3 destination)
    {
        return (transform.position == destination || Vector3.Distance(transform.position, destination) < 0.5);
    }

    void StopPlatform()
    {
        //If it's not activated, leave the velocity at zero so the platform is stopped
        //rb.velocity = Vector3.zero;
        transform.DOKill();
    }

    void MovePlatform()
    {
        float dist;
        Vector3 toPos;
        if (toPos2)
        {
            dist = (position2 - transform.position).magnitude;
            toPos = position2;
        }
        else
        {
            dist = (position1 - transform.position).magnitude;
            toPos = position1;
        }
        var time = dist / speed; // this number is the number of seconds.
        transform.DOMove(toPos, time, false).SetEase(Ease.Linear);
        //rb.velocity = dir * speed;
    }

    public void ActivateObject()
    {
        //When the function is called from the lever, check if the platform is activated or not
        isActive = isActive ? false : true;

        //If it's activated, decide which direction it has to take
        if(isActive)
        {
            MovePlatform();
        }
        else
        {
            //If it's not activated, leave the velocity at zero so the platform is stopped
            StopPlatform();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            player.transform.SetParent(transform);
            Debug.Log("parented!");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.transform.SetParent(transform);
            Debug.Log("parented!");
        }
    }
   
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.transform.SetParent(null);
            Debug.Log("unparented!");
        }
    }
}
