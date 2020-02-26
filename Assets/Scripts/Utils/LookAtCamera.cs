using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] Transform cameraMain;

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(cameraMain.position);
    }
}
