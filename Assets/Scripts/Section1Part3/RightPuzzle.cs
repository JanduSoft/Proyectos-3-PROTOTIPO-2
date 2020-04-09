using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RightPuzzle : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject goldenPedestal;
    [SerializeField] GameObject rightLight;
    [SerializeField] GameObject goldenSkull;
    bool hasCompletedPuzzle = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (goldenPedestal.GetComponent<AddObject>().isActivated && !hasCompletedPuzzle)
        {
            hasCompletedPuzzle = true;
            goldenSkull.GetComponent<PickUpDropandThrow>().enabled = false;
            rightLight.GetComponent<Light>().DOIntensity(5, 1);
        }
    }
}
