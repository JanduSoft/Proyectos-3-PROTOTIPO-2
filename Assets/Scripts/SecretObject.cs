using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.UI;
using DG.Tweening;

public class SecretObject : MonoBehaviour
{
    #region VARIABLES
    public enum Object
    {
        NONE,
        OBJECT_1,
        OBJECT_2,
        OBJECT_3,
        OBJECT_4,
        OBJECT_5,
        OBJECT_6


    };

    [SerializeField] Object _object;
    [SerializeField] TutorialSprites tutoSprites;
    [SerializeField] SecretScreen secretScreen;
    [SerializeField] PlayerMovement playerMove;
    bool isPlayerInside = false;
    bool isShowingObject = false;
    [Header("FOR ANIMATION")]
    [SerializeField] GameObject secretObject;
    [SerializeField] Transform spawnPosition;
    [SerializeField] GameObject background;
    [SerializeField] TutorialSprites tuto;

    GameObject objectInstantiate;
    [Header("AUDIO")]
    [SerializeField] AudioSource sound;
    [SerializeField] float minTime = 3;
    [SerializeField] AudioSource close;
    bool startAnimation = false;
    float timeToFinishAnimation = 0;
    bool isOpen = false;

    [Header("Text")]
    [SerializeField] float sizeAnimationText;
    [SerializeField] GameObject text;
    [SerializeField] Text openSecretsText;

    #endregion

    #region UPDATE
    void Update()
    {
        if (startAnimation)
        {
            switch (GeneralInputScript.currentController.ControllerName)
            {
                case GeneralInputScript.ControllerNames.KEYBOARD_AND_MOUSE:
                    {
                        openSecretsText.text = "press I to open the list of secrets";
                        break;
                    }
                case GeneralInputScript.ControllerNames.PS3:
                    {
                        openSecretsText.text = "press SELECT to open the list of secrets";
                        break;
                    }
                case GeneralInputScript.ControllerNames.PS4:
                    {
                        openSecretsText.text = "press SHARE to open the list of secrets";
                        break;
                    }
                case GeneralInputScript.ControllerNames.XBOX360:
                    {
                        openSecretsText.text = "press BACK to open the list of secrets";
                        break;
                    }
                case GeneralInputScript.ControllerNames.XBOXONE:
                    {
                        openSecretsText.text = "press BACK to open the list of secrets";
                        break;
                    }
                default:
                    break;
            }
            timeToFinishAnimation += Time.deltaTime;

            if (timeToFinishAnimation >= minTime)
            {
                startAnimation = false;
            }
            else if (timeToFinishAnimation >= minTime -0.5f)
            {
                AnimationText();
            }
        }

        if ( isPlayerInside && GeneralInputScript.Input_GetKeyDown("Interact") && !isShowingObject)
        {
            Debug.Log("NOW HERE!");
            //Animation start
            startAnimation = true;
            isOpen = true;
            //Sound
            sound.Play();

            isShowingObject = true;
            //playerMove.canMove = false;
            playerMove.StopMovement(true);

            tutoSprites.DeactivateSprites();
            //ACTUALIZAMOS LOS PLAYER PREFS;
            DiscoverObject();

            //ACTUALIZAMOS EL MENU
            secretScreen.DiscoverNewObject((int)_object);

            //ANIMACION DEL OBJETO
            background.SetActive(true);
            StartAnimationObject();
            return;
        }
        else if (isShowingObject && GeneralInputScript.Input_anyKeyDown() && !startAnimation)
        {
            CloseScreen();
        }
        else if (isOpen && GeneralInputScript.Input_GetKeyDown("Secrets"))
        {
            CloseScreen();
        }
    }
    #endregion

    #region CLOSE
    void CloseScreen()
    {
        isOpen = false;
        close.Play();

        isShowingObject = false;

        //playerMove.canMove = true;
        playerMove.StopMovement(false);

        background.SetActive(false);

        Destroy(objectInstantiate);

        Destroy(this.gameObject);
    }
    #endregion

    #region DISCOVER OBJECT
    void DiscoverObject()
    {
        switch (_object)
        {
            case Object.OBJECT_1:
                {
                    PlayerPrefs.SetInt("Secret1", 1);
                    break;
                }
            case Object.OBJECT_2:
                {
                    PlayerPrefs.SetInt("Secret2", 1);
                    break;
                }
            case Object.OBJECT_3:
                {
                    PlayerPrefs.SetInt("Secret3", 1);
                    break;
                }
            case Object.OBJECT_4:
                {
                    PlayerPrefs.SetInt("Secret4", 1);
                    break;
                }
            case Object.OBJECT_5:
                {
                    PlayerPrefs.SetInt("Secret5", 1);
                    break;
                }
            case Object.OBJECT_6:
                {
                    PlayerPrefs.SetInt("Secret6", 1);
                    break;
                }
            default:
                break;
        }
    }
    #endregion

    #region ANIMATION  OBJECT
    private void StartAnimationObject()
    {
        tuto.DeactivateSprites();
        //TO DO: ANIMATION  OBJECT
        objectInstantiate =  Instantiate(secretObject, spawnPosition.position, spawnPosition.rotation) as GameObject;
        objectInstantiate.transform.SetParent(spawnPosition);


    }
    #endregion

    #region TRIGGER ENTER
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }
    #endregion

    #region TRIGGER EXIT
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }
    #endregion

    #region ANIMATION TEXT
    void AnimationText()
    {
        text.transform.DOScale(sizeAnimationText ,0.5f);
    }
    #endregion
}
