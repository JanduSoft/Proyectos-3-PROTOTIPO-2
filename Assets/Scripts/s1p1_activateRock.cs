using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s1p1_activateRock : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PickUpDragandDrop rockToActivate;
    [SerializeField] GameObject rockTuto;
    void Start()
    {
        rockToActivate.enabled = false;
        rockTuto.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rockToActivate.enabled = true;
            rockTuto.SetActive(true);
        }
    }
}
