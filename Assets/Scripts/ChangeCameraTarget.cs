using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraTarget : MonoBehaviour
{

    [SerializeField] FollowingCharacter targetCamera;
    [SerializeField] Transform newTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetCamera.staticTarget = true;
            targetCamera.target = newTarget.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetCamera.staticTarget = false;
        }
    }
}
