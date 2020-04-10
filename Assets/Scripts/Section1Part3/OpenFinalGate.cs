using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpenFinalGate : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject BOSSGate;
    [SerializeField] GameObject cinematicCamera;
    [SerializeField] PedestalsPuzzle LeftPedestalPuzzle;
    [SerializeField] RightPuzzle RightPedestalPuzzle;
    bool doorClosed = true;
    
    [Space(15)]
    [Header("SOUND")]
    [SerializeField] AudioSource audios;
    [SerializeField] AudioClip doorOpenSound;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (doorClosed && LeftPedestalPuzzle.isPuzzleDone && RightPedestalPuzzle.hasCompletedPuzzle)
        {
            doorClosed = false;
            StartCoroutine(openGate());
        }
    }

    IEnumerator openGate()
    {
        BOSSGate.transform.DOMoveY(BOSSGate.transform.position.y + 5, 6f);
        yield return new WaitForSeconds(0.4f);
        audios.clip = doorOpenSound;
        audios.Play();
        yield return new WaitForSeconds(2.5f);
        audios.DOFade(0, 1f);
    }
}
