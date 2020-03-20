using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPuzzle : MonoBehaviour
{
    [SerializeField] AddObject pedestal;
    [SerializeField] Animator animator;
    [SerializeField] CamerShake shakeCamera;
    [SerializeField] AudioSource audioShake;
    bool isOpened = false;
    // Update is called once per frame
    void Update()
    {
        if (pedestal.isActivated && pedestal._objectTransform.name == "Joya")
        {
            if (!isOpened)
            {
                isOpened = true;
                animator.SetBool("Active", true);
                shakeCamera.StartShake(shakeCamera.durationShake);
                audioShake.Play();
            }
        }
    }
}
