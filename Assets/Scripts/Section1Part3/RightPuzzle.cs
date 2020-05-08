using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Playables;

public class RightPuzzle : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject goldenPedestal;
    [SerializeField] GameObject rightLight;
    [SerializeField] GameObject goldenSkull;
    public bool hasCompletedPuzzle = false;
    [SerializeField] GameObject cinematicObject;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (goldenPedestal.GetComponent<AddObject>().isActivated && !hasCompletedPuzzle)
        {

            hasCompletedPuzzle = true;
            rightLight.GetComponent<Light>().DOIntensity(6,3f);
            StartCoroutine(DoCinematic());
            goldenSkull.GetComponent<PickUpDropandThrow>().enabled = false;
            goldenPedestal.GetComponent<AddObject>().enabled = false;

        }
    }

    IEnumerator DoCinematic()
    {
        yield return new WaitForSeconds(0.5f);
        cinematicObject.SetActive(true);
        cinematicObject.GetComponent<PlayableDirector>().Play();
        yield return new WaitForSeconds((float)cinematicObject.GetComponent<PlayableDirector>().duration);
        cinematicObject.SetActive(false);

    }
}
