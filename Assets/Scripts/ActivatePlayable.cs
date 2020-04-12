using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class ActivatePlayable : MonoBehaviour
{
    [SerializeField] PlayableDirector director;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateObject()
    {
        director.enabled = true;
    }
}
