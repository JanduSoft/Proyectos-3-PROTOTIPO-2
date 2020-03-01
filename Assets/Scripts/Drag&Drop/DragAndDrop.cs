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
        if (player != null)
        {
            isFacingBox = false;

            //checks if the distance from the player to the rock is close enough
            var distancePlayerObject = Vector3.Distance(player.transform.position, transform.position);
            //checks if the player is staring in the direction of the rock
            Vector2 playerForward2d = new Vector2(player.transform.forward.x, player.transform.forward.z);
            Vector2 dirToObject2d = new Vector2(transform.position.x - player.transform.position.x, transform.position.z - player.transform.position.z);

            //we do the dot product of X and Z, to ignore the Y in case the object is placed above or below
            float dot = Vector3.Dot(playerForward2d, dirToObject2d);

            if (dot > 0.5f) { isFacingBox = true; }

            if (distancePlayerObject < minDistanceToGrabObject && Input.GetButtonDown("Interact") && isFacingBox && !cancelledDrop)
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
        transform.SetParent(grabPlace.transform);
        transform.position = grabPlace.transform.position;
        objectIsGrabbed = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minDistanceToGrabObject);
    }
}
