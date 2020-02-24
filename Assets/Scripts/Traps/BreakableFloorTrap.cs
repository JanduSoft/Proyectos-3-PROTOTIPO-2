using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableFloorTrap : MonoBehaviour
{
    [SerializeField] GameObject destroyableObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (destroyableObj.activeInHierarchy == false)
            {
                destroyableObj.SetActive(true);
                transform.parent.gameObject.SetActive(false);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Player"))    
        {
            if(destroyableObj.activeInHierarchy == false)
            {
                destroyableObj.SetActive(true);
                transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
