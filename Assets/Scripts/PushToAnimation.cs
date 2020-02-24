using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushToAnimation : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ToPush"))
        {
            Animator anim = other.GetComponent<Animator>();

            anim.SetBool("Active", true);
        }
    }
}
