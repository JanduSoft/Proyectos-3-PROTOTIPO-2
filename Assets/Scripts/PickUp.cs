using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    protected GameObject player = null;
    protected GameObject grabPlace = null;
    protected float minDistanceToGrabObject = 1.5f;
    protected bool objectIsGrabbed = false;
    protected bool isFacingBox = false;
    protected bool cancelledDrop = false;
    protected float distancePlayerObject;
    // Start is called before the first frame update
    protected virtual void CheckVariables()
    {
        if (player != null)
        {
            isFacingBox = false;
            //checks if the distance from the player to the rock is close enough
            distancePlayerObject = Vector3.Distance(player.transform.position, transform.position);
            //checks if the player is staring in the direction of the rock
            Vector2 playerForward2d = new Vector2(player.transform.forward.x, player.transform.forward.z);
            Vector2 dirToObject2d = new Vector2(transform.position.x - player.transform.position.x, transform.position.z - player.transform.position.z);
            //we do the dot product of X and Z, to ignore the Y in case the object is placed above or below
            float dot = Vector3.Dot(playerForward2d, dirToObject2d);
            if (dot > 0.5f) { isFacingBox = true; }
        }
    }
    protected virtual void PickUpObject()
    {
        if (distancePlayerObject < minDistanceToGrabObject)
        {
            if (!objectIsGrabbed)
            {
                transform.SetParent(player.transform);
                transform.position = grabPlace.transform.position;
                objectIsGrabbed = true;
            }
        }
    }
    protected void ForcePickUpObject()
    {        
        if (!objectIsGrabbed)
        {
            transform.SetParent(player.transform);
            transform.position = grabPlace.transform.position;
            objectIsGrabbed = true;
        }       
    }
    public void GrabObject()
    {
        PickUpObject();
    }
    public void ForceGrabObject()
    {
        ForcePickUpObject();
    }
    public void SetCancelledDrop(bool status)
    {
        cancelledDrop = status;
    }
    public bool GetObjectIsGrabbed()
    {
        return objectIsGrabbed;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minDistanceToGrabObject);
    }
}
