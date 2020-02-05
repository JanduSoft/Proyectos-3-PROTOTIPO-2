using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public enum deathType { WaterSplash, Spikes}
    public deathType DeathType;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (DeathType)
            {
                case deathType.WaterSplash:
                    GameObject.Find("Water splash").GetComponent<AudioSource>().Play();
                    break;
                case deathType.Spikes:
                    break;
            }


            other.GetComponent<playerDeath>().killPlayer(0.1f);

        }
    }
}
