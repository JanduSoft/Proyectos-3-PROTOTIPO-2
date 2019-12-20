using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectHook : MonoBehaviour
{
    HingeJoint myJoint;
    // Start is called before the first frame update
    void Start()
    {
        myJoint = gameObject.GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hook"))
        {
            myJoint.connectedBody = other.GetComponent<Rigidbody>();
        }
    }
}
