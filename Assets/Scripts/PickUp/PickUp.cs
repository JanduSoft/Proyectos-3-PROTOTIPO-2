using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    protected GameObject player = null;
    protected GameObject grabPlace = null;
    protected GameObject distanceChecker = null;
    protected float minDistanceToGrabObject = 2.5f;
    protected bool objectIsGrabbed;
    protected bool distanceSuficient = false;
    [SerializeField] protected bool isFacingBox = false;
    protected bool cancelledDrop = false;
    protected float distancePlayerObject;
    protected float minDot = 0.5f;
    protected Vector3 startingPosition;
    protected Quaternion startingRotation;
    [SerializeField] protected PlayerMovement playerMovement;
    [SerializeField] protected bool respawn = true;

    public virtual void ResetPosition()
    {
        if (!objectIsGrabbed)
        {
            try
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                transform.position = startingPosition;
                transform.localRotation = startingRotation;
            }
            catch
            {
                Debug.Log("Can't respawn object");
            }
        }
    }

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
            if (dot > minDot) { isFacingBox = true; }
            distanceSuficient = (distancePlayerObject < minDistanceToGrabObject);
        }
    }
    protected virtual void PickUpObject()
    {
        GetComponent<Rigidbody>().isKinematic = false;

        transform.SetParent(grabPlace.transform);
        transform.localPosition = new Vector3(0,0,0);
        transform.localRotation = new Quaternion(0, 0, 0, 1);
        objectIsGrabbed = true;

    }
    virtual protected void ForcePickUpObject()
    {
        if (!objectIsGrabbed)
        {
            transform.SetParent(grabPlace.transform);
            transform.localPosition = Vector3.zero;
            objectIsGrabbed = true;
            player.GetComponent<playerDeath>().objectGrabbed = gameObject;
        }
    }
    public void GrabObject()
    {
        Debug.Log("GrabObject");
        playerMovement.ableToWhip = false;
        PickUpObject();
    }
    public void ForceGrabObject()
    {
        playerMovement.ableToWhip = false;
        ForcePickUpObject();
    }
    public void SetCancelledDrop(bool status)
    {
        cancelledDrop = status;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minDistanceToGrabObject);
    }
}