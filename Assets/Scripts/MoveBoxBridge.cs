using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using InControl;

public class MoveBoxBridge : MonoBehaviour
{
    [SerializeField] Animator wagonAnimator;
    bool canActivate = false;

    /*[SerializeField] Transform box;
    [SerializeField] Transform position1;
    [SerializeField] Transform position2;
    [SerializeField] Transform position3;
    [SerializeField] Transform position4;
    [SerializeField] float speed;*/
    bool isMoving = false;

    bool isMovingTo1 = false;
    bool isMovingTo2 = false;
    bool isMovingTo3 = false;

    /*[SerializeField] float maxDistance;
    [SerializeField] float speedLookAt = 0.5f;
    float distanceToTarget;*/
    


    private void Update()
    {
        if (InputManager.ActiveDevice.Action3.WasPressed && canActivate)
        {
            if (!isMoving)
            {
                isMoving = true;
                isMovingTo2 = true;
                wagonAnimator.SetBool("Active", true);
                Invoke("isMovingToFalse", 11);
            }
        }

        //if (isMoving)
        //{
        //    if (isMovingTo1)
        //    {
        //        distanceToTarget = Vector3.Distance(position1.position, box.transform.position);
        //        if (distanceToTarget <= maxDistance)
        //        {
        //            box.DOLookAt(position2.position, speedLookAt);
        //        }
        //        return;
        //    }
        //    else if (isMovingTo2)
        //    {
        //        distanceToTarget = Vector3.Distance(position2.position, box.transform.position);
        //        if (distanceToTarget <= maxDistance)
        //        {
        //            box.DOLookAt(position3.position, speedLookAt);
        //        }
        //    }
        //    else if (isMovingTo3)
        //    {
        //        distanceToTarget = Vector3.Distance(position3.position, box.transform.position);
        //        if (distanceToTarget <= maxDistance)
        //        {
        //            box.DOLookAt(position1.position, speedLookAt);
        //        }
        //    }
        //}
        
    }

    //void MoveToPosition1()
    //{
    //    isMovingTo3 = false;
    //    isMovingTo1 = true;
    //    box.DOMove(position1.position, speed);
    //    Invoke("isMovingToFalse", speed);
    //}

    //void MoveToPosition2()
    //{        
    //    box.DOMove(position2.position, speed);
    //    Invoke("MoveToPosition3", speed);
    //}
    //void MoveToPosition3()
    //{
    //    isMovingTo2 = false;
    //    isMovingTo3 = true;
    //    box.DOMove(position3.position, speed);
    //    Invoke("MoveToPosition1", speed);
    //}
    //void MoveToPosition4()
    //{
    //    box.DOMove(position4.position, speed);
    //    Invoke("MoveToPosition1", speed);
    //}
    void isMovingToFalse()
    {
        isMoving = false;
        wagonAnimator.SetBool("Active", false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canActivate = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canActivate = false;
        }
    }
}
