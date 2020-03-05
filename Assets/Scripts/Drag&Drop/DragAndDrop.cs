using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    Animator playerAnimator;
    [HideInInspector] public GameObject player = null;
    GameObject grabPlace = null;
    GameObject distanceChecker;
    public float minDistanceToGrabObject = 1.5f;
    [HideInInspector] public bool objectIsGrabbed = false;
    bool isFacingBox = false;
    public bool cancelledDrop = false;
    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        grabPlace = GameObject.Find("Hand_R_PickUp");
        startPosition = transform.position;
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

            if (Input.GetButtonDown("Interact")  && !cancelledDrop)
            {
                if (distancePlayerObject < minDistanceToGrabObject && !objectIsGrabbed && isFacingBox)
                {
                    playerAnimator.SetBool("PickUp", true);
                    playerAnimator.SetFloat("Distance", Mathf.Abs((transform.position.y - distanceChecker.transform.position.y)));
                    player.SendMessage("StopMovement", true);
                    if (Mathf.Abs((transform.position.y - distanceChecker.transform.position.y)) < 0.6)
                    {
                        StartCoroutine(PickUpCoroutine(0.35f));
                        StartCoroutine(AnimationsCoroutine(0.5f));
                    }
                    else
                    {
                        StartCoroutine(PickUpCoroutine(0.5f));
                        StartCoroutine(AnimationsCoroutine(0.65f));
                    }
                }
                else if (objectIsGrabbed)
                {
                    playerAnimator.SetBool("DropObject", true);
                    player.SendMessage("StopMovement", true);
                    StartCoroutine(DropObjectCoroutine(0.5f));
                    StartCoroutine(AnimationsCoroutine(0.65f));
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
            distanceChecker = player.transform.GetChild(1).gameObject;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            playerAnimator = player.GetComponentInChildren<Animator>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
        }
    }
    public void DropObject()
    {
        player.GetComponent<playerDeath>().objectGrabbed = null;
        transform.SetParent(null);
        objectIsGrabbed = false;
    }

    public void publicDropObject()
    {
        player.SendMessage("StopMovement", true);
        StartCoroutine(DropObjectCoroutine(0.5f));
        StartCoroutine(AnimationsCoroutine(0.5f));
    }
    public void publicPickUp()
    {
        player.SendMessage("StopMovement", true);
        StartCoroutine(PickUpCoroutine(0.35f));
        StartCoroutine(AnimationsCoroutine(0.5f));
    }
    public void ResetObject()
    {
        transform.position = startPosition;
    }

    public void GrabObject()
    {
        player.GetComponent<playerDeath>().objectGrabbed = gameObject;
        transform.SetParent(grabPlace.transform);
        transform.position = grabPlace.transform.position;
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = new Quaternion(0, 0, 0, 1);
        objectIsGrabbed = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minDistanceToGrabObject);
    }

    IEnumerator AnimationsCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        player.SendMessage("StopMovement", false);
        playerAnimator.SetBool("PickUp", false);
        playerAnimator.SetBool("DropObject", false);
        playerAnimator.SetBool("PlaceObject", false);

    }

    IEnumerator PickUpCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        
        GrabObject();
    }
    IEnumerator DropObjectCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        DropObject();
    }
}
