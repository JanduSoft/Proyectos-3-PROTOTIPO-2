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
    [SerializeField] GameObject activateObject;
    [SerializeField] GameObject lever;

    bool showCanvas = false;
    public float distanceToLever = 3f;
    GameObject player = null;
    bool leverPulled = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (player!=null)
        {
            var currentDistanceToLever = Vector3.Distance(transform.position, player.transform.position);
            if (showCanvas && (currentDistanceToLever<distanceToLever) && !leverPulled && Input.GetButtonDown("Interact"))
            {
                showCanvas = false;
                leverPulled = true;
                lever.transform.localRotation = new Quaternion (lever.transform.rotation.x, lever.transform.rotation.y, -45, lever.transform.rotation.w) ;
                activateObject.SendMessage("ActivateObject", false, SendMessageOptions.DontRequireReceiver);
                //We could have another object attached here, such as a GameObject MortalTrap, and that
                //object has a function activate. That way we could easily do MortalTrap.activate(); from here.
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //If the player enters the lever trigger, we'll show the canvas to press button E
        if (other.CompareTag("Player"))
        {
            showCanvas = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //If the player exits the trigger, we'll hide the canvas
        if (other.CompareTag("Player"))
        {
            showCanvas = false;
            player = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceToLever);
    }
}
