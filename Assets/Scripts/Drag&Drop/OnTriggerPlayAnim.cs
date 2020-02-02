using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerPlayAnim : MonoBehaviour
{
    public string animationClipName;

    Animation rockAnim;
    [SerializeField]bool deactivateRock = false;
    [SerializeField] float delayedSoundTime = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stone") && !other.isTrigger)
        {
            rockAnim = transform.parent.GetChild(0).GetComponent<Animation>();
            rockAnim.Play(animationClipName);
            GameObject.Find("Character").GetComponent<PlayerMovement>().StopMovement(true);
            StartCoroutine(startMovingAgain(rockAnim.GetClip(animationClipName).length));
            try
            {
                 GetComponent<AudioSource>().PlayDelayed(delayedSoundTime);
            }
            catch
            {
                Debug.Log("Can't play hitting ground sound. Please, add Audio Source component to Trigger Anim gameobject.");
            }
        }
    }

    IEnumerator startMovingAgain(float _s)
    {

        if (deactivateRock)
        {
            transform.parent.GetChild(0).GetComponent<DragAndDropObject>().enabled = false;
            transform.parent.GetChild(0).GetComponent<DragAndDropObject>().dragSound.Stop();
        }
        yield return new WaitForSeconds(_s);
        GameObject.Find("Character").GetComponent<PlayerMovement>().StopMovement(false);
    }
}
