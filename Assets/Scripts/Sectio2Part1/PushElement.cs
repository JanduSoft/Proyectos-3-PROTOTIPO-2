using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using InControl;

public class PushElement : MonoBehaviour
{
    [SerializeField] Transform box;
    [SerializeField] PushElement destinationElement;
    [SerializeField] float speed;
    [SerializeField] AudioSource movingSound;
    [SerializeField] Transform destination;
    public bool canPush = true;
    public bool isInside = false;
    bool isMoving = false;

    bool isBlocked = false;
    // Update is called once per frame
    void Update()
    {
        if (InputManager.ActiveDevice.Action3.WasPressed && isInside && canPush && !isMoving)
        {
            box.DOMove(destination.position, speed);
            isMoving = true;
            movingSound.Play();
            StartCoroutine(sound());
            Invoke("RestartMoving", speed);
        }

        if (isBlocked)
        {

            destinationElement.canPush = false;
        }
        else
        {
            destinationElement.canPush = true;
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
            Debug.Log("Se Deberia Poder Mover");
            destinationElement.canPush = true;
        }
        if (other.CompareTag("Player"))
        {
            isInside = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            isBlocked = true;
        }
    }

    void RestartMoving()
    {
        isMoving = false;
    }
    IEnumerator sound()
    {
        yield return new WaitForSeconds(speed);
        movingSound.Stop();
    }
}
