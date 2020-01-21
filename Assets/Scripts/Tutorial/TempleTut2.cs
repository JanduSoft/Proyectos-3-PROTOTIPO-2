using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleTut2 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject templeDoor;
    [SerializeField] Camera cam;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            templeDoor.SetActive(true);
            cam.clearFlags = CameraClearFlags.Skybox;
        }
    }
}
