using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialSprites : MonoBehaviour
{
    #region VARIABLES
    public enum buttonType
    {
        NONE,
        JUMP,
        INTERACT,
        WHIP,
        PLACE_OBJECT,
        DD_OBJECT,
        PICK_UP_OBJECT,
        THROW
        
    };
   
    bool isPlayerInside = false;
    bool isObjectInside = false;


    bool showingButton = false;
    public bool needShowTrhowButton = false;

    [Header("REFERENCES")]
    [SerializeField] DragAndDrop dragAndDrop;
    [SerializeField] PickUpDropandThrow pickUpThrow;

    [Header("SPRITES")]
    [SerializeField] buttonType button;
    [SerializeField] GameObject jump;
    [SerializeField] GameObject interact;
    [SerializeField] GameObject whip;
    [SerializeField] GameObject pickThrow;

    [Header("TRANSITION")]
    [SerializeField] float speed;
    [SerializeField] float inititalSize;
    [SerializeField] float finalSize;
    #endregion

    #region UPDATE
    private void Update()
    {        


        if (button == buttonType.PLACE_OBJECT)
        {
            if (isPlayerInside && isObjectInside)
            {
                if (!showingButton)
                {
                    showingButton = true;
                    ActivateSprite(button);
                }
            }
        }
        else if(button == buttonType.PLACE_OBJECT)
        {
            if (isPlayerInside && pickUpThrow.GetObjectIsGrabbed())
            {
                DeactivateSprites();
            }
            else if (isPlayerInside && !pickUpThrow.GetObjectIsGrabbed())
            {

            }
        }
        else if (button == buttonType.THROW)
        {
            if (isPlayerInside && pickUpThrow.GetObjectIsGrabbed())
            {
                if (needShowTrhowButton)
                {
                    interact.SetActive(false);
                    pickThrow.SetActive(true);
                }


            }
            else if (isPlayerInside && !pickUpThrow.GetObjectIsGrabbed())
            {
                pickThrow.SetActive(false);
                interact.SetActive(true);
            }
            else
            {
                //DeactivateSprites();
            }

        }



    }
    #endregion

    #region TRIGGER ENTER
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;

            switch (button)
            {
                case buttonType.NONE:
                    break;
                case buttonType.JUMP:
                    {
                        ActivateSprite(button);
                        break;
                    }
                case buttonType.INTERACT:
                    {
                        ActivateSprite(button);
                        break;
                    }
                case buttonType.WHIP:
                    {
                        ActivateSprite(button);
                        break;
                    }
                case buttonType.PLACE_OBJECT:
                    {
                        if (isObjectInside)
                        {
                            ActivateSprite(button);
                        }                        
                        break;
                    }
                case buttonType.DD_OBJECT:
                    {
                        if (!dragAndDrop.objectIsGrabbed)
                        {
                            ActivateSprite(button);
                        }
                        break;
                    }
                case buttonType.PICK_UP_OBJECT:
                    {
                        
                        if (!pickUpThrow.GetObjectIsGrabbed())
                        {
                            ActivateSprite(button);
                        }
                        break;
                    }
                case buttonType.THROW:
                    {
                        ActivateSprite(buttonType.INTERACT);
                        break;
                    }
                default:
                    break;
            }
        }
        if ((other.CompareTag("Place") || other.CompareTag("Skull")) && button == buttonType.PLACE_OBJECT)
        {
            isObjectInside = true;           
        }
    }
    #endregion

    #region TRIGGER EXIT
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            DeactivateSprites();

            switch (button)
            {
                case buttonType.NONE:
                    break;
                case buttonType.JUMP:
                    {
                        DeactivateSprites();
                        break;
                    }
                case buttonType.INTERACT:
                    {
                        DeactivateSprites();
                        break;
                    }
                case buttonType.WHIP:
                    {
                        DeactivateSprites();
                        break;
                    }
                case buttonType.PLACE_OBJECT:
                    {
                        showingButton = false;
                        DeactivateSprites();
                        break;
                    }
                case buttonType.DD_OBJECT:
                    {
                        DeactivateSprites();
                        break;
                    }
                case buttonType.PICK_UP_OBJECT:
                    {
                        DeactivateSprites();
                        break;
                    }
                case buttonType.THROW:
                    {
                        DeactivateSprites();
                        break;
                    }
                default:
                    break;
            }
        }
        if ( ( other.CompareTag("Place") || other.CompareTag("Skull") ) && button == buttonType.PLACE_OBJECT)
        {
            isObjectInside = false;
        }
    }
    #endregion

    #region ACTIVATE SPRITE
    public void ActivateSprite(buttonType _button)
    {
        switch (button)
        {
            case buttonType.NONE:
                break;
            case buttonType.JUMP:
                {
                    jump.SetActive(true);
                    jump.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                    jump.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                    break;
                }
            case buttonType.INTERACT:
                {
                    interact.SetActive(true);
                    interact.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                    interact.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                    break;
                }
            case buttonType.WHIP:
                {
                    whip.SetActive(true);
                    whip.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                    whip.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                    break;
                }
            case buttonType.PLACE_OBJECT:
                {
                    interact.SetActive(true);
                    interact.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                    interact.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                    break;
                }
            case buttonType.DD_OBJECT:
                {
                    interact.SetActive(true);
                    interact.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                    interact.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                    break;
                };
            case buttonType.PICK_UP_OBJECT:
                {
                    interact.SetActive(true);
                    interact.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                    interact.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                    break;
                }
            case buttonType.THROW:
                {
                    pickThrow.SetActive(true);
                    pickThrow.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                    pickThrow.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                    break;
                }
            default:
                break;
        }
        
    }
    #endregion

    #region DEACTIVATE SPRITES
    public void DeactivateSprites()
    {
        switch (button)
        {
            case buttonType.NONE:
                break;
            case buttonType.JUMP:
                {
                    jump.SetActive(false);
                    break;
                }
            case buttonType.INTERACT:
                {
                    interact.SetActive(false);
                    break;
                }
            case buttonType.WHIP:
                {
                    whip.SetActive(false);
                    break;
                }
            case buttonType.PLACE_OBJECT:
                {
                    interact.SetActive(false);
                    break;
                }
            case buttonType.DD_OBJECT:
                {
                    interact.SetActive(false);
                    break;
                }
            case buttonType.PICK_UP_OBJECT:
                {
                    interact.SetActive(false);
                    break;
                }
            case buttonType.THROW:
                {
                    pickThrow.SetActive(false);
                    interact.SetActive(false);
                    break;
                }
            default:
                break;
        }
    }
    #endregion

}
