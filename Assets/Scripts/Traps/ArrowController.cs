using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float maxDistance = 0;
    [SerializeField] float speed = 0;
    Vector3 initialPos;
    void Start()
    {
        initialPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed;
        if (Vector3.Distance(transform.position, initialPos) > maxDistance) Destroy(this.transform.gameObject);
    }
}
