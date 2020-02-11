using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnimationScript : MonoBehaviour
{
    // Start is called before the first frame update

    Animation titleAnim;
    public GameObject mainCamera;
    public GameObject titleCamera;
    bool lerpingCamera = false;
    bool animPlayed = false;

    GameObject player;
    Vector3 newCamPos = new Vector3(13.8f,3.92f,-1);
    Vector3 newCamRot = new Vector3(15f, -90f, 0);
    void Start()
    {
        titleAnim = GetComponent<Animation>();
        if (PlayerPrefs.HasKey("hasDoneTitle"))
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lerpingCamera)
        {
            if (!animPlayed)
            {
                titleCamera.transform.localPosition = Vector3.Lerp(titleCamera.transform.localPosition, newCamPos, 0.04f);
                titleCamera.transform.localRotation = Quaternion.Lerp((titleCamera.transform.localRotation), Quaternion.Euler (newCamRot), 0.04f);
            }
            else
            {
                titleCamera.transform.position = Vector3.Lerp(titleCamera.transform.position, newCamPos, 0.02f);
                titleCamera.transform.rotation = Quaternion.Lerp((titleCamera.transform.rotation), Quaternion.Euler(newCamRot), 0.04f);
            }

            if (!animPlayed && Vector3.Distance(titleCamera.transform.localPosition, newCamPos)<0.05f)
            {
                //if camera has done lerp to new position
                lerpingCamera = false;
                titleAnim.Play();
                StartCoroutine(ResumeGameToNormal());
            }
            else if (animPlayed && Vector3.Distance(titleCamera.transform.position, newCamPos) < 0.5f)
            {
                lerpingCamera = false;
                player.GetComponent<PlayerMovement>().StopMovement(false);
                mainCamera.SetActive(true);
                titleCamera.SetActive(false);
                GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            player.GetComponent<PlayerMovement>().StopMovement(true);
            titleCamera.transform.position = mainCamera.transform.position;
            titleCamera.transform.rotation = mainCamera.transform.rotation;
            mainCamera.SetActive(false);
            titleCamera.SetActive(true);
            lerpingCamera = true;
        }
    }

    IEnumerator ResumeGameToNormal()
    {
        yield return new WaitForSeconds(titleAnim.clip.length);
        newCamPos = mainCamera.transform.position;
        newCamRot = mainCamera.transform.rotation.eulerAngles;
        animPlayed = true;
        lerpingCamera = true;

    }

}
