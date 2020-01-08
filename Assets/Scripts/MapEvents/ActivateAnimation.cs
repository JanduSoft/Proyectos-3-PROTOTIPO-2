using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAnimation : MonoBehaviour
{
    [SerializeField] Animator myAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Skull"))
        {
            myAnimator.SetBool("Active", true);
            Invoke("DeactivateAnimation", 2f);
        }
    }

    void DeactivateAnimation()
    {
        myAnimator.SetBool("Active", false);
    }


}
