using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSkull : MonoBehaviour
{
    public GameObject placePosition;
    Transform skullTransform = null;
    public bool isImportantCup = false;
    bool canPlace = false;
    [SerializeField] Animator playeranimator;

    GameObject skull = null;

    public bool isActivated = false;
    [SerializeField] bool faceOppositeDirection = false;

    private void Start()
    {
    }


    void LateUpdate()
    {
        if (skull != null)
        {
            if (Input.GetButtonDown("Interact") && skull.GetComponent<DragAndDrop>().objectIsGrabbed)
            {
                playeranimator.SetBool("PlaceObject", true);
                skull.GetComponent<DragAndDrop>().publicDropObject();
                playeranimator.SetBool("DropObject", false);
                StartCoroutine(AnimationsCoroutine(0.5f));
            }

            if (!isImportantCup && canPlace && !skull.GetComponent<DragAndDrop>().objectIsGrabbed && isActivated && Input.GetButtonDown("Interact"))
            {
                //skull.GetComponent<DragAndDrop>().CancelledDrop(false);
                skull.GetComponent<DragAndDrop>().publicPickUp();
                isActivated = false;
            }
            if(!skull.GetComponent<DragAndDrop>().objectIsGrabbed)
            {
                skullTransform.position = placePosition.transform.position;
                skullTransform.rotation = transform.rotation;
                if (faceOppositeDirection) skullTransform.Rotate(0, 180, 0);   //this is in case you want to make the skull face the oposite direction
                isActivated = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPlace = true;
        }
        else if (other.CompareTag("Skull"))
        {
            skull = other.transform.parent.gameObject;
            skullTransform = other.transform.parent.gameObject.transform;
            skull.GetComponent<DragAndDrop>().CancelledDrop(true);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Skull"))
        {
            Debug.Log("detects skull");
            skull = other.transform.parent.gameObject;
            skullTransform = other.transform.parent.gameObject.transform;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPlace = false;
        }
        else if (other.CompareTag("Skull"))
        {
            other.gameObject.transform.parent.GetComponent<DragAndDrop>().CancelledDrop(false);
            skull = null;
            skullTransform = null;
        }
    }

    IEnumerator AnimationsCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        playeranimator.SetBool("PickUp", false);
        playeranimator.SetBool("DropObject", false);
        playeranimator.SetBool("PlaceObject", false);

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GetComponent<SphereCollider>().bounds.center, GetComponent<SphereCollider>().radius * transform.lossyScale.x);
    }
}
