﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class playerDeath : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 lastGroundedPosition;
    [SerializeField] PlayerMovement playerMovementScript;
    [SerializeField] Whip whipAttackScript;
    [SerializeField] GameObject deathPanel;
    bool isDead = false;

    [HideInInspector] public GameObject objectGrabbed;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovementScript.player.isGrounded && !whipAttackScript.attackMode && !isDead)
            lastGroundedPosition = playerMovementScript.player.transform.position;
    }

    public void killPlayer(float _secondsToRestart = 0)
    {
        if (!isDead)
            StartCoroutine(killPlayerInSeconds(_secondsToRestart));
    }

    IEnumerator killPlayerInSeconds(float _s)
    {
        isDead = true;
        yield return new WaitForSeconds(0.05f);
        playerMovementScript.StopMovement(true);
        for (float f = 0f; f <= 1.5f; f += 0.05f)
        {
            Color c = deathPanel.GetComponent<Image>().color;
            c.a = f;
            deathPanel.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(.01f);
        }
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);   //reloads the same scene
        //teleport player to previous grounded place
        Debug.Log(objectGrabbed);
        if (objectGrabbed!=null)
        {
            DragAndDrop DADScript = objectGrabbed.GetComponent<DragAndDrop>();
            if (DADScript!=null)
            {
                //do for objects with draganddrop
                objectGrabbed.GetComponent<DragAndDrop>().ResetObject();
                objectGrabbed.GetComponent<DragAndDrop>().DropObject();

            }
            else
            {
                //do for objects with PickUp
                objectGrabbed.GetComponent<PickUp>().ResetPosition();
                objectGrabbed.GetComponent<PickUpandDrop>().DropObject();

            }
        }
        playerMovementScript.player.transform.position = lastGroundedPosition;
        playerMovementScript.grounded = true;
        playerMovementScript.animatorController.SetBool("Jumping", false);
        whipAttackScript.ResetAllEnemiesAround();
        isDead = false;

        for (float f = 1.5f; f >= 0.0f; f -= 0.05f)
        {
            Color c = deathPanel.GetComponent<Image>().color;
            c.a = f;
            deathPanel.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(.01f);
        }

        playerMovementScript.StopMovement(false);
    }
}
