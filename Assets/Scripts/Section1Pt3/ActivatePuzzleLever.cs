using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePuzzleLever : MonoBehaviour
{
    // Start is called before the first frame update
    bool isActive = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetLever()
    {
        isActive = false;
    }

    public bool Get_IsActive() { return isActive; }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Interact"))
            {
                isActive = true;
            }
        }
    }
}
