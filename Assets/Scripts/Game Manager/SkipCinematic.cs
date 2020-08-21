using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class SkipCinematic : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject STL1Object;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GeneralInputScript.Input_GetKeyDown("Jump"))
        {
            STL1Object.SetActive(true);
        }
    }
}
