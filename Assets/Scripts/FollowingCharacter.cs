using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCharacter : MonoBehaviour
{
    ////////////////////////////
    /// /////////////////////////////------------------------------VARIABLES
    ////////////////////////////

    [SerializeField] Transform player;
    [SerializeField] Rigidbody targetRB;
    [SerializeField] float diferenceHeight;
    [SerializeField] float cameraSpeed;

    ////////////////////////////
    /// /////////////////////////////------------------------------METHODS
    ////////////////////////////


    /// /////////////////---- START
    void Start()
    {
        
    }

    /// /////////////////---- UPDATE
    void Update()
    {
        Vector3 newPosition = new Vector3(transform.position.x, player.position.y + diferenceHeight, transform.position.z);
        targetRB.MovePosition(newPosition);
        //diferenceHeight = transform.position.y - player.position.y;
        //Debug.Log(diferenceHeight);
        //if (diferenceHeight < 0 && diferenceHeight < -maxHeight)
        //{
        //    transform.Translate(transform.position.x, transform.position.y - cameraSpeed, transform.position.z);
        //}
        //else if (diferenceHeight > 0 && diferenceHeight > maxHeight)
        //{
        //    transform.Translate(transform.position.x, (transform.position.y - cameraSpeed), transform.position.z);
        //}
    }
}
