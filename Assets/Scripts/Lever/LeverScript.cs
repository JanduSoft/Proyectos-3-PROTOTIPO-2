using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
{
    /// <How_it_works>
    /// 
    /// STILL NEED TO DO THE CANVAS
    /// 
    /// If the player enters the lever trigger, the canvas will activate,
    /// if the player exits the trigger or activates the lever, the canvas will disappear.
    /// To activate the lever, the player needs to be at a certain distance from the lever.
    /// 
    /// You can only activate the lever once.
    /// 
    /// </How_it_works>


    // Variables
    enum LeverState { ON, OFF };
    LeverState leverState = LeverState.OFF;
    public bool canTurnOnAndOff = true;

    public float distanceToLever = 3f;
    GameObject player = null;

    void Start()
    {
        
    }

    void Update()
    {
        if (player!=null)
        {
            var currentDistanceToLever = Vector3.Distance(transform.position, player.transform.position);

            if ((currentDistanceToLever<distanceToLever) && Input.GetButtonDown("Interact"))
            {
                if (canTurnOnAndOff)
                    leverState = leverState == LeverState.OFF ? LeverState.ON : LeverState.OFF;
                else
                    leverState = LeverState.ON;

                Debug.Log(leverState);

            }
        }
    }

    public void ChangeState()
    {
        if (canTurnOnAndOff)
            leverState = leverState == LeverState.OFF ? LeverState.ON : LeverState.OFF;
        else
            leverState = LeverState.ON;

        Debug.Log(leverState);
    }

    private void OnTriggerStay(Collider other)
    {
        //If the player enters the lever trigger, we'll show the canvas to press button E
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //If the player exits the trigger, we'll hide the canvas
        if (other.CompareTag("Player"))
        {
            player = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceToLever);
    }
}
