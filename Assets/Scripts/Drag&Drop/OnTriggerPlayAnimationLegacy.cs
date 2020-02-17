using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerPlayAnimationLegacy : MonoBehaviour
{
    public string animationClipName;

    Animation rockAnim;
    [SerializeField] bool deactivateRock = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stone") && !other.isTrigger)
        {
            //rockAnim = transform.parent.GetChild(0).GetComponent<Animation>();
            //rockAnim.Play(animationClipName);
            GameObject.Find("Character").GetComponent<PlayerMovement>().StopMovement(true);
            //StartCoroutine(startMovingAgain(rockAnim.GetClip(animationClipName).length));
            transform.parent.GetChild(0).GetComponentInParent<DragAndDropObject>().LetGoRock();
            StartCoroutine(startMovingAgain(0.5f));
        }
    }

    IEnumerator startMovingAgain(float _s)
    {
        if (deactivateRock)
        {
            transform.parent.GetChild(0).GetComponent<DragAndDropObject>().enabled = false;
        }

        transform.parent.GetChild(0).GetComponentInParent<DragAndDropObject>().dragSound.Stop();
        transform.parent.GetChild(0).GetComponentInParent<DragAndDropObject>().LetGoRock();
        Rigidbody rb = transform.parent.GetChild(0).GetComponentInParent<DragAndDropObject>().rb;
        rb.AddForce(Physics.gravity * (rb.mass * rb.mass));
        yield return new WaitForSeconds(_s);
        GameObject.Find("Character").GetComponent<PlayerMovement>().StopMovement(false);
    }
}