using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ActivateAnimation : MonoBehaviour
{
    public enum typeAnimator
    {
        NONE,
        DOOR,
        PLATFORM,
        BRIDGE,
        ROPE,
        TORCH
    }
    [SerializeField] Animator myAnimator;
    [SerializeField] typeAnimator type;

    bool isTorchInside = false;
    bool canBurn = false;

    [Header("FOR CAMERA SHAKE")]
    [SerializeField] Camera myCamera;
    [SerializeField] float durationShake;
    [SerializeField] float strength;
    [SerializeField] int vibrato;
    [SerializeField] float randomness;

    private void Update()
    {
        if (isTorchInside && canBurn && Input.GetButtonDown("Interact"))
        {
            myAnimator.SetBool("Active", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Skull") && type == typeAnimator.DOOR)
        {
            myAnimator.SetBool("Active", true);
            Invoke("DeactivateAnimation", 2f);
        }
        else if (other.CompareTag("Player") && type == typeAnimator.PLATFORM)
        {
            myAnimator.SetBool("Active", true);
            Invoke("DeactivateAnimation", 2.5f);
        }
        else if (other.CompareTag("Block") && type == typeAnimator.BRIDGE)
        {
            myAnimator.SetBool("Active", true);
            Debug.Log("Camera Shake!!");
            myCamera.DOShakePosition(durationShake, strength, vibrato, randomness, true);
        }
        else if (other.CompareTag("Torch") && type == typeAnimator.ROPE)
        {
            isTorchInside = true;
            if (other.gameObject.GetComponent<TorchPuzzles>().getTochIgnited())
            {
                canBurn = true;
            }
        }
        else if (other.CompareTag("Torch") && type == typeAnimator.TORCH)
        {
            myAnimator.SetBool("Active", true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            DeactivateAnimation();
            myCamera.DOShakePosition(durationShake, strength, vibrato, randomness, true);
        }
        else if (other.CompareTag("Torch") && type == typeAnimator.ROPE)
        {
            isTorchInside = false;
        }
    }

    void DeactivateAnimation()
    {
        myAnimator.SetBool("Active", false);
    }


}
