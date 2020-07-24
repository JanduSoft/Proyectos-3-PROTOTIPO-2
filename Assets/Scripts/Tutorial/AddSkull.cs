using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using DG.Tweening;

public class AddSkull : MonoBehaviour
{
    public GameObject placePosition;
    Transform skullTransform = null;
    public bool isImportantCup = false;
    bool canPlace = false;
    [SerializeField] Animator playeranimator;

    [SerializeField] GameObject skull = null;

    public bool isActivated = false;
    [SerializeField] bool faceOppositeDirection = false;

    bool goneDown = false;
    [Header("Only if isImportantCup")]
    [SerializeField] GameObject[] effects = new GameObject[4];
    //1 and 2 are the fire eyes
    //3 is dirt
    //4 is white smoke
    [Space(10)]
    AudioSource[] shakeSound;

    private void Start()
    {
        try
        {
            shakeSound = GetComponents<AudioSource>();
        }
        catch
        {
            Debug.LogWarning("Couldn't find audiosources in gameobject " + gameObject.name);
        }
    }


    void LateUpdate()
    {
        if (skull != null)
        {
            if (InputManager.ActiveDevice.Action3.WasPressed && skull.GetComponent<PickUpDropandThrow>().GetObjectIsGrabbed())
            {
                playeranimator.SetBool("PlaceObject", true);
                skull.GetComponent<PickUpDropandThrow>().DropObject();
                playeranimator.SetBool("DropObject", false);
                StartCoroutine(AnimationsCoroutine(0.1f));
            }

            if (!isImportantCup && canPlace && !skull.GetComponent<PickUpDropandThrow>().GetObjectIsGrabbed() && isActivated && InputManager.ActiveDevice.Action3.WasPressed)
            {
                //skull.GetComponent<DragAndDrop>().CancelledDrop(false);
                playeranimator.SetBool("PickUp", true);
                skull.GetComponent<PickUpDropandThrow>().ForceGrabObject();
                isActivated = false;
                StartCoroutine(AnimationsCoroutine(0.1f));
            }
            if(!skull.GetComponent<PickUpDropandThrow>().GetObjectIsGrabbed())
            {
                skullTransform.position = placePosition.transform.position;
                skullTransform.rotation = transform.rotation;
                skullTransform.SetParent(placePosition.transform);
                if (faceOppositeDirection) skullTransform.Rotate(0, 180, 0);   //this is in case you want to make the skull face the oposite direction
                isActivated = true;
            }
        }

        if (isActivated && isImportantCup && !goneDown)
        {
            skull.GetComponent<PickUpDropandThrow>().enabled = false;
            goneDown = true;
            Camera.main.DOShakePosition(1, 3, 10, 90, true);
            transform.DOMoveY(transform.position.y - 1f, 5f)
                .OnComplete(() => { 
                    effects[2].GetComponent<ParticleSystem>().Stop(); 
                    effects[3].GetComponent<ParticleSystem>().Stop(); 
                });
            foreach (var item in shakeSound)
            {
                item.Play();
            }
            try
            {
                effects[0].gameObject.SetActive(true);
                effects[1].gameObject.SetActive(true);
                effects[2].gameObject.SetActive(true);
                effects[3].gameObject.SetActive(true);
            }
            catch
            {
                Debug.LogWarning("Tried to access eyeFires in AddSkull.cs but there's no attached Game Object");
            }
            this.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPlace = true;
        }
        else if (other.CompareTag("Skull") || other.CompareTag("Place"))
        {
            skull = other.transform.parent.gameObject;
            skullTransform = other.transform.parent.gameObject.transform;
            skull.GetComponent<PickUpDropandThrow>().SetCancelledDrop(true);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Skull") || other.CompareTag("Place"))
        {
            skull = other.transform.parent.gameObject;
            skullTransform = other.transform.parent.gameObject.transform;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPlace = false;
            skull.GetComponent<PickUpDropandThrow>().SetCancelledDrop(false);
        }
        else if (other.CompareTag("Skull"))
        {
            other.gameObject.transform.parent.GetComponent<PickUpDropandThrow>().SetCancelledDrop(false);
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
