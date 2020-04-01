using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

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
    GameObject objectInstantiate;
    #endregion


    #region UPDATE
    void Update()
    {
        if ( isPlayerInside && InputManager.ActiveDevice.Action3.WasPressed && !isShowingObject)
        {
            isShowingObject = true;
            playerMove.canMove = false;

            tutoSprites.DeactivateSprites();
            //ACTUALIZAMOS LOS PLAYER PREFS;
            DiscoverObject();

            //ACTUALIZAMOS EL MENU
            secretScreen.DiscoverNewObject((int)_object);

            //ANIMACION DEL OBJETO
            StartAnimationObject();
            return;
        }
        else if (isShowingObject && InputManager.ActiveDevice.Action3.WasPressed)
        {
            isShowingObject = false;
            playerMove.canMove = true;

            Destroy(objectInstantiate);

            Destroy(this.gameObject);
        }
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
}
