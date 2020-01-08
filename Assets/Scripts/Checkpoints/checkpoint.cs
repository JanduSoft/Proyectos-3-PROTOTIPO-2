using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
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
        if (other.CompareTag("Player"))
        {
            GameObject player = GameObject.Find("Character");
            PlayerPrefs.SetFloat("checkpointPosX", player.transform.position.x);
            PlayerPrefs.SetFloat("checkpointPosY", player.transform.position.y);
            PlayerPrefs.SetFloat("checkpointPosZ", player.transform.position.z);

            Debug.Log("Progress saved");

            //Deactivate trigger
            GetComponent<BoxCollider>().enabled = false;

        }

    }
}
