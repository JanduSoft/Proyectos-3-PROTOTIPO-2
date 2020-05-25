using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OnTriggerPlayAnim : MonoBehaviour
{
    public string animationClipName;
    Animation rockAnim;
    [SerializeField] bool deactivateRock = false;
    [SerializeField] float timeToSound = 1.8f;
    [SerializeField] float startMoving = 1.5f;

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Stone")|| other.CompareTag("Block")) && !other.isTrigger)
        {
            transform.parent.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
            GameObject.Find("Character").GetComponent<PlayerMovement>().StopMovement(true);
            transform.parent.GetChild(0).GetComponentInParent<PickUpDragandDrop>().LetGoRock();
            StartCoroutine(SoundTime());
            rockAnim = transform.parent.GetChild(0).GetComponent<Animation>();
            rockAnim.Play(animationClipName);
            transform.parent.GetChild(0).GetComponentInParent<PickUpDragandDrop>().dragSound.Stop();
            StartCoroutine(startMovingAgain(startMoving));
        }
    }




    IEnumerator SoundTime()
    {
        yield return new WaitForSeconds(timeToSound);
        Camera.main.DOShakePosition(0.5f, 1, 1, 90, true);
        GetComponent<AudioSource>().Play();
    }

    IEnumerator startMovingAgain(float _s)
    {
        transform.parent.GetChild(0).GetComponent<PickUpDragandDrop>().enabled = false;
        transform.parent.GetChild(0).GetComponentInParent<PickUpDragandDrop>().dragSound.Stop();
        transform.parent.GetChild(0).GetComponentInParent<PickUpDragandDrop>().LetGoRock();
        Rigidbody rb = transform.parent.GetChild(0).GetComponentInParent<PickUpDragandDrop>().rb;
        rb.AddForce(Physics.gravity * (rb.mass * rb.mass));
        yield return new WaitForSeconds(_s);
        if (!deactivateRock)
            transform.parent.GetChild(0).GetComponent<PickUpDragandDrop>().enabled = true;

        transform.parent.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;

        GameObject.Find("Character").GetComponent<PlayerMovement>().StopMovement(false);
    }

}

