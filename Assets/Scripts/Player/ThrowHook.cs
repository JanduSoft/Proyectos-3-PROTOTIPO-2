using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class ThrowHook : MonoBehaviour
{
    [SerializeField] bool canWhip = false;
    [SerializeField] bool canWrap = false;
    [SerializeField] Transform hookSpawner;
    [SerializeField] float hookLife;

   // [SerializeField] PlayerController player;
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
        if (InputManager.ActiveDevice.Action4.WasPressed && canWhip)
        {
            if(currentHook == null)
            {
                currentHook = (GameObject)Instantiate(hook, hookSpawner.position, hookSpawner.rotation);
                currentHook.GetComponent<RopeScript>().destiny = destiny;
                Invoke("DestroyCurrentHook", hookLife);
                //player.onWhip = true;
            }
            else
            {
                Destroy(currentHook.gameObject);
                //player.onWhip = false;
            }

        }
        else if (InputManager.ActiveDevice.Action4.WasPressed && canWrap)
        {
            if (currentHook == null)
            {
                currentHook = (GameObject)Instantiate(hook, hookSpawner.position, hookSpawner.rotation);
                currentHook.GetComponent<RopeScript>().destiny = destiny;

                Invoke("DestroyCurrentHook" ,  hookLife);
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
        //Destroy(currentHook.gameObject);
    }

    void DestroyCurrentHook()
    {
        Destroy(currentHook.gameObject);
    }
}
