using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGemaMagenta : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]bool lever1 = false;
    [SerializeField] bool lever2 = false;
    [SerializeField] bool lever3 = false;
    [SerializeField] bool isPlaced = false;
    [SerializeField] List<GameObject> deactivate;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lever1 && lever2 && lever3)
        {
            for (int i = 0; i < deactivate.Count; i++) deactivate[i].SetActive(false);
        }
    }
    public void Solved(string name)
    {
        if (name == "lever1") lever1 = true;
        if (name == "lever2") lever2 = true;
        if (name == "lever3") lever3 = true;
        Debug.Log("Solved");
    }
    public void Broken(int i)
    {
        if (name == "lever1") lever1 = false;
        if (name == "lever2") lever2 = false;
        if (name == "lever3") lever3 = false;
        Debug.Log("Broken");
    }

}
