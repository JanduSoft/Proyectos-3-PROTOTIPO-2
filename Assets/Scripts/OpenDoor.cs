using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    ////////////////////////////
    /// /////////////////////////////------------------------------VARIABLES
    ////////////////////////////
    enum ButtonType
    {
        NONE,
        TEMPLE,
        DOOR
    };

    [SerializeField] ButtonType button;

    [Header("Door")]
    [SerializeField] GameObject doorOne;
    [SerializeField] GameObject doorTwo;
    [SerializeField] GameObject doorThree;
    [SerializeField] GameObject doorFour;
    [SerializeField] GameObject door;
    [SerializeField] bool doorIsOpening;
    [SerializeField] float speedDoor;
    private float counterDoorOne;
    private float counterDoorTwo;
    private float counterDoorThree;
    private float counterDoorFour;

    [Header("Temple")]
    [SerializeField] GameObject temple;
    [SerializeField] GameObject platforms;
    [SerializeField] GameObject dustParticles;
    [SerializeField] Transform spawn1;
    [SerializeField] Transform spawn2;
    [SerializeField] Transform spawn3;
    [SerializeField] Transform spawn4;
    private Animator templeAnimaior;
    private Animator platformsAnimator;

    [SerializeField] GameObject pilar1;
    [SerializeField] GameObject pilar2;
    [SerializeField] GameObject pilar3;
    [SerializeField] GameObject pilar4;



    private bool isInside = false;
    [SerializeField] GameObject buttonText;
    private int currentPilar = 1;
    


    ////////////////////////////
    /// /////////////////////////////------------------------------METHODS
    ////////////////////////////

    /// /////////////////---- START
    private void Start()
    {
        templeAnimaior = temple.GetComponent<Animator>();
        platformsAnimator = platforms.GetComponent<Animator>();
    }

    /// /////////////////---- UPDATE
    private void Update()
    {
        if (isInside && Input.GetKeyDown(KeyCode.E))
        {
            switch (button)
            {
                case ButtonType.DOOR:
                    {
                        doorIsOpening = true;

                        isInside = false;
                        buttonText.SetActive(false);

                        break;
                    }
                case ButtonType.TEMPLE:
                    {
                        templeAnimaior.SetBool("Temple", true);
                        platformsAnimator.SetBool("IsActivated", true);
                        Destroy(Instantiate(dustParticles, spawn1.position, spawn1.rotation), 4.75f);
                        Destroy(Instantiate(dustParticles, spawn2.position, spawn2.rotation), 4.75f);
                        Destroy(Instantiate(dustParticles, spawn3.position, spawn3.rotation), 4.75f);
                        Destroy(Instantiate(dustParticles, spawn4.position, spawn4.rotation), 4.75f);


                        Invoke("ActivateGameObject", 1.0f);
                        Invoke("ActivateGameObject", 2.0f);
                        Invoke("ActivateGameObject", 3.0f);
                        Invoke("ActivateGameObject", 4.0f);
                        //pilar1.SetActive(true);
                        //pilar2.SetActive(true);
                        //pilar3.SetActive(true);
                        //pilar4.SetActive(true);

                        BoxCollider box = GetComponent<BoxCollider>();
                        Destroy(box);

                        isInside = false;
                        buttonText.SetActive(false);

                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            
        }

        if (doorIsOpening)
        {
            
            //////////////////////////DOOR ONE
            if (counterDoorOne < 4f)
            {
                counterDoorOne += speedDoor;
                doorOne.transform.position -= new Vector3(0, speedDoor, 0);
            }
            //////////////////////////DOOR TWO
            if (counterDoorOne > 0.4f && counterDoorTwo < 4f)
            {
                counterDoorTwo += speedDoor;
                doorTwo.transform.position -= new Vector3(0, speedDoor, 0);
            }
            //////////////////////////DOOR THREE
            if (counterDoorOne > 0.8f && counterDoorThree < 4f)
            {
                counterDoorThree += speedDoor;
                doorThree.transform.position -= new Vector3(0, speedDoor, 0);
            }
            //////////////////////////DOOR FOUR
            if (counterDoorOne > 1.2f && counterDoorFour < 4f)
            {
                counterDoorFour += speedDoor;
                doorFour.transform.position -= new Vector3(0, speedDoor, 0);
            }

            if (counterDoorFour > 4f)
            {
                door.SetActive(false);
            }
        }
    }

    void ActivateGameObject()
    {
        if (currentPilar == 1)
        {
            pilar1.SetActive(true);
            currentPilar++;
        }
        else if (currentPilar == 2)
        {
            pilar2.SetActive(true);
            currentPilar++;
        }
        else if (currentPilar == 3)
        {
            pilar3.SetActive(true);
            currentPilar++;
        }
        else if (currentPilar == 4)
        {
            pilar4.SetActive(true);
            currentPilar++;
        }

    }
    /// /////////////////---- ON TRIGGER ENTER
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = true;
            buttonText.SetActive(true);
        }
    }

    /// /////////////////---- ON TRIGGER EXIT
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = false;
            buttonText.SetActive(false);
        }
    }


}
