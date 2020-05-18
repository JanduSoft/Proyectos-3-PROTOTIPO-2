using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPuzzle : MonoBehaviour
{
    [SerializeField] AddObject pedestal;
    [SerializeField] Animator animator;
    [SerializeField] CamerShake shakeCamera;
    [SerializeField] AudioSource audioShake;
    [SerializeField] GameObject checkMark;
    bool isOpened = false;
    // Update is called once per frame
    void Update()
    {
        if (pedestal.isActivated && pedestal._objectTransform.name == "Joya")
        {
            if (!isOpened)
            {
                checkMark.SetActive(true);
                isOpened = true;
                animator.SetBool("Active", true);
                shakeCamera.StartShake(shakeCamera.durationShake);
                audioShake.Play();
            }
        }
    }
}
