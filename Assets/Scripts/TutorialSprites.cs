using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using InControl;

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
        THROW,
        MOVE_ROCK,
        NEW_PLACE_OBJECT
        
    };

    public bool isPlayerInside = false;
    public bool isObjectInside = false;


    bool showingButton = false;
    public bool needShowTrhowButton = false;

    [Header("REFERENCES")]
    [SerializeField] DragAndDrop dragAndDrop;
    [SerializeField] PickUpDropandThrow pickUpThrow;

    [Header("SPRITES")]
    [SerializeField] buttonType button;

    [Header("KEYBOARD")]
    [SerializeField] GameObject jump;
    [SerializeField] GameObject interact;
    [SerializeField] GameObject whip;
    [SerializeField] GameObject pickThrow;
    [Header("PS4")]
    [SerializeField] GameObject jumpPS4;
    [SerializeField] GameObject interactPS4;
    [SerializeField] GameObject whipPS4;
    [SerializeField] GameObject pickThrowPS4;
    [Header("XBOX")]
    [SerializeField] GameObject jumpXBOX;
    [SerializeField] GameObject interactXBOX;
    [SerializeField] GameObject whipXBOX;
    [SerializeField] GameObject pickThrowXBOX;

    [Header("TRANSITION")]
    [SerializeField] float speed;
    [SerializeField] float inititalSize;
    private float finalSize = 0.4f;

    InControlManager inputController;
    public InControlManager.ControllerType currentController;

    PlayerMovement player;
    #endregion

    #region START
    private void Start()
    {
        if (button == buttonType.NEW_PLACE_OBJECT || button == buttonType.THROW)
        {
            player = GameObject.Find("Character").GetComponent<PlayerMovement>();
        }
        

        inputController = GameObject.Find("ControlPrefab").GetComponent<InControlManager>();
        Invoke("CheckController", 1);
    }
    #endregion

    #region UPDATE
    private void Update()
    {
        CheckController();

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
                    if (player.isOnPedestal)
                    {
                        switch (currentController)
                        {
                            case InControlManager.ControllerType.KEYBOARD:
                                {
                                    interact.SetActive(true);
                                    whip.SetActive(false);
                                    break;
                                }
                            case InControlManager.ControllerType.PS4:
                                {
                                    interactPS4.SetActive(true);
                                    whipPS4.SetActive(false);
                                    break;
                                }
                            case InControlManager.ControllerType.XBOX:
                                {
                                    interactXBOX.SetActive(true);
                                    whipXBOX.SetActive(false);
                                    break;
                                }
                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (currentController)
                        {
                            case InControlManager.ControllerType.KEYBOARD:
                                {
                                    interact.SetActive(false);
                                    whip.SetActive(true);
                                    break;
                                }
                            case InControlManager.ControllerType.PS4:
                                {
                                    interactPS4.SetActive(false);
                                    whipPS4.SetActive(true);
                                    break;
                                }
                            case InControlManager.ControllerType.XBOX:
                                {
                                    interactXBOX.SetActive(false);
                                    whipXBOX.SetActive(true);
                                    break;
                                }
                            default:
                                break;
                        }
                    }
                    
                    
                }


            }
            else if (isPlayerInside && !pickUpThrow.GetObjectIsGrabbed())
            {
                switch (currentController)
                {
                    case InControlManager.ControllerType.KEYBOARD:
                        {
                            whip.SetActive(false);
                            interact.SetActive(true);
                            break;
                        }
                    case InControlManager.ControllerType.PS4:
                        {
                            whipPS4.SetActive(false);
                            interactPS4.SetActive(true);
                            break;
                        }
                    case InControlManager.ControllerType.XBOX:
                        {
                            whipXBOX.SetActive(false);
                            interactXBOX.SetActive(true);
                            break;
                        }
                    default:
                        break;
                }
                
            }
            else
            {
                //DeactivateSprites();
            }

        }
        else if (button == buttonType.MOVE_ROCK)
        {
            if (isPlayerInside)
            {
                bool isPressingButton = InputManager.ActiveDevice.Action3;
                if (isPressingButton)
                {
                    DeactivateSprites();
                }
                else
                {
                    ActivateSprite(buttonType.INTERACT);
                }
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
                        if (player.ableToWhip)
                        {
                            ActivateSprite(buttonType.INTERACT);
                        }
                        //ActivateSprite(buttonType.INTERACT);
                        break;
                    }
                case buttonType.MOVE_ROCK:
                    {
                        ActivateSprite(buttonType.INTERACT);
                        break;
                    }
                case buttonType.NEW_PLACE_OBJECT:
                    {
                        player.isOnPedestal = true;
                        break;
                    }
                default:
                    break;
            }
        }
        if ((other.CompareTag("Place") || other.CompareTag("Skull")) && button == buttonType.PLACE_OBJECT)
        {
            isObjectInside = true;
            pickUpThrow = other.GetComponent<PickUpDropandThrow>();
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
                case buttonType.NEW_PLACE_OBJECT:
                    {
                        player.isOnPedestal = false;
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
                    switch (currentController)
                    {
                        case InControlManager.ControllerType.KEYBOARD:
                            {
                                jump.SetActive(true);
                                jump.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                                jump.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                                break;
                            }
                        case InControlManager.ControllerType.PS4:
                            {
                                jumpPS4.SetActive(true);
                                jumpPS4.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                                jumpPS4.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                                break;
                            }
                        case InControlManager.ControllerType.XBOX:
                            {
                                jumpXBOX.SetActive(true);
                                jumpXBOX.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                                jumpXBOX.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                                break;
                            }
                        default:
                            break;
                    }
                    break;
                }
            case buttonType.INTERACT:
                {
                    switch (currentController)
                    {

                        case InControlManager.ControllerType.KEYBOARD:
                            {
                                Debug.Log("NO DEBERIA ENTRAR AQUI");
                                //Desctivamos las otras
                                interactXBOX.SetActive(false);
                                interactPS4.SetActive(false);

                                interact.SetActive(true);
                                interact.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                                interact.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                                break;
                            }
                        case InControlManager.ControllerType.PS4:
                            {
                                //Desctivamos las otras
                                interact.SetActive(false);
                                interactXBOX.SetActive(false);

                                interactPS4.SetActive(true);
                                interactPS4.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                                interactPS4.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                                break;
                            }
                        case InControlManager.ControllerType.XBOX:
                            {
                                interactXBOX.SetActive(true);
                                interactXBOX.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                                interactXBOX.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                                break;
                            }
                        default:
                            break;
                    }
                    
                    break;
                }
            case buttonType.WHIP:
                {
                    switch (currentController)
                    {
                        case InControlManager.ControllerType.KEYBOARD:
                            {
                                whip.SetActive(true);
                                whip.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                                whip.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                                break;
                            }
                        case InControlManager.ControllerType.PS4:
                            {
                                whipPS4.SetActive(true);
                                whipPS4.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                                whipPS4.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                                break;
                            }
                        case InControlManager.ControllerType.XBOX:
                            {
                                whipXBOX.SetActive(true);
                                whipXBOX.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                                whipXBOX.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                                break;
                            }
                        default:
                            break;
                    }
                    break;
                }
            case buttonType.PLACE_OBJECT:
                {
                    switch (currentController)
                    {
                        case InControlManager.ControllerType.KEYBOARD:
                            {
                                interact.SetActive(true);
                                interact.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                                interact.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                                break;
                            }
                        case InControlManager.ControllerType.PS4:
                            {
                                interactPS4.SetActive(true);
                                interactPS4.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                                interactPS4.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                                break;
                            }
                        case InControlManager.ControllerType.XBOX:
                            {
                                interactXBOX.SetActive(true);
                                interactXBOX.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                                interactXBOX.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                                break;
                            }
                        default:
                            break;
                    }
                    break;
                }
            case buttonType.DD_OBJECT:
                {
                    switch (currentController)
                    {
                        case InControlManager.ControllerType.KEYBOARD:
                            {
                                interact.SetActive(true);
                                interact.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                                interact.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                                break;
                            }
                        case InControlManager.ControllerType.PS4:
                            {
                                interactPS4.SetActive(true);
                                interactPS4.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                                interactPS4.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                                break;
                            }
                        case InControlManager.ControllerType.XBOX:
                            {
                                interactXBOX.SetActive(true);
                                interactXBOX.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                                interactXBOX.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                                break;
                            }
                        default:
                            break;
                    }
                    break;
                };
            case buttonType.PICK_UP_OBJECT:
                {
                    switch (currentController)
                    {
                        case InControlManager.ControllerType.KEYBOARD:
                            {
                                interact.SetActive(true);
                                interact.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                                interact.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                                break;
                            }
                        case InControlManager.ControllerType.PS4:
                            {
                                interactPS4.SetActive(true);
                                interactPS4.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                                interactPS4.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                                break;
                            }
                        case InControlManager.ControllerType.XBOX:
                            {
                                interactXBOX.SetActive(true);
                                interactXBOX.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                                interactXBOX.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                                break;
                            }
                        default:
                            break;
                    }
                    break;
                }
            case buttonType.THROW:
                {
                    switch (currentController)
                    {
                        case InControlManager.ControllerType.KEYBOARD:
                            {
                                whip.SetActive(true);
                                whip.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                                whip.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                                break;
                            }
                        case InControlManager.ControllerType.PS4:
                            {
                                whipPS4.SetActive(true);
                                whipPS4.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                                whipPS4.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                                break;
                            }
                        case InControlManager.ControllerType.XBOX:
                            {
                                whipXBOX.SetActive(true);
                                whipXBOX.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                                whipXBOX.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                                break;
                            }
                        default:
                            break;
                    }
                    break;
                }
            case buttonType.MOVE_ROCK:
                {
                    switch (currentController)
                    {
                        case InControlManager.ControllerType.KEYBOARD:
                            {
                                interact.SetActive(true);
                                interact.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                                interact.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                                break;
                            }
                        case InControlManager.ControllerType.PS4:
                            {
                                interactPS4.SetActive(true);
                                interactPS4.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                                interactPS4.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                                break;
                            }
                        case InControlManager.ControllerType.XBOX:
                            {
                                interactXBOX.SetActive(true);
                                interactXBOX.transform.localScale = new Vector3(inititalSize, inititalSize, inititalSize);
                                interactXBOX.transform.DOScale(new Vector3(finalSize, finalSize, finalSize), speed);
                                break;
                            }
                        default:
                            break;
                    }
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
        #region OLD LOGIC DEACTIVATE TUTOSPRITE
        //switch (button)
        //{
        //    case buttonType.NONE:
        //        break;
        //    case buttonType.JUMP:
        //        {
        //            switch (currentController)
        //            {
        //                case InControlManager.ControllerType.KEYBOARD:
        //                    {
        //                        jump.SetActive(false);
        //                        break;
        //                    }
        //                case InControlManager.ControllerType.PS4:
        //                    {
        //                        jumpPS4.SetActive(false);
        //                        break;
        //                    }
        //                case InControlManager.ControllerType.XBOX:
        //                    {
        //                        jumpXBOX.SetActive(false);
        //                        break;
        //                    }
        //                default:
        //                    break;
        //            }
        //            break;
        //        }
        //    case buttonType.INTERACT:
        //        {
        //            switch (currentController)
        //            {
        //                case InControlManager.ControllerType.KEYBOARD:
        //                    {
        //                        interact.SetActive(false);
        //                        break;
        //                    }
        //                case InControlManager.ControllerType.PS4:
        //                    {
        //                        interactPS4.SetActive(false);
        //                        break;
        //                    }
        //                case InControlManager.ControllerType.XBOX:
        //                    {
        //                        interactXBOX.SetActive(false);
        //                        break;
        //                    }
        //                default:
        //                    break;
        //            }
        //            break;
        //        }
        //    case buttonType.WHIP:
        //        {
        //            switch (currentController)
        //            {
        //                case InControlManager.ControllerType.KEYBOARD:
        //                    {
        //                        whip.SetActive(false);
        //                        break;
        //                    }
        //                case InControlManager.ControllerType.PS4:
        //                    {
        //                        whipPS4.SetActive(false);
        //                        break;
        //                    }
        //                case InControlManager.ControllerType.XBOX:
        //                    {
        //                        whipXBOX.SetActive(false);
        //                        break;
        //                    }
        //                default:
        //                    break;
        //            }
        //            break;
        //        }
        //    case buttonType.PLACE_OBJECT:
        //        {
        //            switch (currentController)
        //            {
        //                case InControlManager.ControllerType.KEYBOARD:
        //                    {
        //                        interact.SetActive(false);
        //                        break;
        //                    }
        //                case InControlManager.ControllerType.PS4:
        //                    {
        //                        interactPS4.SetActive(false);
        //                        break;
        //                    }
        //                case InControlManager.ControllerType.XBOX:
        //                    {
        //                        interactXBOX.SetActive(false);
        //                        break;
        //                    }
        //                default:
        //                    break;
        //            }
        //            break;
        //        }
        //    case buttonType.DD_OBJECT:
        //        {
        //            switch (currentController)
        //            {
        //                case InControlManager.ControllerType.KEYBOARD:
        //                    {
        //                        interact.SetActive(false);
        //                        break;
        //                    }
        //                case InControlManager.ControllerType.PS4:
        //                    {
        //                        interactPS4.SetActive(false);
        //                        break;
        //                    }
        //                case InControlManager.ControllerType.XBOX:
        //                    {
        //                        interactXBOX.SetActive(false);
        //                        break;
        //                    }
        //                default:
        //                    break;
        //            }
        //            break;
        //        }
        //    case buttonType.PICK_UP_OBJECT:
        //        {
        //            switch (currentController)
        //            {
        //                case InControlManager.ControllerType.KEYBOARD:
        //                    {
        //                        interact.SetActive(false);
        //                        break;
        //                    }
        //                case InControlManager.ControllerType.PS4:
        //                    {
        //                        interactPS4.SetActive(false);
        //                        break;
        //                    }
        //                case InControlManager.ControllerType.XBOX:
        //                    {
        //                        interactXBOX.SetActive(false);
        //                        break;
        //                    }
        //                default:
        //                    break;
        //            }
        //            break;
        //        }
        //    case buttonType.THROW:
        //        {
        //            switch (currentController)
        //            {
        //                case InControlManager.ControllerType.KEYBOARD:
        //                    {
        //                        interact.SetActive(false);
        //                        whip.SetActive(false);
        //                        break;
        //                    }
        //                case InControlManager.ControllerType.PS4:
        //                    {
        //                        interactPS4.SetActive(false);
        //                        whipPS4.SetActive(false);
        //                        break;
        //                    }
        //                case InControlManager.ControllerType.XBOX:
        //                    {
        //                        interactXBOX.SetActive(false);
        //                        whipXBOX.SetActive(false);
        //                        break;
        //                    }
        //                default:
        //                    break;
        //            }                    
        //            break;
        //        }
        //    case buttonType.MOVE_ROCK:
        //        {
        //            switch (currentController)
        //            {
        //                case InControlManager.ControllerType.KEYBOARD:
        //                    {
        //                        interact.SetActive(false);
        //                        break;
        //                    }
        //                case InControlManager.ControllerType.PS4:
        //                    {
        //                        interactPS4.SetActive(false);
        //                        break;
        //                    }
        //                case InControlManager.ControllerType.XBOX:
        //                    {
        //                        interactXBOX.SetActive(false);
        //                        break;
        //                    }
        //                default:
        //                    break;
        //            }
        //            break;
        //        }
        //    default:
        //        break;
        //}
        #endregion

        jump.SetActive(false);
        interact.SetActive(false);
        whip.SetActive(false);
        pickThrow.SetActive(false);
        
        jumpPS4.SetActive(false);
        interactPS4.SetActive(false);
        whipPS4.SetActive(false);
        pickThrowPS4.SetActive(false);
        
        jumpXBOX.SetActive(false);
        interactXBOX.SetActive(false);
        whipXBOX.SetActive(false);
        pickThrowXBOX.SetActive(false);
}
    #endregion

    #region CHECK CONTROLLER
    void CheckController()
    {
        switch (inputController.controller)
        {
            case InControlManager.ControllerType.NONE:
                break;
            case InControlManager.ControllerType.KEYBOARD:

                currentController = InControlManager.ControllerType.KEYBOARD;
                break;
            case InControlManager.ControllerType.PS4:
                currentController = InControlManager.ControllerType.PS4;
                break;
            case InControlManager.ControllerType.XBOX:
                currentController = InControlManager.ControllerType.XBOX;
                break;
            default:
                break;
        }
    }
    #endregion
}
