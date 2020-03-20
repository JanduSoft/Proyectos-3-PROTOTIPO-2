using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class playerDeath : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 lastGroundedPosition;
    Vector3 lastDirection;
    [SerializeField] PlayerMovement playerMovementScript;
    [SerializeField] Whip whipAttackScript;
    [SerializeField] GameObject deathPanel;
    [SerializeField] float respawnOffset;
    bool isDead = false;

    [HideInInspector] public GameObject objectGrabbed;
    void Start()
    {
        //will do the fade out of the black screen when this script is started
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovementScript.player.isGrounded && !whipAttackScript.attackMode && !isDead)
        {
            lastGroundedPosition = playerMovementScript.player.transform.position;
            lastDirection = playerMovementScript.player.velocity;
        }
    }

    public void SpawnPlayer()
    {
        StartCoroutine(SpawnPlayerCoroutine());
    }

    IEnumerator SpawnPlayerCoroutine()
    {
        deathPanel.GetComponent<Image>().color = Color.black;
        playerMovementScript.StopMovement(true);

        for (float f = 1.5f; f >= 0.0f; f -= 0.05f)
        {
            Color c = deathPanel.GetComponent<Image>().color;
            c.a = f;
            deathPanel.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(.01f);
        }

        playerMovementScript.StopMovement(false);
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

        #region
        //This section is commented so when you die with an object, you still have that object
        #region
        //Debug.Log(objectGrabbed);
        //if (objectGrabbed!=null)
        //{
        //    DragAndDrop DADScript = objectGrabbed.GetComponent<DragAndDrop>();
        //    if (DADScript!=null)
        //    {
        //        //do for objects with draganddrop
        //        objectGrabbed.GetComponent<DragAndDrop>().ResetObject();
        //        objectGrabbed.GetComponent<DragAndDrop>().DropObject();

        //    }
        //    else
        //    {
        //        //do for objects with PickUp
        //        objectGrabbed.GetComponent<PickUp>().ResetPosition();
        //        objectGrabbed.GetComponent<PickUpandDrop>().DropObject();

        //    }
        //}
        #endregion

        #endregion

        Vector3 offsetDir = new Vector3(lastDirection.x, -1, lastDirection.z);
        playerMovementScript.player.transform.position = lastGroundedPosition - offsetDir * respawnOffset;
        playerMovementScript.inRespawn = true;
        playerMovementScript.fallVelocity = 0;
        playerMovementScript.animatorController.SetBool("Jumping", false);
        isDead = false;

        for (float f = 1.5f; f >= 0.0f; f -= 0.05f)
        {
            Color c = deathPanel.GetComponent<Image>().color;
            c.a = f;
            deathPanel.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(.01f);
        }

        playerMovementScript.StopMovement(false);
        playerMovementScript.inRespawn = false;
    }
}
