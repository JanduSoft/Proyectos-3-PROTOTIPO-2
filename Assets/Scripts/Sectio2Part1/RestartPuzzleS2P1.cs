using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class RestartPuzzleS2P1 : MonoBehaviour
{
    [Header("TRANSFORMS")]
    [SerializeField] Transform box1;
    [SerializeField] Transform box2;
    [SerializeField] Transform box3;
    [SerializeField] Transform box4;
    [SerializeField] float speed;

    Vector3 initialPosition1;
    Vector3 initialPosition2;
    Vector3 initialPosition3;
    Vector3 initialPosition4;
    bool isInside = false;

    [Header("FOR CAMERA SHAKE")]
    [SerializeField] Camera myCamera;
    [SerializeField] float durationShake;
    [SerializeField] float strength;
    [SerializeField] int vibrato;
    [SerializeField] float randomness;

    void Start()
    {
        initialPosition1 = box1.position;
        initialPosition2 = box2.position;
        initialPosition3 = box3.position;
        initialPosition4 = box4.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ActivateObject()
    {
        myCamera.DOShakePosition(durationShake, strength, vibrato, randomness, true);
        box1.DOMove(initialPosition1, speed);
        box2.DOMove(initialPosition2, speed);
        box3.DOMove(initialPosition3, speed);
        box4.DOMove(initialPosition4, speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = false;
        }
    }
}
