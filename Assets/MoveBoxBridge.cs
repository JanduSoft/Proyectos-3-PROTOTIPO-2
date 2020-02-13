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
    [SerializeField] float speed;
    int position = 1;

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && canActivate)
        {
            if (position == 1)
            {
                position = 2;
                box.DOMove(position2.position, speed);
            }
            else
            {
                position = 1;
                box.DOMove(position1.position, speed);
            }
        }
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
