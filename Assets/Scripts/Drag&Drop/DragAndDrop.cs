using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{

    /// <How_it_works>
    /// 
    /// If the player is close enough and he interacts with the object,
    /// the object will get attached to the player.
    /// When the object is a child of the player, it will move as he moves.
    /// 
    /// </How_it_works>


    [SerializeField]GameObject player = null;
    [SerializeField] GameObject grabPlace = null;
    [SerializeField] public float minDistanceToGrabObject = 1.5f;
    [SerializeField] bool objectIsGrabbed = false;
    [SerializeField] bool isFacingBox = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player!=null)
        {
            isFacingBox = false;

            //checks if the distance from the player to the rock is close enough
            var distancePlayerObject = Vector3.Distance(player.transform.position, transform.position);
            //checks if the player is staring in the direction of the rock
            float dot = Vector3.Dot(player.transform.forward, (transform.position - player.transform.position).normalized);
            if (dot > 0.7f) { isFacingBox = true; }

            if (distancePlayerObject<minDistanceToGrabObject && Input.GetButtonDown("Interact") && isFacingBox)
            {
                if (!objectIsGrabbed)
                {
                    grabPlace = player.transform.GetChild(2).gameObject;
                    transform.SetParent(player.transform);
                    transform.position = grabPlace.transform.position;
                    objectIsGrabbed = true;
                }
                else
                {
                    transform.SetParent(null);
                    objectIsGrabbed = false;
                }
            }
        }
    }

    public void DropObject()
    {
        transform.SetParent(null);
        objectIsGrabbed = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minDistanceToGrabObject);
    }
}
