using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepAudio : MonoBehaviour
{
    [Header("Audiosource")]
    [SerializeField]AudioSource audiosource;
    [Header("Steps")]
    [SerializeField] AudioClip[] grassSteps;
    [SerializeField] AudioClip[] concreteSteps;
    [SerializeField] AudioClip[] dirtSteps;
    [Header("Variables")]
    [SerializeField] float minVolume;
    [SerializeField] float maxVolume;
    [SerializeField] float minPitch;
    [SerializeField] float maxPitch;

    [Header("Particles")]
    [SerializeField] GameObject dustParticle;
    [SerializeField] GameObject leftFoot;
    [SerializeField] GameObject rightFoot;

    void Start()
    {
        if (audiosource == null && GetComponent<AudioSource>() != null)
        {
            //if there's no audiosource attached and there is an audiosource as a component, get it
            audiosource = GetComponent<AudioSource>();
        }
        else if (GetComponent<AudioSource>()==null)
        {
            Debug.LogError("Attach an AudioSource component to gameobject " + gameObject.name);
        }
    }

    void PlayFootStep()
    {
        Debug.Log("Current surface:" + PlayerMovement.currentSurface);
        switch (PlayerMovement.currentSurface)
        {
            case PlayerMovement.GroundType.DIRT:
                audiosource.clip = dirtSteps[Random.Range(0, dirtSteps.Length)];
                break;

            case PlayerMovement.GroundType.GRASS:
                audiosource.clip = grassSteps[Random.Range(0, grassSteps.Length)];
                break;

            case PlayerMovement.GroundType.CONCRETE:
                audiosource.clip = concreteSteps[Random.Range(0, concreteSteps.Length)];
                break;
        }
        audiosource.volume = Random.Range(minVolume, maxVolume);
        audiosource.pitch = Random.Range(minPitch, maxPitch);
        audiosource.Play();
    }

    void PlayDustLeft()
    {
        GameObject aux = Instantiate(dustParticle, leftFoot.transform.position, Quaternion.identity);
        aux.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Destroy(aux, 1);
    }

    void PlayDustRight()
    {
        GameObject aux = Instantiate(dustParticle, rightFoot.transform.position, Quaternion.identity);
        aux.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Destroy(aux, 1);
    }

}
