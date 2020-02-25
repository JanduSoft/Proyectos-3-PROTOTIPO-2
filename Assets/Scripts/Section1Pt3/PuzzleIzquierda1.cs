using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PuzzleIzquierda1 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject leverRoja;
    [SerializeField] GameObject leverAzul;
    [SerializeField] GameObject leverLila;
    [SerializeField] GameObject tileRojo;
    [SerializeField] GameObject tileAzul;
    [SerializeField] GameObject tileLila;
    [SerializeField] GameObject movingGrid;
    [SerializeField] GameObject swordPedestal;
    [SerializeField] GameObject sword;
    [SerializeField] Vector3 EndMovingGridPosition;
    [SerializeField] Vector3 EndPedestalPosition;

    [Header("AUDIO")]
    [SerializeField] AudioClip winPuzzleSound;
    [SerializeField] AudioClip dragMetalSound;
    [SerializeField] AudioClip rockRumbleSound;


    enum Colors { RED, BLUE, PURPLE};
    Colors[] correctColorOrder = new Colors []{Colors.PURPLE,Colors.RED, Colors.BLUE};
    Colors[] usedColorOrder = new Colors[3];

    int currentLever = 0;

    bool hasDoneRed = false;
    bool hasDoneBlue = false;
    bool hasDonePurple = false;
    bool swordPuzzleDone = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if  (leverRoja.GetComponent<ActivatePuzzleLever>().Get_IsActive() && !hasDoneRed)
        {
            tileRojo.transform.GetChild(0).GetComponent<Light>().enabled = true;
            hasDoneRed = true;
            usedColorOrder[currentLever] = Colors.RED;
            currentLever++;
        }
        else if (leverAzul.GetComponent<ActivatePuzzleLever>().Get_IsActive() && !hasDoneBlue)
        {
            tileAzul.transform.GetChild(0).GetComponent<Light>().enabled = true;
            hasDoneBlue = true;
            usedColorOrder[currentLever] = Colors.BLUE;
            currentLever++;
        }
        else if (leverLila.GetComponent<ActivatePuzzleLever>().Get_IsActive() && !hasDonePurple)
        {
            tileLila.transform.GetChild(0).GetComponent<Light>().enabled = true;
            hasDonePurple = true;
            usedColorOrder[currentLever] = Colors.PURPLE;
            currentLever++;
        }

        if (currentLever==3 && !swordPuzzleDone)
        {
            Debug.Log("Correct: " + (int)correctColorOrder[0] + " " + (int)correctColorOrder[1] + " " + (int)correctColorOrder[2]);
            Debug.Log("Used: " + (int)usedColorOrder[0] + " " + (int)usedColorOrder[1] + " " + (int)usedColorOrder[2]);

            if ((int)correctColorOrder[0]==(int)usedColorOrder[0] &&
                (int)correctColorOrder[1]==(int)usedColorOrder[1] &&
                (int)correctColorOrder[2]==(int)usedColorOrder[2])
            {
                //puzzle correct
                //TODO: OPEN MOVING GRID
                swordPuzzleDone = true;
                GetComponent<AudioSource>().clip = winPuzzleSound;
                GetComponent<AudioSource>().Play();
                StartCoroutine(OpenGridAndMoveSword());
            }
            else
            {
                //incorrect puzzle
                //TODO: CAMERA SHAKE
                Camera.main.transform.DOShakePosition(0.5f,1,15,100,false,true);

                currentLever = 0;

                tileRojo.transform.GetChild(0).GetComponent<Light>().enabled = false;
                tileAzul.transform.GetChild(0).GetComponent<Light>().enabled = false;
                tileLila.transform.GetChild(0).GetComponent<Light>().enabled = false;

                leverRoja.GetComponent<ActivatePuzzleLever>().ResetLever();
                leverAzul.GetComponent<ActivatePuzzleLever>().ResetLever();
                leverLila.GetComponent<ActivatePuzzleLever>().ResetLever();

                hasDoneRed = false;
                hasDoneBlue = false;
                hasDonePurple = false;
            }
        }
    }

    IEnumerator OpenGridAndMoveSword()
    {
        yield return new WaitForSeconds(1.2f);
        GetComponent<AudioSource>().clip = dragMetalSound;
        GetComponent<AudioSource>().Play();
        Camera.main.transform.DOShakePosition(3.0f, 0.5f, 10, 90, false, true);
        movingGrid.transform.DOLocalMove(EndMovingGridPosition,3f,false);

        yield return new WaitForSeconds(3f);

        GetComponent<AudioSource>().clip = rockRumbleSound;
        GetComponent<AudioSource>().Play();
        Camera.main.transform.DOShakePosition(3f, 1f, 10, 100, false, true);
        swordPedestal.transform.DOLocalMove(EndPedestalPosition, 3f, false);


        yield return new WaitForSeconds(3f);
        sword.GetComponent<PickUpandDrop>().enabled = true;
    }
}
