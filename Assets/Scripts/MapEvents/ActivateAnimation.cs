using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAnimation : MonoBehaviour
{
    public enum typeAnimator
    {
        NONE,
        DOOR,
        PLATFORM
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
    }

    void DeactivateAnimation()
    {
        myAnimator.SetBool("Active", false);
    }


}
