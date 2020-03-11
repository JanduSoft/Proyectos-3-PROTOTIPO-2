using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManagerS1P1 : MonoBehaviour
{
    [SerializeField] Animator doorAnimator;
    private int counter = 0;

    [Header("FOR CAMERA SHAKE")]
    [SerializeField] Camera myCamera;
    [SerializeField] float durationShake;
    [SerializeField] float strength;
    [SerializeField] int vibrato;
    [SerializeField] float randomness;
    [SerializeField] AudioSource shakeSound;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            counter++;
            Debug.Log("Hay colocadas " + counter);
            if (counter == 3)
            {
                shakeSound.Play();
                doorAnimator.SetBool("Active", true);
                myCamera.DOShakePosition(durationShake, strength, vibrato, randomness, true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            counter--;
            Debug.Log("Hay colocadas " + counter);
            doorAnimator.SetBool("Active", false);
        }
    }
}
