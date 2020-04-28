using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class cryptPuzzle : MonoBehaviour
{
    [SerializeField] GameObject fire1;
    [SerializeField] GameObject fire2;
    [SerializeField] GameObject horn;
    [SerializeField] Camera cam;
    [SerializeField] Transform destination;
    bool notMoved = true;
    [SerializeField] CamerShake shaking;
    [SerializeField] Animator CoffinAnimator;
    [SerializeField] Light CoffinLight;
    [SerializeField] AudioSource ShakeSound;
    [SerializeField] AudioSource SolvePuzzleSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fire1.activeInHierarchy && fire2.activeInHierarchy && notMoved)
        {
            //horn.transform.DOMove(destination.position, 1);
            SolvePuzzleSound.Play();
            horn.SetActive(true);
            CoffinAnimator.enabled = true;
            ShakeSound.Play();
            notMoved = false;
            shaking.StartShake(1.5f);
            CoffinLight.enabled = true;
        }
    }
}
