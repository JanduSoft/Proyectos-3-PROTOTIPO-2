﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    ////////////////////////////
    /// /////////////////////////////------------------------------VARIABLES
    ////////////////////////////

    [SerializeField] float rotationSpeed;
    [SerializeField] Transform Target, player;
    [SerializeField] float padZoom;

    [SerializeField] float zoomSpeed = 2f;
    float mouseX,
            mouseY;
    private Camera cam;
    private float targetZoom;
    [SerializeField] float zoomFactor;
    [SerializeField] float zoomLerpSpeed;
    public Transform obstructionPlayer;
    public Transform obstructionTarget;

    [SerializeField] GameObject wall1;

    ////////////////////////////
    /// /////////////////////////////------------------------------METHODS
    ////////////////////////////

    /// /////////////////---- START
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        ///Camera
        cam = Camera.main;
        targetZoom = cam.orthographicSize;

        obstructionPlayer = Target;
        obstructionTarget = Target;
    }

    /// /////////////////---- UPDATE
    void Update()
    {
        //ViewObstructedPlayer();
        //cam.transform.DOLookAt(Target.position, 0.5f);

        //////////---------ZOOM
        /*float scrollData;
        scrollData = Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetButtonDown("RightTrigger"))
        {
            scrollData = padZoom;
        }
        else if (Input.GetButtonDown("LeftTrigger"))
        {
            scrollData = -padZoom;
        }*/

        //targetZoom -= scrollData * zoomFactor;
        // targetZoom = Mathf.Clamp(targetZoom, 5f, 30f);
        //cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
        //cam.DOOrthoSize(targetZoom, 2f);
    }

    /// /////////////////---- LATE UPDATE
 

    /// /////////////////---- CAMERA CONTROL
    void CameraControl()
    {
        //mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        //mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        //
        //mouseY = Mathf.Clamp(mouseY, -35, 60);
        //
        //transform.LookAt(Target);
        //Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(this.transform.position, Target.position);
    }
    /// /////////////////---- OBSTRUCTION PLAYER
    void ViewObstructedPlayer()
    { 
        RaycastHit hit;

        if (Physics.Raycast(this.transform.position, Target.position, out hit, 100f))
        {
            if (hit.collider.gameObject.tag != "Player")
            {

                //obstructionPlayer.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;


                obstructionPlayer = hit.transform;
                obstructionPlayer.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                Debug.Log("Algo Obstruye el jugador");
                return;

            }            
            else
            {
                //obstructionPlayer.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }
        }
    }

    /// /////////////////---- OBSTRUCTION TARGET
    void ViewObstructedTarget()
    {
        /* RaycastHit hit;

         if (Physics.Raycast(transform.position, Target.position - transform.position, out hit, 30))
         {


             if (hit.collider.gameObject.tag == "Wall1")
             {
                 if (obstructionTarget.gameObject.tag != "Wall1")
                 {
                     obstructionTarget.gameObject.SetActive(true);
                 }

                 obstructionTarget = hit.transform;
                 //obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                 obstructionTarget.gameObject.SetActive(false);

                 if (Vector3.Distance(obstructionTarget.position, transform.position) >= 3f && Vector3.Distance(transform.position, Target.position) >= 1.5f)
                 {
                     transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
                 }
             }
             else if (hit.collider.gameObject.tag == "Wall2")
             {
                 if (obstructionTarget.gameObject.tag != "Wall2")
                 {
                     obstructionTarget.gameObject.SetActive(true);
                 }

                 obstructionTarget = hit.transform;
                 obstructionTarget.gameObject.SetActive(false);

                 if (Vector3.Distance(obstructionTarget.position, transform.position) >= 3f && Vector3.Distance(transform.position, Target.position) >= 1.5f)
                 {
                     transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
                 }
             }
             else if (hit.collider.gameObject.tag == "Wall3")
             {
                 if (obstructionTarget.gameObject.tag != "Wall3")
                 {
                     obstructionTarget.gameObject.SetActive(true);
                 }

                 obstructionTarget = hit.transform;
                 obstructionTarget.gameObject.SetActive(false);

                 if (Vector3.Distance(obstructionTarget.position, transform.position) >= 3f && Vector3.Distance(transform.position, Target.position) >= 1.5f)
                 {
                     transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
                 }
             }
             else if (hit.collider.gameObject.tag == "Wall4")
             {
                 if (obstructionTarget.gameObject.tag != "Wall4")
                 {
                     obstructionTarget.gameObject.SetActive(true);
                 }

                 obstructionTarget = hit.transform;
                 obstructionTarget.gameObject.SetActive(false);

                 if (Vector3.Distance(obstructionTarget.position, transform.position) >= 3f && Vector3.Distance(transform.position, Target.position) >= 1.5f)
                 {
                     transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
                 }
             }
         }*/
    }
}
