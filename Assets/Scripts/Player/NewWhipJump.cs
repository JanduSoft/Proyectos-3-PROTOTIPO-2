using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewWhipJump : MonoBehaviour
{
    #region VARIABLES
    [Header("MOVEMENT")]
    [SerializeField] Transform destination;
    [SerializeField] float speed;
    bool canWhip = false;
    [Header("EXTERN VARIABLES")]
    [SerializeField] Transform player;
    [SerializeField] Transform whipableObject;
    [SerializeField] GameObject spriteIndicateObject;

    #endregion

    #region UPDATE
    private void Update()
    {
        if ((Input.GetButtonDown("Whip")) && canWhip)
        {
            WhipJump();
        }
    }
    #endregion

    #region WHIP JUMP
    void WhipJump()
    {
        if (canWhip)
        {
            spriteIndicateObject.transform.position = whipableObject.position;
        }
        player.DOMove(destination.position, speed);
    }
    #endregion

    #region WHIP JUMP ACTIVATE
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spriteIndicateObject.transform.position = whipableObject.position;
            spriteIndicateObject.SetActive(true);
            canWhip = true;
        }
    }
    #endregion

    #region WHIP JUMP DEACTIVATE
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spriteIndicateObject.SetActive(false);
            canWhip = false;
        }
    }
    #endregion
}
