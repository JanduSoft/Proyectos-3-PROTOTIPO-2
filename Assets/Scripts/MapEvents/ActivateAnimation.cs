using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using InControl;

public class ActivateAnimation : MonoBehaviour
{
    #region VARIABLES
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
    [SerializeField] Animator playeranimator;

    [Header("FOR CAMERA SHAKE")]
    [SerializeField] Camera myCamera;
    [SerializeField] float durationShake;
    [SerializeField] float strength;
    [SerializeField] int vibrato;
    [SerializeField] float randomness;
    [SerializeField] Transform objectPos = null;
    bool isObjectPlaced = false;
    bool isOpened = false;
    [Header("FOR SOUND EFFECTS")]
    [SerializeField] AudioSource puzzleJingle;
    [SerializeField] AudioSource shakeSound;
    #endregion

    #region TRIGGER ENTER
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Place") && type == typeAnimator.DOOR)
        {
            if (other.transform.parent.GetComponent<PickUpandDrop>().GetObjectIsGrabbed() && objectPos != null && InputManager.ActiveDevice.Action3.WasPressed)
            {
                other.transform.parent.gameObject.transform.position = objectPos.transform.position;
                isObjectPlaced = true;
            }
            if (isObjectPlaced && other.transform.parent.name == objectToBePlaced.name)
            {
                puzzleJingle.Play();
                myAnimator.SetBool("Active", true);
                Invoke("DeactivateAnimation", 2f);
                Debug.Log("Camera Shake!!");
                myCamera.DOShakePosition(durationShake, strength, vibrato, randomness, true);
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
            myCamera.DOShakePosition(durationShake, strength, vibrato, randomness,true);
        }
        else if (other.CompareTag("Place") && type == typeAnimator.BRIDGE)
        {
            myAnimator.SetBool("Active", true);
            puzzleJingle.Play();
            shakeSound.Play();
            myCamera.DOShakePosition(durationShake, strength, vibrato, randomness, true);
            Destroy(this.gameObject);

        }


    }
    #endregion

    #region TRIGGER EXIT
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            DeactivateAnimation();
            myCamera.DOShakePosition(durationShake, strength, vibrato, randomness, true);
        }
        if (other.CompareTag("Place"))
            isObjectPlaced = false;


    }
    #endregion

    #region TRIGGER STAY
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Place") && type == typeAnimator.DOOR)
        {
            if (InputManager.ActiveDevice.Action3.WasPressed)
            {
                playeranimator.SetBool("DropObject", false);
                playeranimator.SetBool("PlaceObject", true);
                StartCoroutine(AnimationsCoroutine(0.5f));
            }
            if (other.transform.parent.GetComponent<PickUpandDrop>().GetObjectIsGrabbed() && objectPos != null && InputManager.ActiveDevice.Action3.WasPressed)
            {
                other.transform.parent.parent = null;
                other.tag = "Untagged";
                other.transform.parent.gameObject.transform.position = objectPos.position;
                other.transform.parent.gameObject.transform.rotation = objectPos.rotation;
                isObjectPlaced = true;
                if (!isOpened)
                {
                    isOpened = true;
                    puzzleJingle.Play();
                    shakeSound.Play();
                    myCamera.DOShakePosition(durationShake, strength, vibrato, randomness, true);
                }
            }
            if (isObjectPlaced && other.transform.parent.name == objectToBePlaced.name)
            {
                myAnimator.SetBool("Active", true);
                if (!isOpened)
                {
                    isOpened = true;
                    puzzleJingle.Play();
                    shakeSound.Play();
                    myCamera.DOShakePosition(durationShake, strength, vibrato, randomness, true);
                }
                Invoke("DeactivateAnimation", 2f);
            }
        }
    }
    #endregion

    #region DEACTIVATE ANIMATION
    void DeactivateAnimation()
    {
        myAnimator.SetBool("Active", false);
    }
    #endregion

    #region START SHAKE
    void StartShake()
    {
        puzzleJingle.Play();
        shakeSound.Play();
        myCamera.DOShakePosition(durationShake, strength, vibrato, randomness, true);
    }
    #endregion

    IEnumerator AnimationsCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        playeranimator.SetBool("PickUp", false);
        playeranimator.SetBool("DropObject", false);
        playeranimator.SetBool("PlaceObject", false);

    }
}
