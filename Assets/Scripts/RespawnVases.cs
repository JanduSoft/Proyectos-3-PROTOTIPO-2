using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnVases : MonoBehaviour
{

    [Header("VASES")]
    [SerializeField] List<GameObject> OriginalVases;

    //VARIABLES
    List<GameObject> Breakables = new List<GameObject>();

    bool reseting = false;
    void Start()
    {

        foreach (var item in OriginalVases)
        {
            Breakables.Add(item.transform.GetChild(0).gameObject);
        }
    }

    IEnumerator resetVases()
    {
        reseting = true;
        yield return new WaitForSeconds(1);
        for (int i = 0; i < Breakables.Count; i++)
        {
            Breakables[i].GetComponent<PickUpDropandThrow>().ResetObject();
            Breakables[i].transform.SetParent(OriginalVases[i].transform);
            Breakables[i].GetComponent<PickUpDropandThrow>().enabled = false;
            Breakables[i].GetComponent<PickUpDropandThrow>().enabled = true;
            Breakables[i].SetActive(true);
        }

        reseting = false;
        yield return null;

    }
    // Update is called once per frame
    void Update()
    {
        foreach (var item in Breakables)
        {
            if (!item.GetComponent<PickUpDropandThrow>().isBroken)
                return;
        }

        StartCoroutine(resetVases());
    }
}
