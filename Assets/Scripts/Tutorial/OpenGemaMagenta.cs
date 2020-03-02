using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpenGemaMagenta : MonoBehaviour
{
    // Start is called before the first frame update
    bool lever1 = false;
    bool lever2 = false;
    bool lever3 = false;



    [Header("FOR CAMERA SHAKE")]
    [SerializeField] Camera myCamera;
    [SerializeField] float durationShake;
    [SerializeField] float strength;
    [SerializeField] int vibrato;
    [SerializeField] float randomness;
    bool shakeActivated = false;


    [SerializeField] List<GameObject> deactivate;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lever1 && lever2 && lever3)
        {
            //////CAMERA SHAKE
            if (!shakeActivated)
            {
                shakeActivated = true;
                myCamera.DOShakePosition(durationShake, strength, vibrato, randomness, true);
            }
            for (int i = 0; i < deactivate.Count; i++) deactivate[i].transform.DOMoveY(deactivate[i].transform.position.y + 2,2);
        }
    }
    public void Solved(string name)
    {
        if (name == "lever1") lever1 = true;
        if (name == "lever2") lever2 = true;
        if (name == "lever3") lever3 = true;
        Debug.Log("Solved");
    }
    public void Broken(string name)
    {
        if (name == "lever1") lever1 = false;
        if (name == "lever2") lever2 = false;
        if (name == "lever3") lever3 = false;
        Debug.Log("Broken");
    }
}
