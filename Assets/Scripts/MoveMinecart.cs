using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMinecart : MonoBehaviour
{
    bool onRail;
    [SerializeField] AddObject ao;
    [SerializeField] LeverMinecart durection;
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject mc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ao.isActivated)
        {
            if(onRail)
            {
                Debug.Log("Moving!");
                //if(minecartAction)
                rb.AddForce(Vector3.forward * 10f);
                rb.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Place"))
        {
            onRail = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Place"))
        {
            onRail = false;
        }
    }
}
