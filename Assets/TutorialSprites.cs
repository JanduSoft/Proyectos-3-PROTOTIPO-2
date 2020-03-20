﻿using System.Collections;
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
        PICK_UP_OBJECT
        
    };
   
    bool isPlayerInside = false;
    bool isObjectInside = false;
    [Header("REFERENCES")]
    [SerializeField] DragAndDrop dragAndDrop;
    [SerializeField] PickUpandDrop pickUP;

    [Header("SPRITES")]
    [SerializeField] buttonType button;
    [SerializeField] GameObject jump;
    [SerializeField] GameObject interact;
    [SerializeField] GameObject whip;

    [Header("TRANSITION")]
    [SerializeField] float speed;
    [SerializeField] float inititalSize;
    [SerializeField] float finalSize;
    #endregion

    #region UPDATE
    private void Update()
    {
        if (isPlayerInside && pickUP.GetObjectIsGrabbed())
        {
            DeactivateSprites();
        }
        else if (isPlayerInside && !pickUP.GetObjectIsGrabbed() )
        {

        }
    }
    #endregion

    #region TRIGGER ENTER
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
                        isPlayerInside = true;
                        if (!pickUP.GetObjectIsGrabbed())
                        {
                            ActivateSprite(button);
                        }
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
    public void ActivateSprite(buttonType button)
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
            default:
                break;
        }
    }
    #endregion

}
