using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialSprites : MonoBehaviour
{
    public enum buttonType
    {
        NONE,
        JUMP,
        INTERACT,
        WHIP
        
    };

    [Header("SPRITES")]
    [SerializeField] buttonType button;
    [SerializeField] GameObject jump;
    [SerializeField] GameObject interact;
    [SerializeField] GameObject whip;

    [Header("TRANSITION")]
    [SerializeField] float speed;
    [SerializeField] float inititalSize;
    [SerializeField] float finalSize;

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
                        jump.SetActive(true);
                        jump.transform.localScale= new Vector3(inititalSize, inititalSize, inititalSize);
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
                default:
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (button)
            {
                case buttonType.NONE:
                    break;
                case buttonType.JUMP:
                    {                        
                        jump.transform.DOScale(new Vector3(inititalSize, inititalSize, inititalSize), speed);
                        Invoke("DeactivateSprites", speed);
                        break;
                    }
                case buttonType.INTERACT:
                    {
                        interact.transform.DOScale(new Vector3(inititalSize, inititalSize, finalSize), speed);
                        Invoke("DeactivateSprites", speed);
                        break;
                    }
                case buttonType.WHIP:
                    {
                        whip.transform.DOScale(new Vector3(inititalSize, inititalSize, inititalSize), speed);
                        Invoke("DeactivateSprites", speed);
                        break;
                    }
                default:
                    break;
            }
        }
    }

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
            default:
                break;
        }
    }
}
