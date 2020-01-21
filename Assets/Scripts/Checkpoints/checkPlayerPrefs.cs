using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPlayerPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("checkpointPosX") && PlayerPrefs.HasKey("checkpointPosY") && PlayerPrefs.HasKey("checkpointPosZ"))
        {
            Vector3 newPos = new Vector3(PlayerPrefs.GetFloat("checkpointPosX"), PlayerPrefs.GetFloat("checkpointPosY"), PlayerPrefs.GetFloat("checkpointPosZ"));
            GameObject.Find("Character").transform.position = newPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
