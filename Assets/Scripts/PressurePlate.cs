﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PressurePlate : MonoBehaviour
{
    [SerializeField] GameObject preassurePlate;
    [SerializeField] float toMove;
    [SerializeField] float speed;
    bool isDown = false;
    Vector3 initialPosition;

    [Header("FOR SHAKE")]
    [SerializeField] float strength;
    [SerializeField] int vibrato;
    [SerializeField] float randomness;
    [SerializeField] AudioSource shakeSound;
    [SerializeField] GameObject leftEye;
    [SerializeField] GameObject rightEye;

    private void Start()
    {
        initialPosition = preassurePlate.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            Debug.Log("Detecto el Bloque");
            shakeSound.Play();
            if (leftEye!=null && rightEye!=null)
            {
                leftEye.SetActive(true);
                rightEye.SetActive(true);
            }
            preassurePlate.transform.DOShakePosition(speed,strength,vibrato,randomness);
            preassurePlate.transform.DOMoveY(initialPosition.y - toMove, speed);
            isDown = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            shakeSound.Play();

            if (leftEye != null && rightEye != null)
            {
                leftEye.SetActive(false);
                rightEye.SetActive(false);
            }

            preassurePlate.transform.DOShakePosition(speed, strength, vibrato, randomness);
            preassurePlate.transform.DOMoveY(initialPosition.y, speed);

        }
    }
}
