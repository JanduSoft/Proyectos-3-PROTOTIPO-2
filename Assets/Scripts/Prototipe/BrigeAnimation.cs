using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrigeAnimation : MonoBehaviour
{
    [SerializeField] Animator bridgeAnimtor;
    private bool isInside= false;
    [SerializeField] GameObject textButton;
    [SerializeField] AudioSource platformAudioSource;
    [SerializeField] AudioSource switchAudioSource;

    #region UPDATE
    private void Update()
    {
        if (isInside)
        {
            if (Input.GetKeyDown(KeyCode.F) || Input.GetButtonDown("Fire3"))
            {
                bridgeAnimtor.SetBool("Active", true);
                platformAudioSource.Play();
                switchAudioSource.Play();
            }
        }
    }
    #endregion

    #region TRIGGER DETECTION
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = true;
            textButton.SetActive(true);
            //bridgeAnimtor.SetBool("Active" , true);
        }
        if (other.CompareTag("Hook"))
        {
            bridgeAnimtor.SetBool("Active" , true);
            platformAudioSource.Play();
            switchAudioSource.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = false;
            textButton.SetActive(false);
        }
    }
    #endregion
}
