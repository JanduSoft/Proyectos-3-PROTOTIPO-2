using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsMoving : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] wheels;
    [SerializeField] float movingSpeed;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].transform.Rotate(new Vector3(movingSpeed, 0, 0));
        }
    }
}
