using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PushToAnimation : MonoBehaviour
{
    [Header("FOR CAMERA SHAKE")]
    [SerializeField] Camera myCamera;
    [SerializeField] float durationShake;
    [SerializeField] float strength;
    [SerializeField] int vibrato;
    [SerializeField] float randomness;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ToPush"))
        {
            Animator anim = other.GetComponent<Animator>();

            anim.SetBool("Active", true);
            Invoke("StartShake", 2.5f);
        }
    }

    void StartShake()
    {
        myCamera.DOShakePosition(durationShake, strength, vibrato, randomness, true);
    }
}
