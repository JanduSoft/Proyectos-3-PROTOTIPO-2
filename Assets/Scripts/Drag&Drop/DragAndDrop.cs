using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{

    [HideInInspector] public GameObject player = null;
    GameObject grabPlace = null;
    public float minDistanceToGrabObject = 1.5f;
    [HideInInspector] public bool objectIsGrabbed = false;
    bool isFacingBox = false;
    public bool cancelledDrop = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player!=null)
        {
            isFacingBox = false;

            //checks if the distance from the player to the rock is close enough
            var distancePlayerObject = Vector3.Distance(player.transform.position, transform.position);
            //checks if the player is staring in the direction of the rock
            float dot = Vector3.Dot(player.transform.forward, (transform.position - player.transform.position).normalized);
            if (dot > 0.7f) { isFacingBox = true; }

            if (distancePlayerObject<minDistanceToGrabObject && Input.GetButtonDown("Interact") && isFacingBox && !cancelledDrop)
            {
                if (!objectIsGrabbed)
                {
                    GrabObject();
                }
                else
                {
                    DropObject();
                }
            }
        }
    }

    public void CancelledDrop(bool tof)
    {
        cancelledDrop = tof;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            grabPlace = player.transform.GetChild(1).gameObject;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            grabPlace = player.transform.GetChild(1).gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
            grabPlace = null;
        }
    }
    public void DropObject()
    {
        transform.SetParent(null);
        objectIsGrabbed = false;
    }

    public void GrabObject()
    {
        transform.SetParent(player.transform);
        transform.position = grabPlace.transform.position;
        objectIsGrabbed = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minDistanceToGrabObject);
    }
}
