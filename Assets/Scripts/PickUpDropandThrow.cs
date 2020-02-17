using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PickUpDropandThrow : PickUpandDrop
{
    double timeKeyDown = 0f;
    bool keyDown = false;
    int force = 10;
    [Header("OBJECT COMPONENTS")] 
    [SerializeField] Rigidbody _thisRB;
    [SerializeField] SphereCollider _thisSC;
    [Header("EXTERNAL OBJECTS")]
    [SerializeField] GameObject dustParticles;
    [SerializeField] GameObject objectInside;
    bool useGravity = true;
    // Start is called before the first frame update
    void Start()
    {
        _thisRB.useGravity = false;
        useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckVariables();
        if (Input.GetButtonDown("Interact"))
        {
            keyDown = true;
            
        }
        else if ((Input.GetButtonUp("Interact")))
        {
            if (isFacingBox && !cancelledDrop)
            {
                if (!objectIsGrabbed)
                {
                    PickUpObject();
                }
                else if (timeKeyDown > 0f && timeKeyDown < 0.3f && objectIsGrabbed)
                {
                    DropObject();
                }
                else if (timeKeyDown > 0.3f && objectIsGrabbed)
                {
                    ThrowObject();
                    useGravity = true;
                }
            }
            timeKeyDown = 0;
            keyDown = false;
        }
        if (keyDown)
        {
            timeKeyDown += Time.deltaTime;
            if (timeKeyDown > 0.5f && objectIsGrabbed)
            {
                ThrowObject();
                useGravity = true;
                timeKeyDown = 0;
            }
        }

    }
    private void FixedUpdate()
    {
        if (useGravity) _thisRB.AddForce(Physics.gravity * (_thisRB.mass * _thisRB.mass));
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
    private void OnCollisionEnter(Collision collision)
    {
        if(tag == "Destroyable")
        {
            objectInside.SetActive(true);
            dustParticles.SetActive(true);
            dustParticles.transform.SetParent(null);
            objectInside.transform.SetParent(null);
            gameObject.SetActive(false);
        }
    }
    protected void ObjectDrop()
    {
        transform.SetParent(null);
        objectIsGrabbed = false;
    }

    protected void ThrowObject()
    {
        DropObject();
        transform.tag = "Destroyable";
        _thisSC.enabled = false;
        Vector3 temp = player.transform.forward * (5000 * ((float)timeKeyDown / 0.5f)) + player.transform.up * (3750 * ((float)timeKeyDown / 0.5f));
        _thisRB.AddForce(temp);

    }

    public void DropObject()
    {
        ObjectDrop();
    }
    public bool GetObjectIsGrabbed()
    {
        Debug.Log(transform.name + " " + objectIsGrabbed);
        return objectIsGrabbed;
    }
}
