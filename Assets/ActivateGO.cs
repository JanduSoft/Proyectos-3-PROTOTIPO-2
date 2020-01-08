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

    public GameObject[] objects;
    public ObjectCondition condition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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

}
