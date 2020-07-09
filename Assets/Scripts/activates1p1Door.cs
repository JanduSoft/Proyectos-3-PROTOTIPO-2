using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class activates1p1Door : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AddObject pedestalScript;
    [SerializeField] GameObject part5;
    [SerializeField] AudioSource puzzleJingle;
    [SerializeField] AudioSource shakeSound;
    [SerializeField] Animator doorAnimator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pedestalScript.isActivated)
        {
            pedestalScript.enabled = false;
            puzzleJingle.Play();
            shakeSound.Play();
            Camera.main.DOShakeRotation(2,1.5f,35,27);
            doorAnimator.Play("WheelDoorAnimation");
            part5.SetActive(true);
            this.enabled = false;

        }
    }
}
