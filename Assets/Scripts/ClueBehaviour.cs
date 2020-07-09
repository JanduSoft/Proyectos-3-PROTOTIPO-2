using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClueBehaviour : MonoBehaviour
{
    int audio;
    [SerializeField] float timeFirstAnimation = 3;
    bool firstTime = true;
    bool canPlay = true;
    [SerializeField] PlayerMovement player;
    [SerializeField] Transform cluePosition;
    [SerializeField] Transform targetCamera;

    [Header("CAMERAS")]
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera tutoCamera;
    [SerializeField] float cameraReductionFOV = 20;
    float initialFovMain;
    float initialFovTuto;

    [Header("SOUNDS")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audios;
    [SerializeField] float timeToCanPlayAgain;

    private void Start()
    {
        audio = Random.Range(0, audios.Length - 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //DarPista              
            audio++;
            if (audio > audios.Length - 1)
            {
                audio = 0;
            }

            //Comprobar si tiene que hacer la animacion
            if (firstTime)
            {
                firstTime = false;
                //player.canMove = false;
                player.StopMovement(true);
                Invoke("AllowMovement", timeFirstAnimation);

                //Guardamos el FOV actual
                initialFovMain = mainCamera.fieldOfView;
                initialFovTuto = tutoCamera.fieldOfView;

                //Hacemos el zoom in
                mainCamera.DOFieldOfView(mainCamera.fieldOfView - cameraReductionFOV, timeFirstAnimation/2);
                tutoCamera.DOFieldOfView(tutoCamera.fieldOfView - cameraReductionFOV, timeFirstAnimation/2);

                //Hacemos el Look At
                mainCamera.transform.DOLookAt(cluePosition.position, timeFirstAnimation / 2);
                tutoCamera.transform.DOLookAt(cluePosition.position, timeFirstAnimation / 2);

                //Preparamos el xoom out
                Invoke("RestartFOV", timeFirstAnimation / 2);

            }

            //Reproducir el Sonido
            if (canPlay)
            {
                audioSource.clip = audios[audio];
                audioSource.Play();
            }
            
            canPlay = false;
            Invoke("AllowPlayAudio", timeToCanPlayAgain);
            
        }
    }

    #region RESTART FOV
    void RestartFOV()
    {
        mainCamera.DOFieldOfView(initialFovMain, timeFirstAnimation / 2);
        tutoCamera.DOFieldOfView(initialFovTuto, timeFirstAnimation / 2);

        mainCamera.transform.DOLookAt(targetCamera.position, timeFirstAnimation / 2);
        tutoCamera.transform.DOLookAt(targetCamera.position, timeFirstAnimation / 2);
    }
    #endregion

    #region ALLOW MOVEMENT
    void AllowMovement()
    {
        //player.canMove = true;
        player.StopMovement(false);
    }
    #endregion

    #region ALLOW PLAY AUDIO
    void AllowPlayAudio()
    {
        canPlay = true;
    }
    #endregion

}
