using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonActivator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject skull;
  
    void Start()
    {
        transform.parent.GetComponent<Animator>().enabled = true;
        transform.parent.GetComponent<Animator>().Play("SkeletonFallingAnimation");
        StartCoroutine(enableScript());
    }

    IEnumerator enableScript()
    {
        yield return new WaitForSeconds(1.0f);
        transform.parent.GetComponent<Animator>().enabled = false;
        skull.transform.SetParent(null);
        skull.GetComponent<PickUpDropandThrow>().enabled = true;

    }
}
