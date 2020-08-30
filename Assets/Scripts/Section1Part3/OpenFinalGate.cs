using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Playables;

public class OpenFinalGate : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PedestalsPuzzle LeftPedestalPuzzle;
    [SerializeField] RightPuzzle RightPedestalPuzzle;
    [SerializeField] GameObject rightDoor;
    [SerializeField] AudioSource openDoorSfx;
    bool doorClosed = true;
    [SerializeField] GameObject[] toDesable;
    [SerializeField] GameObject playableObject;

    [SerializeField] bool DEBUG_PUZZLECOMPLETE = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (doorClosed && LeftPedestalPuzzle.isPuzzleDone && RightPedestalPuzzle.hasCompletedPuzzle || DEBUG_PUZZLECOMPLETE)
        {
            doorClosed = false;
            //play rumble sound and open the door a bit
            rightDoor.transform.DOLocalRotate(new Vector3(0,-200,0), 3);
            openDoorSfx.Play();

        }
    }

    void detectGameEnding()
    {
        if (PlayerPrefs.GetInt("NumberOfDeaths")==0)
        {
            Logros.CallAchievement(17); //completed the game without deaths
        }


        Logros.CallAchievement(6);  //ended the game
    }

    void openGate()
    {

        detectGameEnding();

        //play animation
        playableObject.GetComponent<PlayableDirector>().Play();

        //disable parts
        foreach (var item in toDesable)
        {
            item.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!doorClosed && other.CompareTag("Player"))
        {
            openGate();
        }
    }
}
