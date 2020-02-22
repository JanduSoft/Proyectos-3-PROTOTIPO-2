using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGO : MonoBehaviour
{
    public enum ObjectCondition
    {
        NONE,
        ACTIVATE,
        DESACTIVATE
    }
    public enum Interactuable
    {
        NONE,
        PLAYER,
        BLOCK,
        FIRE,
        ENEMY
    }

    [SerializeField] GameObject[] objects;
    [SerializeField] ObjectCondition condition;
    [SerializeField] Interactuable interactuable;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && interactuable == Interactuable.PLAYER)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                switch (condition)
                {
                    case ObjectCondition.NONE:
                        break;
                    case ObjectCondition.ACTIVATE:
                        objects[i].SetActive(true);
                        break;
                    case ObjectCondition.DESACTIVATE:
                        objects[i].SetActive(false);
                        break;
                    default:
                        break;
                }
            }
        }
        else if (other.CompareTag("Block") && interactuable == Interactuable.BLOCK)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                switch (condition)
                {
                    case ObjectCondition.NONE:
                        break;
                    case ObjectCondition.ACTIVATE:
                        objects[i].SetActive(true);
                        break;
                    case ObjectCondition.DESACTIVATE:
                        objects[i].SetActive(false);
                        break;
                    default:
                        break;
                }
            }
        }
        else if (other.CompareTag("Fire") && interactuable == Interactuable.FIRE)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                switch (condition)
                {
                    case ObjectCondition.NONE:
                        break;
                    case ObjectCondition.ACTIVATE:
                        objects[i].SetActive(true);
                        break;
                    case ObjectCondition.DESACTIVATE:
                        objects[i].SetActive(false);
                        break;
                    default:
                        break;
                }
            }
        }
        else if (other.CompareTag("Skull") && interactuable == Interactuable.ENEMY)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                switch (condition)
                {
                    case ObjectCondition.NONE:
                        break;
                    case ObjectCondition.ACTIVATE:
                        objects[i].SetActive(true);
                        break;
                    case ObjectCondition.DESACTIVATE:
                        objects[i].SetActive(false);
                        break;
                    default:
                        break;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Block") && interactuable == Interactuable.BLOCK)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].SetActive(false);   
            }
        }
    }

}
