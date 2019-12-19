using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRoofTrap : MonoBehaviour
{
    [SerializeField] Rigidbody roofRB;
    [SerializeField] Transform preassurePlateEndTransform;
    [SerializeField] bool trapActivated = false;
    [SerializeField] Vector3 fallSpeed;
    Vector3 startingPos;
    bool preassuringPlate = false;
    float time = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        #region CALCULATE MOVEMENT VECTOR
        if (trapActivated)
        {
            roofRB.useGravity = (true);            
        }
        #endregion

        #region PLATE EFFECT ANIMATION
        /*float temp;
        if (preassuringPlate)
        {
            time += Time.deltaTime;
            //We calculate the new Y position to make the PreassurePlate go DOWN
            temp = Mathf.Lerp(transform.position.y, preassurePlateEndTransform.position.y, time);
            transform.position = new Vector3(transform.position.x, temp, transform.position.z);
        }
        else
        {
            if (transform.position != startingPos)
            {
                //We calculate the new Y position to make the PreassurePlate go UP
                temp = Mathf.Lerp(transform.position.y, startingPos.y, time);
                transform.position = new Vector3(transform.position.x, temp, transform.position.z);
            }
            time = 0;
        }
        if (time >= 1)
        {
            trapActivated = true;
        }*/

        #endregion
    }

    private void FixedUpdate()
    {
        if (trapActivated)
        {
            roofRB.AddForce(fallSpeed);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            // preassuringPlate = true;
            trapActivated = true;
        }
    }

}
