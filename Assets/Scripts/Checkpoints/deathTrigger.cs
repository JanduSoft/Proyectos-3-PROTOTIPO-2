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
                    try
                    {
                        GameObject.Find("Water splash").GetComponent<AudioSource>().Play();
                    }
                    catch 
                    { 
                        Debug.Log("Couldn't play WATER SPLASH sound effect. Make sure to add Global Sounds prefab to scene. Make sure Water splash exists."); 
                    }
                    
                    break;
                case deathType.Spikes:
                    try
                    {
                        GameObject.Find("Player flesh").GetComponent<AudioSource>().Play();
                    }
                    catch 
                    { 
                        Debug.Log("Couldn't play PLAYER FLESH sound effect. Make sure to add Global Sounds prefab to scene. Make sure Player flesh exists."); 
                    }
                    break;
            }


            other.GetComponent<playerDeath>().killPlayer(0.5f);

        }
    }
}
