using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TorchPuzzles : MonoBehaviour
{
    GameObject player = null;
    GameObject grabPlace = null;
    public float minDistanceToGrabObject = 1.5f;
    public bool objectIsGrabbed = false;
    bool isFacingBox = false;
    [SerializeField] GameObject firePlace;
    [SerializeField] GameObject ropeToBeIgnited;
    [SerializeField] GameObject fireParticles;
    [SerializeField] GameObject chains;
    [SerializeField] PlayableDirector animation;
    [SerializeField] bool nearFire = false;
    [SerializeField] bool nearRope = false;
    public bool torchIgnited = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            isFacingBox = false;

            //checks if the distance from the player to the rock is close enough
            var distancePlayerObject = Vector3.Distance(player.transform.position, transform.position);
            //checks if the player is staring in the direction of the rock
            float dot = Vector3.Dot(player.transform.forward, (transform.position - player.transform.position).normalized);
            if (dot > 0.7f) { isFacingBox = true; }

            if (distancePlayerObject < minDistanceToGrabObject && Input.GetButtonDown("Interact") && isFacingBox && (!nearFire && !nearRope))
            {
                if (!objectIsGrabbed)
                {
                    transform.SetParent(null);
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
            else if(nearFire && Input.GetButtonDown("Interact"))
            {
                Debug.Log("Torch Ignited");
                fireParticles.SetActive(true);
                torchIgnited = true;
            }
            else if(torchIgnited && nearRope && Input.GetButtonDown("Interact"))
            {
                Debug.Log("Rope Burnt");
                chains.SetActive(false);
                animation.enabled = true;
                nearRope = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            grabPlace = player.transform.GetChild(1).gameObject;
        }
        else if (other.name == firePlace.name)
        {
            Debug.Log("Near Fire");
            nearFire = true;
        }
        else if (other.name == ropeToBeIgnited.name)
        {
            Debug.Log("Near Rope");
            nearRope = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            grabPlace = player.transform.GetChild(1).gameObject;
        }
        else if (other.name == firePlace.name)
        {
            Debug.Log("Near Fire");
            nearFire = true;
        }
        else if (other.name == ropeToBeIgnited.name)
        {
            Debug.Log("Near Rope");
            nearRope = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
            grabPlace = null;
        }
        else if (other.name == firePlace.name)
        {
            Debug.Log("Near Fire");
            nearFire = false;
        }
        else if (other.name == ropeToBeIgnited.name)
        {
            Debug.Log("Near Rope");
            nearRope = false;
        }
    }
    public void DropObject()
    {
        transform.SetParent(null);
        objectIsGrabbed = false;
    }

    public bool getTochIgnited()
    {
        return torchIgnited;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minDistanceToGrabObject);
    }
}
