using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraTransition : MonoBehaviour
{
    #region VARIABLES
    [Header("CAMERA")]
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera onboardingCamera;
    [SerializeField] int newFOV;

    [Header("TARGET CAMERA")]
    [SerializeField] Transform targetCamera;    
    [SerializeField] Vector3 newRotationTarget;
    
    [SerializeField] float speedTransition;

    FollowingCharacter myTargetCamera;

    #endregion

    private void Start()
    {
        myTargetCamera = GameObject.Find("TargetMainScene").GetComponent<FollowingCharacter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //transformCamera.DOMove(newTransformCamera.position, 4f);
            targetCamera.DORotate(newRotationTarget, speedTransition);
            mainCamera.DOFieldOfView(newFOV, speedTransition);
            onboardingCamera.DOFieldOfView(newFOV, speedTransition);

            myTargetCamera.naturalPosition = newRotationTarget;
            //mainCamera.DOShakePosition(1f,2f,10);
        }
    }
}
