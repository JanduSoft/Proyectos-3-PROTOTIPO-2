using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePartsOfMap : MonoBehaviour
{
    public enum typeOfGO
    {
        NONE,
        TO_ENABLE,
        TO_DISABLE
    }
    [SerializeField] GameObject go;
    [SerializeField] typeOfGO typeGO;

    private SectionMapVariables myPart;

    private void Start()
    {
        myPart = go.GetComponent<SectionMapVariables>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (typeGO)
            {
                case typeOfGO.NONE:
                    break;
                case typeOfGO.TO_ENABLE:
                    {
                        if (myPart.isActive)
                        {
                            go.SetActive(true);
                        }                        
                        break;
                    }
                case typeOfGO.TO_DISABLE:
                    {
                        if (myPart.isActive)
                        {
                            go.SetActive(false);
                        }
                        break;
                    }
                default:
                    break;
            }
        }
    }
        
}
