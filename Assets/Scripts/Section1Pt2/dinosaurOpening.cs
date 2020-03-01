using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;

public class dinosaurOpening : MonoBehaviour
{
    [SerializeField] GameObject fire1;
    [SerializeField] GameObject fire2;
    [SerializeField] GameObject horn;
    [SerializeField] PlayableDirector destination;

    [SerializeField] CamerShake shaking;
    bool notMoved = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (fire1.activeInHierarchy && fire2.activeInHierarchy && notMoved)
        {
            destination.enabled = true;
            notMoved = false;
            shaking.StartShake(5);
        }
    }
}
