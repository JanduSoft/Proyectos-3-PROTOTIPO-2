using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PedestalsPuzzle : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] pedestals = new GameObject[2];
    public GameObject[] objectsToPut = new GameObject[2];
    public GameObject[] lights = new GameObject[2];
    bool [] pedestalsActivated= new bool [2];
    public GameObject leftLight;
    //0 - red
    //1 - purple

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if the first pedestal is activated, turn light on
        //if it's not activated, turn light off
        if (pedestals[0].GetComponent<AddObject>().isActivated)
        {
            lights[0].GetComponent<Light>().DOIntensity(2, 1);
            pedestalsActivated[0] = true;
        }
        else if (!pedestals[0].GetComponent<AddObject>().isActivated)
        {
            lights[0].GetComponent<Light>().DOIntensity(0, 1);
            pedestalsActivated[0] = false;
        }

        //if the second pedestal is activated, turn light on
        //if it's not activated, turn light off
        if (pedestals[1].GetComponent<AddObject>().isActivated)
        {
            lights[1].GetComponent<Light>().DOIntensity(2, 1);
            pedestalsActivated[1] = true;
        }
        else if (!pedestals[1].GetComponent<AddObject>().isActivated)
        {
            lights[1].GetComponent<Light>().DOIntensity(0, 1);
            pedestalsActivated[1] = false;
        }

        //if both pedestals are activated, puzzle is done
        if (pedestalsActivated[0] && pedestalsActivated[1])
        {
            Debug.Log("PUZZLE DONE!");
            //deactivate grabbing the objects again
            objectsToPut[0].GetComponent<PickUpDropandThrow>().enabled = false;
            objectsToPut[1].GetComponent<PickUpDropandThrow>().enabled = false;
            leftLight.GetComponent<Light>().DOIntensity(5, 1);

        }
    }
}
