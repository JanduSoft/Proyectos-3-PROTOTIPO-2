using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class playerDeath : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PlayerMovement playerMovementScript;
    [SerializeField] Whip whipAttackScript;
    [SerializeField] GameObject deathPanel;
    public bool isDead = false;

    public Vector3 lastSpawnPointTouched;
    public Vector3 lastCameraRotation;
    public Vector3 currentCameraRotation;
    public float lastFov;

    [HideInInspector] public GameObject objectGrabbed;
    void Start()
    {
        lastSpawnPointTouched = transform.position;
        lastCameraRotation = currentCameraRotation;
        lastFov = Camera.main.fieldOfView;
        //will do the fade out of the black screen when this script is started
        SpawnPlayer();
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
        Logros.numberOfDeaths++;
        PlayerPrefs.SetInt("NumberOfDeaths", Logros.numberOfDeaths);

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
        playerMovementScript.player.transform.position = lastSpawnPointTouched;
        Camera.main.transform.parent.transform.DOKill();
        Camera.main.transform.parent.transform.DORotate(lastCameraRotation,0.05f);
        GameObject.Find("TargetMainScene").GetComponent<FollowingCharacter>().naturalPosition = lastCameraRotation;
        currentCameraRotation = lastCameraRotation;
        Camera.main.fieldOfView = lastFov;
        GameObject auxPlayer = GameObject.Find("Character_Explorer_Male_01");
        Vector3 auxRotation = new Vector3(0, auxPlayer.transform.localRotation.y, 0);
        auxPlayer.transform.localRotation = Quaternion.Euler(auxRotation);

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
