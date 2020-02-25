using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class cinematicS1P2 : MonoBehaviour
{
    [SerializeField] GameObject cinematicGO;
    [SerializeField] GameObject bigEnemy;
    [SerializeField] PlayerMovement playerM;
    [SerializeField] Camera mainCamera;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(sequence());
        }
    }

    IEnumerator sequence()
    {
        playerM.StopMovement(true);
        mainCamera.DOShakePosition(2,5);
        yield return new WaitForSeconds(1);
        bigEnemy.SetActive(true);
        yield return new WaitForSeconds(1f);
        cinematicGO.SetActive(true);
        yield return new WaitForSeconds(2f);
        mainCamera.DOShakePosition(2, 5);
        yield return new WaitForSeconds(1f);
        playerM.StopMovement(false);

    }
}
