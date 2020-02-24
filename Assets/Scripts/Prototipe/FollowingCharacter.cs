using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FollowingCharacter : MonoBehaviour
{
    ////////////////////////////
    /// /////////////////////////////------------------------------VARIABLES
    ////////////////////////////

    [SerializeField] Transform player;
    [SerializeField] float cameraSpeed;
    public bool staticTarget = false;
    public Vector3 target;

    ////////////////////////////
    /// /////////////////////////////------------------------------METHODS
    ////////////////////////////

    /// /////////////////---- UPDATE
    void Update()
    {
        if (!staticTarget)
        {
            transform.DOMove(player.position, cameraSpeed);
        }
        else
        {
            transform.DOMove(target, cameraSpeed);
        }
                
    }

    
}
