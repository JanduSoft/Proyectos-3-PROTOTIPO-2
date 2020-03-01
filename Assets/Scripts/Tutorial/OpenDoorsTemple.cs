using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpenDoorsTemple : MonoBehaviour
{
    [SerializeField] GameObject DoorLeft;
    [SerializeField] GameObject DoorRight;
    [SerializeField] Vector3 rotDoorLeft;
    [SerializeField] Vector3 rotDoorRight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            DoorLeft.transform.DORotateQuaternion(new Quaternion(rotDoorLeft.x, rotDoorLeft.y, rotDoorLeft.z, DoorLeft.transform.rotation.w),2);
            DoorRight.transform.DORotateQuaternion(new Quaternion(rotDoorRight.x, rotDoorRight.y, rotDoorRight.z, DoorRight.transform.rotation.w), 2);
        }
    }
}
