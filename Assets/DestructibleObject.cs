using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    [SerializeField] GameObject particlesDestruction;
    [SerializeField] float lifeTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            Invoke("DestroyObject", lifeTime);
        }
    }

    void DestroyObject()
    {
        Instantiate(particlesDestruction, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    
}
