using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLevel1 : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] GameObject light;
    #endregion

    #region ON TRIGGER ENTER/EXIT
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            light.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            light.SetActive(false);
        }
    }
    #endregion
}
