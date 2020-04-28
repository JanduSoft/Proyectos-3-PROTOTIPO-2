using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class activateHorn : MonoBehaviour
{

    [SerializeField] PlayableDirector pd;
    [SerializeField] GameObject pudt;
    bool doOnce = false;

    private void Start()
    {
        pudt.GetComponent<PickUpDropandThrow>().enabled = false;
    }

    void Update()
    {
        if (pd.isActiveAndEnabled && !doOnce)
        {
            Debug.Log("hello");
            doOnce = true;
            pudt.GetComponent<PickUpDropandThrow>().enabled = true;
        }
    }
}
