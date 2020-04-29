using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PushToAnimation : MonoBehaviour
{
    [SerializeField] AudioSource hittingSound;
    [Header("FOR CAMERA SHAKE")]
    [SerializeField] Camera myCamera;
    [SerializeField] float durationShake;
    [SerializeField] float strength;
    [SerializeField] int vibrato;
    [SerializeField] float randomness;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ToPush"))
        {

            Animator anim = other.GetComponent<Animator>();
            hittingSound.Play();
            float time = 0;
            //anim.SetBool("Active", true);
            if (other.name=="Pilar1")
            {
                anim.Play("Pilar1Animation");
                time =2.09f;
            }
            else if (other.name=="Pilar2")
            {
                anim.Play("Pilar2Animation");
                time =2.09f;

            }
            else if (other.name == "Statues")
            {
                anim.Play("StatuesAnimation");
                time =2.0f;
            }

            Invoke("StartShake", time);

        }
    }

    void StartShake()
    {
        myCamera.DOShakePosition(durationShake, strength, vibrato, randomness, true);
    }
}
