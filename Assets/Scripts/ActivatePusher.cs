using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using InControl;

public class ActivatePusher : MonoBehaviour
{
    [Header("FIRST ROTATION")]
    [SerializeField] float startAngle;
    [SerializeField] float speedFirst;
    [Header("SECOND ROTATION")]
    [SerializeField] float finalAngle;
    [SerializeField] float speedSecond;
    [Header("LAST ROTATION")]
    [SerializeField] float speedFinal;

    [SerializeField] Transform toRotate;
    bool playerIsInside = false;
    bool isMoving = false;

    // Update is called once per frame
    void Update()
    {
        if (InputManager.ActiveDevice.Action3.WasPressed && playerIsInside && !isMoving)
        {
            isMoving = true;
            FirstRoation();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInside = false;
        }
    }

    void FirstRoation()
    {
        toRotate.DORotate(new Vector3(startAngle,0,0) ,speedFirst);
        Invoke("SecondRotation", speedFirst);
    }

    void SecondRotation()
    {
        toRotate.DORotate(new Vector3(finalAngle, 0, 0), speedSecond);
        Invoke("LastRotation", speedSecond);
    }

    void LastRotation()
    {
        toRotate.DORotate(new Vector3(0, 0, 0), speedFinal);
        Invoke("RestartMovement", speedFinal);
    }

    void RestartMovement()
    {
        isMoving = false;
    }
}
