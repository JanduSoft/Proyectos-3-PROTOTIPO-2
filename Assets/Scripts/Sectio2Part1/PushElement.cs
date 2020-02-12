using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PushElement : MonoBehaviour
{
    [SerializeField] Transform box;
    [SerializeField] PushElement destinationElement;
    [SerializeField] float speed;
    [SerializeField] Transform destination;
    public bool canPush = true;
    public bool isInside = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && isInside && canPush)
        {
            box.DOMove(destination.position, speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Block"))
        {
            destinationElement.canPush = false;
        }
        if (other.CompareTag("Player"))
        {
            isInside = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            destinationElement.canPush = true;
        }
        if (other.CompareTag("Player"))
        {
            isInside = false;
        }
    }
}
