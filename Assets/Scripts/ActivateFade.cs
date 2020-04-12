using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DFTGames.Tools;

public class ActivateFade : MonoBehaviour
{
    [SerializeField] GameObject camera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            camera.GetComponent<FadeObstructors>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            camera.GetComponent<FadeObstructors>().enabled = false;
        }
    }
}
