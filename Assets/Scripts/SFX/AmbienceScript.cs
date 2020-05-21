using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioClip[] randomSounds;
    [SerializeField] AudioClip AmbienceSound;
    [SerializeField] AudioClip AmbienceSound2;
    [SerializeField] float minTime=1;
    [SerializeField] float maxTime=5;
    [SerializeField] float minPitch=0.8f;
    [SerializeField] float maxPitch=1.2f;
    [SerializeField] float maxVolume=1f;
    [SerializeField] float minVolume = 0.5f;

    AudioSource[] aus;

    float StartTime = 0;
    float timeUntilNext = 1;

    void Start()
    {
        StartTime = Time.realtimeSinceStartup;
        aus = GetComponents<AudioSource>();
        if (aus.Length < 3) Debug.LogError("You need 2 audiosources in AmbienceScript");

        aus[2].loop = false;

        aus[0].loop = true;
        aus[0].clip = AmbienceSound;
        aus[0].volume = 1;
        aus[0].Play();

        aus[1].loop = true;
        aus[1].clip = AmbienceSound2;
        aus[1].volume = 0.6f;
        aus[1].Play();
    }

    void play_random_sound()
    {
        float pitch = Random.Range(minPitch, maxPitch);
        float volume = Random.Range(minVolume, maxVolume);
        int audioId = Random.Range(0, aus.Length);

        aus[2].clip = randomSounds[audioId];
        aus[2].pitch = pitch;
        aus[2].volume = volume;
        aus[2].Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.realtimeSinceStartup-StartTime>=timeUntilNext)
        {
            StartTime = Time.realtimeSinceStartup;
            play_random_sound();
            timeUntilNext = Random.Range(minTime, maxTime);
        }
    }
}
