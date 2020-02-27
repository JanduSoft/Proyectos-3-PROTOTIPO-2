using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerPlayAnim : MonoBehaviour
{
    public string animationClipName;

    Animation rockAnim;
    [SerializeField]bool deactivateRock = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stone") && !other.isTrigger)
        {
            rockAnim = transform.parent.GetChild(0).GetComponent<Animation>();
            rockAnim.Play(animationClipName);
            GameObject.Find("Character").GetComponent<PlayerMovement>().StopMovement(true);
            transform.parent.GetChild(0).GetComponentInParent<PickUpDragandDrop>().dragSound.Stop();
            transform.parent.GetChild(0).GetComponentInParent<PickUpDragandDrop>().LetGoRock();
            StartCoroutine(startMovingAgain(rockAnim.GetClip(animationClipName).length));
        }
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
