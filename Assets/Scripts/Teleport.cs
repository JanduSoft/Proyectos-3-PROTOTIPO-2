using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] Transform spawnPosition;
    [SerializeField] GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            Debug.Log("Detecta la entrada");
            other.gameObject.transform.position = spawnPosition.position;
            Debug.Log("amor");
        }

    }
}
