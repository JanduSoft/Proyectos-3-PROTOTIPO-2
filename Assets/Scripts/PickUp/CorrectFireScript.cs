using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectFireScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject torch;
    [SerializeField] GameObject correctFire;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (torch.GetComponent<PickUpDropandIgnite>().currentFireObject == correctFire)
        {
            GetComponent<SphereCollider>().enabled = true;
        }
        else
        {
            GetComponent<SphereCollider>().enabled = false;
        }
    }
}
