﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingArrowsTrap : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform shootingTransform;
    [SerializeField] float widthOffset = 0;
    [SerializeField] float heightOffset = 0;
    [SerializeField] int numArrows = 0;
    [SerializeField] Transform preassurePlateEndTransform;
    Vector3 startingPos;
    bool preassuringPlate = false;
    int i = 0;
    [SerializeField] bool trapActivated = false;
    float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        #region SHOOTING ARROWS
        if (trapActivated)
        {
            for(; i< numArrows; i++)
            {
                //We randomise the position of the arrows 
                float x = shootingTransform.position.x + Random.Range(-widthOffset, widthOffset);
                float y = shootingTransform.position.y + Random.Range(0, heightOffset);
                float z = shootingTransform.position.z + Random.Range(-widthOffset, widthOffset) ;
                //We isntantiate the arrows
                Instantiate(arrowPrefab, new Vector3(x,y,z), Quaternion.LookRotation(shootingTransform.forward), shootingTransform);
                StartCoroutine(ResetArrows());
            }
            trapActivated = false;
        }
        #endregion

        #region PLATE EFFECT ANIMATION
        float temp ;
        if (preassuringPlate)
        {
            time += Time.deltaTime;
            //We calculate the new Y position to make the PreassurePlate go DOWN
            temp = Mathf.Lerp(transform.position.y, preassurePlateEndTransform.position.y, time);
            transform.position = new Vector3(transform.position.x, temp, transform.position.z);
        }
        else
        {
            if(transform.position != startingPos)
            {
                //We calculate the new Y position to make the PreassurePlate go UP
                temp = Mathf.Lerp(transform.position.y, startingPos.y, time);
                transform.position = new Vector3(transform.position.x, temp, transform.position.z);
            }
            time = 0;
        }
        if(time >= 1)
        {
            trapActivated = true;
        }

        #endregion
    }

    IEnumerator ResetArrows()
    {
        yield return new WaitForSeconds(1.5f);
        i = 0;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            // preassuringPlate = true;
            other.gameObject.GetComponent<PlayerMovement>().isOnPressurePlate = true;
            trapActivated = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            //  preassuringPlate = false;
            other.gameObject.GetComponent<PlayerMovement>().isOnPressurePlate = false;
            trapActivated = false;
            i = 0;
        }
       
    }
}
