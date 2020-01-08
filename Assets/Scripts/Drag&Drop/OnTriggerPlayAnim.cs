using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerPlayAnim : MonoBehaviour
{
    public string animationClipName;

    Animation rockAnim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stone") && !other.isTrigger)
        {
            rockAnim = transform.parent.GetChild(0).GetComponent<Animation>();
            rockAnim.Play(animationClipName);
            Debug.Log("yoyo");
            GameObject.Find("Character").GetComponent<PlayerMovement>().StopMovement(true);
            StartCoroutine(startMovingAgain(rockAnim.GetClip(animationClipName).length));
        }
    }

    IEnumerator startMovingAgain(float _s)
    {
        yield return new WaitForSeconds(_s);
        Debug.Log("yasta");
        GameObject.Find("Character").GetComponent<PlayerMovement>().StopMovement(false);
    }
}
