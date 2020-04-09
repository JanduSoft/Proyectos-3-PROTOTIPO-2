using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveAxes : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float SwingSpeed;
    [SerializeField] Vector3 maxRotation;
    [SerializeField] Vector3 minRotation;
    [SerializeField] bool isActive;
    [SerializeField] float startTimeDelay = 0f;
    void Start()
    {
        if (isActive)
        {
            StartCoroutine(StartDelay());
        }
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(startTimeDelay);
        RotateToPosition(minRotation);
    }

    void RotateToPosition(Vector3 rotation)
    {
        Vector3 nextRotation;
        if (rotation == minRotation) nextRotation = maxRotation;
        else
            nextRotation = minRotation;
        transform.DORotate(rotation, SwingSpeed).OnComplete(() => { RotateToPosition(nextRotation); });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<playerDeath>().killPlayer(0.1f);
        }
    }
}
