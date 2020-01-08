using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BridgeMovement : MonoBehaviour
{
    [SerializeField] Vector3 newPosition;
    [SerializeField] float speed;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateObject()
    {
        transform.DOMove(newPosition, speed);
    }
}
