using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectFireScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject torch;
    [SerializeField] ParticleSystem torchFire;
    [SerializeField] GameObject correctFire;

    [SerializeField] List<GameObject> Fires;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Fires.Contains(torch.GetComponent<PickUpDropandIgnite>().currentFireObject))
        {
            if (torch.GetComponent<PickUpDropandIgnite>().currentFireObject == correctFire)
            {
                GetComponent<SphereCollider>().enabled = true;
            }
            else
            {
                GetComponent<SphereCollider>().enabled = false;
            }

            ParticleSystem current = torch.GetComponent<PickUpDropandIgnite>().currentFireObject.transform.GetChild(0).GetComponent<ParticleSystem>();

            var col = torchFire.colorOverLifetime;
            col.color = current.colorOverLifetime.color;
        }
    }
}
