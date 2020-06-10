using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepAudio : MonoBehaviour
{
    //[Header("Audiosource")]
    //[SerializeField]AudioSource audiosource;
    //[Header("Steps")]
    //[SerializeField] AudioClip[] grassSteps;
    //[SerializeField] AudioClip[] concreteSteps;
    //[SerializeField] AudioClip[] dirtSteps;
    //[Header("Variables")]
    //[SerializeField] float minVolume;
    //[SerializeField] float maxVolume;
    //[SerializeField] float minPitch;
    //[SerializeField] float maxPitch;

    [Header("Particles")]
    [SerializeField] GameObject dustParticle;
    [SerializeField] GameObject leftFoot;
    [SerializeField] GameObject rightFoot;

    FMOD.Studio.EventInstance footstepsInstance;

    void Start()
    {
        //if (audiosource == null && GetComponent<AudioSource>() != null)
        //{
        //    //if there's no audiosource attached and there is an audiosource as a component, get it
        //    audiosource = GetComponent<AudioSource>();
        //}
        //else if (GetComponent<AudioSource>()==null)
        //{
        //    Debug.LogError("Attach an AudioSource component to gameobject " + gameObject.name);
        //}

        footstepsInstance = FMODUnity.RuntimeManager.CreateInstance("event:/char/footsteps");
    }

    void PlayFootStep()
    {
        switch (PlayerMovement.currentSurface)
        {
            case PlayerMovement.GroundType.DIRT:
                //audiosource.clip = dirtSteps[Random.Range(0, dirtSteps.Length)];
                footstepsInstance.setParameterByName("step_surface", 2);
                break;

            case PlayerMovement.GroundType.GRASS:
                //audiosource.clip = grassSteps[Random.Range(0, grassSteps.Length)];
                footstepsInstance.setParameterByName("step_surface", 0);

                break;

            case PlayerMovement.GroundType.CONCRETE:
                //audiosource.clip = concreteSteps[Random.Range(0, concreteSteps.Length)];
                footstepsInstance.setParameterByName("step_surface", 1);

                break;
        }
        //audiosource.volume = Random.Range(minVolume, maxVolume);
        //audiosource.pitch = Random.Range(minPitch, maxPitch);
        //audiosource.Play();
        footstepsInstance.start();
    }

    void PlayDustLeft()
    {
        if (dustParticle!=null)
        {
            GameObject aux = Instantiate(dustParticle, leftFoot.transform.position, Quaternion.identity);
            aux.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Destroy(aux, 1);
        }
    }

    void PlayDustRight()
    {
        if (dustParticle != null)
        {
            GameObject aux = Instantiate(dustParticle, rightFoot.transform.position, Quaternion.identity);
            aux.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Destroy(aux, 1);
        }
    }

}
