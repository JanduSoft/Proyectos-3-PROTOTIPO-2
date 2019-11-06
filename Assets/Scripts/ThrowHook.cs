using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowHook : MonoBehaviour
{
    [SerializeField] bool canWhip = false;
    [SerializeField] bool canWrap = false;
    [SerializeField] Transform hookSpawner;
    [SerializeField] PlayerController player;
    private GameObject objectToWrap;
    public GameObject hook;
    GameObject currentHook;
    public Vector3 destiny;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canWhip)
        {
            if(currentHook == null)
            {
                currentHook = (GameObject)Instantiate(hook, hookSpawner.position, hookSpawner.rotation);
                currentHook.GetComponent<RopeScript>().destiny = destiny;
                player.onWhip = true;
            }
            else
            {
                Destroy(currentHook.gameObject);
                player.onWhip = false;
            }

        }
        else if (Input.GetMouseButtonDown(0) && canWrap)
        {
            if (currentHook == null)
            {
                currentHook = (GameObject)Instantiate(hook, hookSpawner.position, hookSpawner.rotation);
                currentHook.GetComponent<RopeScript>().destiny = destiny;
            }
            else
            {
                Destroy(currentHook.gameObject);
            }
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WhipPoint"))
        {
            canWhip = true;
            destiny = other.transform.position;
        }
        else if (other.CompareTag("WrapPoint"))
        {
            canWrap = true;
            destiny = other.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WhipPoint"))
        {
            canWhip = false;
            destiny = new Vector3(0,0,0);
        }
        else if (other.CompareTag("WrapPoint"))
        {
            canWrap = false;
            destiny = new Vector3(0, 0, 0);
        }
    }
}
