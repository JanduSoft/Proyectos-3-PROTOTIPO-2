using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepAudio : MonoBehaviour
{
    [SerializeField] AudioSource steps;
    [SerializeField] GameObject dustParticle;
    [SerializeField] Transform leftFoot;
    [SerializeField] Transform rightFoot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FootSteps(int i)
    {
        switch(i)
        {
            case 0:
                Debug.Log("Being called from walking left foot");
                break;
            case 1:
                Debug.Log("Being called from walking right foot");
                break;
            case 2:
                Debug.Log("Being called from Jogging left foot");
                break;
            case 3:
                Debug.Log("Being called from Jogging right foot");
                break;
            case 4:
                Debug.Log("Being called from Running left foot");
                break;
            case 5:
                Debug.Log("Being called from Running right foot");
                break;
            
        }
        //if (i == 0)
        //    Destroy(Instantiate(dustParticle, leftFoot.position, Quaternion.identity), 0.55f);
        //else
        //    Destroy(Instantiate(dustParticle, rightFoot.position, Quaternion.identity), 0.55f);
        steps.Play();
    }
}
