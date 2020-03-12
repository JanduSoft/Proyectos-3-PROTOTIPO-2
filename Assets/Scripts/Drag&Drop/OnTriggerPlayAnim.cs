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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stone") && !other.isTrigger)
        {
            StartCoroutine(SoundTime());
            rockAnim = transform.parent.GetChild(0).GetComponent<Animation>();
            rockAnim.Play(animationClipName);
            GameObject.Find("Character").GetComponent<PlayerMovement>().StopMovement(true);
            transform.parent.GetChild(0).GetComponentInParent<PickUpDragandDrop>().dragSound.Stop();
            transform.parent.GetChild(0).GetComponentInParent<PickUpDragandDrop>().LetGoRock();
            StartCoroutine(startMovingAgain(rockAnim.GetClip(animationClipName).length));
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
        if (deactivateRock)
        {
            transform.parent.GetChild(0).GetComponent<PickUpDragandDrop>().enabled = false;
        }

        transform.parent.GetChild(0).GetComponentInParent<PickUpDragandDrop>().dragSound.Stop();
        transform.parent.GetChild(0).GetComponentInParent<PickUpDragandDrop>().LetGoRock();
        Rigidbody rb = transform.parent.GetChild(0).GetComponentInParent<PickUpDragandDrop>().rb;
        rb.AddForce(Physics.gravity * (rb.mass * rb.mass));
        yield return new WaitForSeconds(_s);
        GameObject.Find("Character").GetComponent<PlayerMovement>().StopMovement(false);
    }

}

