using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reverbChange : MonoBehaviour
{
    // Start is called before the first frame update

    FMOD.Studio.EventInstance rvbSnapshot;
    void Start()
    {
        rvbSnapshot = FMODUnity.RuntimeManager.CreateInstance("snapshot:/lvl1_openZone_snapshot");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enter");
            rvbSnapshot.start();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("exit");

            rvbSnapshot.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

    }
}
