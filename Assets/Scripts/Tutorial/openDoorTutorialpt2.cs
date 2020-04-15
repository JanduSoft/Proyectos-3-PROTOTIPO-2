using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using InControl;
using UnityEngine.Playables;
public class openDoorTutorialpt2 : MonoBehaviour
{
    bool canPlace = false;
    [SerializeField] GameObject dor;
    [SerializeField] int index;
    public GameObject placePosition;
    GameObject skull = null;

    public bool isActivated = false;
    [SerializeField] GameObject GemLight;
    [SerializeField] GameObject cinematicObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (skull != null)
        {
            if (canPlace && skull.GetComponent<DragAndDrop>().objectIsGrabbed && !isActivated && InputManager.ActiveDevice.Action3.WasPressed)
            {
                dor.SendMessage("Solved", index);
                isActivated = true;
                try
                {
                    GemLight.GetComponent<Light>().DOIntensity(4, 3);
                    StartCoroutine(DoCinematic());

                }
                catch
                {
                    Debug.LogWarning("You need to attach 'Purple Light' or 'Red Light' to openDoorTutorialpt2.cs");
                }
            }
        }


    }

    IEnumerator DoCinematic()
    {
        yield return new WaitForSeconds(0.5f);
        cinematicObject.SetActive(true);
        cinematicObject.GetComponent<PlayableDirector>().Play();
        yield return new WaitForSecondsRealtime((float)cinematicObject.GetComponent<PlayableDirector>().duration);
        cinematicObject.SetActive(false);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPlace = true;
        }
        else if (other.CompareTag("Place"))
        {
            skull = other.transform.gameObject;

        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Place"))
        {
            skull = other.transform.gameObject;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPlace = false;
        }
        else if (other.CompareTag("Place"))
        {
            other.gameObject.transform.GetComponent<DragAndDrop>().cancelledDrop = (false);
            skull = null;
        }

    }
}
