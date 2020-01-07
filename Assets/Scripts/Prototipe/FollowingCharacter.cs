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

    ////////////////////////////
    /// /////////////////////////////------------------------------METHODS
    ////////////////////////////

    /// /////////////////---- UPDATE
    void Update()
    {
        Vector3 newPosition = new Vector3(player.position.x, player.position.y , player.position.z);        
        transform.DOMove(player.position, cameraSpeed);        
    }

    
}
