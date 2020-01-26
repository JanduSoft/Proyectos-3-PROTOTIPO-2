using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAnimation : MonoBehaviour
{
    public enum typeAnimator
    {
        NONE,
        DOOR,
        PLATFORM,
        BRIDGE
    }
    [SerializeField] Animator myAnimator;
    [SerializeField] typeAnimator type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Skull") && type == typeAnimator.DOOR )
        {
            myAnimator.SetBool("Active", true);
            Invoke("DeactivateAnimation", 2f);
        }
        else if (other.CompareTag("Player") && type == typeAnimator.PLATFORM)
        {
            myAnimator.SetBool("Active", true);
            Invoke("DeactivateAnimation", 2.5f);
        }
        else if (other.CompareTag("Block") && type == typeAnimator.BRIDGE)
        {
            myAnimator.SetBool("Active", true);
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            DeactivateAnimation();
        }
    }

    void DeactivateAnimation()
    {
        myAnimator.SetBool("Active", false);
    }


}
