using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using InControl;
using UnityEngine.Playables;

public class ActivateDoor : MonoBehaviour
{
    [SerializeField] Transform objectoToMove;
    [SerializeField] Transform finalPosition;
    [SerializeField] float speed;
    [SerializeField] Animator animator;
    [Header("PARTS TO ACTIVATE")]
    [SerializeField] EnablePartsOfMap toDisable1;
    [SerializeField] EnablePartsOfMap toDisable2;
    [SerializeField] EnablePartsOfMap toEnable;

    [SerializeField] SectionMapVariables myPart;
    [SerializeField] Transform skullPlace;

    [Header("FOR CAMERA SHAKE")]
    [SerializeField] Camera myCamera;
    [SerializeField] float durationShake;
    [SerializeField] float strength;
    [SerializeField] int vibrato;
    [SerializeField] float randomness;
    [SerializeField] Transform objectPos = null;
    bool isOpened = false;
    [Header("FOR SOUND EFFECTS")]
    [SerializeField] AudioSource puzzleJingle;
    [SerializeField] AudioSource shakeSound;
    [Header("FOR CINEMATIC")]
    [SerializeField] GameObject cinematic;



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Place"))
        {
            if (InputManager.ActiveDevice.Action3.WasPressed)
            {
                animator.SetBool("DropObject", false);
                animator.SetBool("PlaceObject", true);
                StartCoroutine(AnimationsCoroutine(0.5f));
            }
            if (!other.transform.parent.GetComponent<PickUpandDrop>().GetObjectIsGrabbed())
            {
                objectoToMove.DOMoveY(finalPosition.position.y, speed);
                other.tag = "Untagged";
                other.transform.parent.gameObject.transform.position = skullPlace.position;
                other.transform.parent.gameObject.transform.rotation = skullPlace.rotation;
                other.transform.parent.gameObject.GetComponent<PickUpandDrop>().enabled = false;
                myPart.isActive = true;
                if (!isOpened)
                {
                    puzzleJingle.Play();
                    isOpened = true;
                    shakeSound.Play();
                    myCamera.DOShakePosition(durationShake, strength, vibrato, randomness, true);
                    StartCoroutine(doCinematic());

                    
                }
            }
        }
    }

    IEnumerator doCinematic()
    {
        yield return new WaitForSeconds(1);
        cinematic.SetActive(true);
        cinematic.GetComponent<PlayableDirector>().Play();
        yield return new WaitForSeconds((float)cinematic.GetComponent<PlayableDirector>().playableAsset.duration);
        cinematic.SetActive(false);
        yield return null;

    }
    IEnumerator AnimationsCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        animator.SetBool("PickUp", false);
        animator.SetBool("DropObject", false);
        animator.SetBool("PlaceObject", false);

    }
}
