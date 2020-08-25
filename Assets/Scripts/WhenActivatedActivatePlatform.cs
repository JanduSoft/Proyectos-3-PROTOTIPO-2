using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WhenActivatedActivatePlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject targetActivate;
    bool gobletActivated = false;
    [SerializeField] GameObject openDoor;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<AddSkull>().isActivated && !gobletActivated)
        {
            gobletActivated = true;
            targetActivate.SendMessage("ActivateObject", false, SendMessageOptions.DontRequireReceiver);
            
            if (openDoor!=null)
            {
                openDoor.transform.DOMoveY(-4, 2);
            }
        }
    }
}
