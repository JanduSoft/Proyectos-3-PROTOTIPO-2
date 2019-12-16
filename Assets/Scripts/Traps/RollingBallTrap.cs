using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBallTrap : MonoBehaviour
{
    [SerializeField] GameObject rollingBall;
    [SerializeField] GameObject player;
    [SerializeField] Transform preassurePlateEndTransform;
    [SerializeField] bool trapActivated = false;
    [SerializeField] float speed = 0;
    [SerializeField] float distance = 0;
    Vector3 startingPos;
    Vector3 movementVector;
    bool preassuringPlate = false;
    float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = rollingBall.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        #region SHOOTING ARROWS
        if (trapActivated)
        {
            movementVector = Vector3.right * speed;
        }
        if (Vector3.Distance(startingPos, rollingBall.transform.position) > distance) trapActivated = false;
        #endregion

        #region PLATE EFFECT ANIMATION
        float temp;
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
        }

        #endregion
    }

    private void FixedUpdate()
    {
        if (trapActivated)
        {
            rollingBall.transform.position += movementVector;
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
