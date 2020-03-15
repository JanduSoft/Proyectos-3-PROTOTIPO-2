﻿using System.Collections;
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

    private Vector3 normalRotation;
    private float angleBound = 15;
    private float speedBound = 1.5f;
    public Vector3 naturalPosition;

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

        /////GIRAR LA CAMARA UN POCO CON EL STICK IZQUIERDO
        
                ////MOVIMIENTO EN X
        if (Input.GetAxis("Horizontal2") == 1)
        {
            transform.DORotate(new Vector3(naturalPosition.x,
                                            naturalPosition.y - angleBound,
                                            naturalPosition.z),
                                            speedBound);
            return;
        }
        else if (Input.GetAxis("Horizontal2") == -1)
        {
            transform.DORotate(new Vector3( naturalPosition.x,
                                            naturalPosition.y + angleBound,
                                            naturalPosition.z),
                                            speedBound);
            return;

        }
        else
        {
            transform.DORotate(new Vector3(naturalPosition.x,
                                            naturalPosition.y,
                                            naturalPosition.z),
                                            speedBound);
        }

    }

    
}
