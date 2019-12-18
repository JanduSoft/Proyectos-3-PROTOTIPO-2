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
        }
    }
}
