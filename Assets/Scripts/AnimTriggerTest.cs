using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTriggerTest : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource hitSound;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void testThing()
    {
        hitSound.Play();
    }
}
