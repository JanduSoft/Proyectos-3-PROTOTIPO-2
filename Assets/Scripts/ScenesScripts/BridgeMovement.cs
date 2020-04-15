using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BridgeMovement : MonoBehaviour
{
    [SerializeField] Vector3 newPosition;
    [SerializeField] float speed;
    public bool isActivated = false;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateObject()
    {
        transform.DOMove(newPosition, speed);
        isActivated = true;
    }
}
