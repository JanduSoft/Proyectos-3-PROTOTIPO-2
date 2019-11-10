using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrigeAnimation : MonoBehaviour
{
    [SerializeField] Animator bridgeAnimtor;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hook"))
        {
            bridgeAnimtor.SetBool("Active" , true);
        }
    }
}
