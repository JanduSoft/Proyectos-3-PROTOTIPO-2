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
        BRIDGE
    }
    [SerializeField] Animator myAnimator;
    [SerializeField] typeAnimator type;
    [SerializeField] GameObject objectToBePlaced;

    [Header("FOR CAMERA SHAKE")]
    [SerializeField] Camera myCamera;
    [SerializeField] float durationShake;
    [SerializeField] float strength;
    [SerializeField] int vibrato;
    [SerializeField] float randomness;
    [SerializeField] Transform objectPos = null;
    bool isObjectPlaced = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Place") && type == typeAnimator.DOOR)
        {
            if (!other.transform.parent.GetComponent<PickUpandDrop>().GetObjectIsGrabbed() && objectPos != null)
            {
                other.transform.parent.gameObject.transform.position = objectPos.transform.position;
                isObjectPlaced = true;
            }
            if (isObjectPlaced && other.transform.parent.name == objectToBePlaced.name)
            {
                myAnimator.SetBool("Active", true);
                Invoke("DeactivateAnimation", 2f);
            }
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
            //myCamera.DOShakePosition(durationShake, strength, vibrato, randomness,true);
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            DeactivateAnimation();
            //myCamera.DOShakePosition(durationShake, strength, vibrato, randomness, true);
        }
        if (other.CompareTag("Place"))
            isObjectPlaced = false;


    }

    void DeactivateAnimation()
    {
        myAnimator.SetBool("Active", false);
    }


}
