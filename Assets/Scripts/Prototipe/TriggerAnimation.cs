using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] Animator animator;
    private int counter;
    private bool isInside = false;
    #endregion

    #region UPDATE
    private void Update()
    {
        if(isInside)
        {
            counter++;
            if (counter >= 120)
            {
                animator.SetBool("Active", true);
            }
        }
    }
    #endregion

    #region TRIGGER
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = true;
        }
        
    }
    #endregion
}
