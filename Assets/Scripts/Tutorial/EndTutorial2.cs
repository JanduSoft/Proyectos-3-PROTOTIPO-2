using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndTutorial2 : MonoBehaviour
{
    // Start is called before the first frame update
    bool gem1Placed = false;
    bool gem2Placed = false;
    [SerializeField] GameObject dorr;

    [Header("FOR CAMERA SHAKE")]
    [SerializeField] Camera myCamera;
    [SerializeField] float durationShake;
    [SerializeField] float strength;
    [SerializeField] int vibrato;
    [SerializeField] float randomness;
    bool shakeActivated = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gem1Placed && gem2Placed)
        {
            //////CAMERA SHAKE
            if (!shakeActivated)
            {
                shakeActivated = true;
                myCamera.DOShakePosition(durationShake, strength, vibrato, randomness, true);
            }

            dorr.transform.DOMove(new Vector3(-5.1f, -2, 49.06f),2);
        }
    }
    public void Solved(int index)
    {
        if (index == 1) gem1Placed = true;
        if (index == 2) gem2Placed = true;
        Debug.Log("Solved");
    }
}
