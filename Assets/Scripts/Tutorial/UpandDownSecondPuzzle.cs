using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpandDownSecondPuzzle : MonoBehaviour
{
    #region VARABLES
    [Header ("Sound Variables")]
    [SerializeField] AudioSource leverSound;
    [Header ("Puzzle Variables")]
    [SerializeField] Transform target;
    [SerializeField] GameObject place;
    [SerializeField] List<Transform> positions;
    [SerializeField] int targePosition;
    [SerializeField] string indexLever;
    [SerializeField] int index = 0;
    int max_index = 2;
    bool canInteract = false;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract)
        {
            if(Input.GetButtonDown("Interact"))
            {
                leverSound.Play();
                canInteract = false;
                switch(index)
                {
                    case 0:
                        target.position = positions[1].position;
                        index++;
                        break;
                    case 1:
                        target.position = positions[2].position;
                        index++;
                        break;
                    case 2:
                        target.position = positions[0].position;
                        index = 0;
                        break;
                    default:
                        index = 0;
                        break;
                }
                if (index == targePosition) place.SendMessage("Solved", indexLever);
                else place.SendMessage("Broken", indexLever);
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !leverSound.isPlaying)
        {
            canInteract = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && !leverSound.isPlaying)
        {
            canInteract = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            canInteract = false;
        }
    }
}
