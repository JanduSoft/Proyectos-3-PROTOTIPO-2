using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRandomFire : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioClip[] fireAudios;
    [SerializeField] float minVolume;
    [SerializeField] float maxVolume;
    void Start()
    {
        AudioSource auxAS = GetComponent<AudioSource>();
        auxAS.volume = Random.Range(minVolume, maxVolume);
        auxAS.clip = fireAudios[Random.Range(0,fireAudios.Length)];
        auxAS.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
