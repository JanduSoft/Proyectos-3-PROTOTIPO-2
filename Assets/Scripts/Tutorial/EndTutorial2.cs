using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTutorial2 : MonoBehaviour
{
    // Start is called before the first frame update
    bool gem1Placed = false;
    bool gem2Placed = false;
    [SerializeField] GameObject dorr;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gem1Placed && gem2Placed) dorr.SetActive(false);
    }
    public void Solved(int index)
    {
        if (index == 1) gem1Placed = true;
        if (index == 2) gem2Placed = true;
    }
}
