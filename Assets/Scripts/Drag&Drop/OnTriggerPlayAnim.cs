using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OnTriggerPlayAnim : MonoBehaviour
{
    public string animationClipName;
    [SerializeField] bool deactivateRock = false;

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Stone")|| other.CompareTag("Block")) && !other.isTrigger)
        {
            StartCoroutine(StopCharacterMovement());
            transform.parent.GetChild(0).GetComponent<PickUpDragandDrop>().touchedTrigger = gameObject;
            transform.parent.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
            transform.parent.GetChild(0).GetComponent<PickUpDragandDrop>().enabled = false;
            transform.parent.GetChild(0).GetComponentInParent<PickUpDragandDrop>().dragSound.Stop();
            transform.parent.GetChild(0).GetComponentInParent<PickUpDragandDrop>().LetGoRock();
            Rigidbody rb = transform.parent.GetChild(0).GetComponentInParent<PickUpDragandDrop>().rb;
            rb.AddForce(Physics.gravity * (rb.mass * rb.mass));
        }
    }

    IEnumerator StopCharacterMovement()
    {
        GameObject.Find("Character").GetComponent<PlayerMovement>().StopMovement(true);
        yield return new WaitForSeconds(0.5f);
        GameObject.Find("Character").GetComponent<PlayerMovement>().StopMovement(false);

    }



    public void playSound()
    {
        Camera.main.DOShakePosition(0.5f, 1, 1, 90, true);
        GetComponent<AudioSource>().Play();
        if (!deactivateRock)
            transform.parent.GetChild(0).GetComponent<PickUpDragandDrop>().enabled = true;
    }

}

