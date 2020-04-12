using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlaceAnyObject : MonoBehaviour
{
    public enum ObjectsToPlace
    {
        NONE,
        SKULL,
        TORCH
    }

    [SerializeField] ObjectsToPlace typeOfObject;
    bool isPlayerInside = false;
    bool isObjectInside = false;
    bool isPlaced = false;
    GameObject otherObject;


    // Update is called once per frame
    void Update()
    {
        
        if (isPlayerInside && isPlaced && InputManager.ActiveDevice.Action3.WasPressed )
        {
            switch (typeOfObject)
            {
                case ObjectsToPlace.NONE:
                    {
                        Debug.Log("CHOSE ONE TYPE OF OBJECT");
                        break;
                    }
                case ObjectsToPlace.SKULL:
                    {
                        break;
                    }
                case ObjectsToPlace.TORCH:
                    {
                        Debug.Log("RecogerAntorcha");
                        isPlaced = false;
                        break;
                    }
                default:
                    {
                        Debug.Log("CHOSE ONE TYPE OF OBJECT");
                        break;
                    }
            }
        }
        else if (isPlayerInside && isObjectInside && !isPlaced && InputManager.ActiveDevice.Action3.WasPressed)
        {
            switch (typeOfObject)
            {
                case ObjectsToPlace.NONE:
                    {
                        Debug.Log("CHOSE ONE TYPE OF OBJECT");
                        break;
                    }
                case ObjectsToPlace.SKULL:
                    {
                        break;
                    }
                case ObjectsToPlace.TORCH:
                    {
                        Debug.Log("Dejar Antorcha");

                        otherObject.GetComponent<TorchPuzzles>().objectIsGrabbed = false;

                        otherObject.transform.position = this.transform.position;

                        otherObject.transform.SetParent(null);
                        Vector3 auxScale = otherObject.transform.localScale; 
                        otherObject.transform.SetParent(this.transform);
                        //otherObject.transform.localScale = auxScale;

                        isPlaced = true;
                        break;
                    }
                default:
                    {
                        Debug.Log("CHOSE ONE TYPE OF OBJECT");
                        break;
                    }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
        }

        switch (typeOfObject)
        {
            case ObjectsToPlace.NONE:
                break;
            case ObjectsToPlace.SKULL:
                {
                    if (other.CompareTag("Skull"))
                    {
                        isObjectInside = true;
                    }
                    break;
                }
            case ObjectsToPlace.TORCH:
                {
                    if (other.CompareTag("Place"))
                    {
                        otherObject = other.gameObject;
                        isObjectInside = true;
                    }
                    break;
                }
            default:
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }

        switch (typeOfObject)
        {
            case ObjectsToPlace.NONE:
                break;
            case ObjectsToPlace.SKULL:
                {
                    if (other.CompareTag("Skull"))
                    {
                        isObjectInside = false;
                    }
                    break;
                }
            case ObjectsToPlace.TORCH:
                {
                    if (other.CompareTag("Place"))
                    {
                        otherObject = null;
                        isObjectInside = false;
                    }
                    break;
                }
            default:
                break;
        }
    }
}
