using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using InControl;


public class SkipCurrentCinematic : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PlayableDirector>().state == PlayState.Playing)
        {
            GameObject.Find("Character").GetComponent<PlayerMovement>().canMove = false;
        }
        if (InputManager.ActiveDevice.Action1.WasPressed && GetComponent<PlayableDirector>().state==PlayState.Playing)
        {
            GetComponent<PlayableDirector>().enabled = false;
            GameObject.Find("Character").GetComponent<PlayerMovement>().canMove = true;
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        GameObject.Find("Character").GetComponent<PlayerMovement>().canMove = true;
    }

}
