using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoorTutorialpt2 : MonoBehaviour
{
    bool canPlace = false;
    [SerializeField] GameObject dor;
    [SerializeField] int index;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canPlace && Input.GetButtonDown("Interact"))
        {
            dor.SendMessage("Solved", index);
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPlace = true;
        }

    }
    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPlace = false;
        }

    }
}
