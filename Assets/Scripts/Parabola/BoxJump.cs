using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxJump : MonoBehaviour
{
    protected float animation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animation += Time.deltaTime;
        animation = animation % 5f;

        transform.position = MathParabola.Parabola(Vector3.zero, Vector3.forward * 10, 5f, animation / 5f);
    }
}
