using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using InControl;

public class FollowingCharacter : MonoBehaviour
{
    ////////////////////////////
    /// /////////////////////////////------------------------------VARIABLES
    ////////////////////////////

    bool isFollowing = false;
    [SerializeField] Transform player;
    [SerializeField] float cameraSpeed;

    [SerializeField] float maxDistance=2;
    float distanceToCharacter;

    [HideInInspector]
    public bool staticTarget = false;

    public Vector3 target;

    private Vector3 normalRotation;
    private float angleBound = 15;
    private float speedBound = 1.5f;
    [HideInInspector]
    public Vector3 naturalPosition;

    ////////////////////////////
    /// /////////////////////////////------------------------------METHODS
    ////////////////////////////

    /// /////////////////---- START
    private void Start()
    {
        naturalPosition = this.transform.rotation.eulerAngles;
    }

    /// /////////////////---- UPDATE
    void Update()
    {
        if (!staticTarget)
        {
            distanceToCharacter = Vector3.Distance(this.transform.position, player.position);

            if (distanceToCharacter >= maxDistance)
            {
                    transform.DOMove(player.position, cameraSpeed);
            }
        }
        else
        {
            transform.DOMove(target, cameraSpeed);
        }

        /////GIRAR LA CAMARA UN POCO CON EL STICK IZQUIERDO

        //MOVIMIENTO EN LA X
        //float rightStickAxis = InputManager.ActiveDevice.RightStickX;
        float rightStickAxis = GeneralInputScript.Input_GetAxis("CameraHorizontal");
        if (rightStickAxis > 0.9f) rightStickAxis = 0.9f;
        else if (rightStickAxis < -0.9f) rightStickAxis = -0.9f;


        if (rightStickAxis == 0.9f)
        {
            transform.DORotate(new Vector3(naturalPosition.x,
                                            naturalPosition.y - angleBound,
                                            naturalPosition.z),
                                            speedBound);
            return;
        }
        else if (rightStickAxis == -0.9f)
        {
            transform.DORotate(new Vector3(naturalPosition.x,
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, maxDistance);

    }

    

}
