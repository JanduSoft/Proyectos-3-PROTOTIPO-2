using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePartsOfMap : MonoBehaviour
{
    enum typeOfGO
    {
        NONE,
        TO_ENABLE,
        TO_DISABLE
    }
    [SerializeField] typeOfGO typeGO;
    [SerializeField] GameObject go;

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
                        go.SetActive(true);
                        break;
                    }
                case typeOfGO.TO_DISABLE:
                    {
                        go.SetActive(false);
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
