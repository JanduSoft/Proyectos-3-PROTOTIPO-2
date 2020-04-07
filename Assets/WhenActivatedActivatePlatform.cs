using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhenActivatedActivatePlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject targetActivate;
    bool gobletActivated = false;
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
        }
    }
}
