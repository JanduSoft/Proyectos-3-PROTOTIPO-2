using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class doBridgeCinematic : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] BridgeMovement bmscript;
    [SerializeField] GameObject cinematicObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bmscript!=null && bmscript.isActivated)
        {
            bmscript = null;
            StartCoroutine(DoCinematic());
            
        }
    }

    IEnumerator DoCinematic()
    {
        yield return new WaitForSeconds(0.5f);
        cinematicObject.SetActive(true);
        cinematicObject.GetComponent<PlayableDirector>().Play();
        yield return new WaitForSecondsRealtime((float)cinematicObject.GetComponent<PlayableDirector>().duration);
        cinematicObject.SetActive(false);

    }
}
