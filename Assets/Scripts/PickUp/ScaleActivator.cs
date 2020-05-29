using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleActivator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.parent.GetComponent<Rigidbody>().useGravity = true;
        StartCoroutine(EnableScript());
    }

    IEnumerator EnableScript()
    {
        yield return new WaitForSeconds(0.5f);
        transform.parent.GetComponent<AudioSource>().Play();
        transform.parent.GetComponent<PickUpDropandThrow>().enabled = true;
    }
}
