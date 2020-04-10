using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RespawnPointScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject checkpointCanvas;
    [SerializeField] Image checkpointImage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine(ImageShow());
            checkpointImage.DOKill();
            StartCoroutine(ImageShow());
            other.gameObject.GetComponent<playerDeath>().lastSpawnPointTouched = other.gameObject.transform.position;
        }
    }

    IEnumerator ImageShow()
    {
        checkpointCanvas.SetActive(true);

        checkpointImage.DOFade(1, 0.2f);
        yield return new WaitForSeconds(0.3f);
        checkpointImage.DOFade(0, 0.2f);
        yield return new WaitForSeconds(0.3f);
        checkpointImage.DOFade(1, 0.2f);
        yield return new WaitForSeconds(0.3f);
        checkpointImage.DOFade(0, 0.2f);

        checkpointCanvas.SetActive(false);
    }
}
