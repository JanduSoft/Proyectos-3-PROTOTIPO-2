using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveBoxBridge : MonoBehaviour
{
    bool canActivate = false;

    [SerializeField] Transform box;
    [SerializeField] Transform position1;
    [SerializeField] Transform position2;
    [SerializeField] Transform position3;
    [SerializeField] Transform position4;
    [SerializeField] float speed;
    bool isMoving = false;

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && canActivate)
        {
            if (!isMoving)
            {
                isMoving = true;
                MoveToPosition2();               
            }
        }
    }

    void MoveToPosition1()
    {
        box.DOMove(position1.position, speed);
        Invoke("isMovingToFalse", speed);
    }

    void MoveToPosition2()
    {
        box.DOMove(position2.position, speed);
        Invoke("MoveToPosition3", speed);
    }
    void MoveToPosition3()
    {
        box.DOMove(position3.position, speed);
        Invoke("MoveToPosition1", speed);
    }
    void MoveToPosition4()
    {
        box.DOMove(position4.position, speed);
        Invoke("MoveToPosition1", speed);
    }
    void isMovingToFalse()
    {
        isMoving = false;
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
