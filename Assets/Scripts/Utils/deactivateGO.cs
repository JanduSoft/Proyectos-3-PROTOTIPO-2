using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deactivateGO : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject go;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") go.SetActive(false);
    }
}
