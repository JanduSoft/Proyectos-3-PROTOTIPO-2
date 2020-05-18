using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMision : MonoBehaviour
{

    public enum Type
    {
        NONE,
        PLACE_OBJECT,
        CHECK_POINT
    }

    [SerializeField] GameObject checkMark;
    [SerializeField] Type updateType;
    public string objectName;

    private void OnTriggerEnter(Collider other)
    {
        
        if (updateType == Type.CHECK_POINT)
        {
            if (other.CompareTag("Player"))
            {
                checkMark.SetActive(true);
            }
        }        
        else if (updateType == Type.PLACE_OBJECT)
        {
            if (other.name == objectName)
            {
                checkMark.SetActive(true);
            }
        }       

        
    }
}
