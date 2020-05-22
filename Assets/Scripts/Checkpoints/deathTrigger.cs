using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public enum deathType { WaterSplash, Spikes, Fall}
    public deathType DeathType;

    GameObject playerSoundsObject;
    AudioSource[] sounds;
    void Start()
    {
        transform.tag = "Death";
        playerSoundsObject = GameObject.Find("sfx_Death");
        sounds = playerSoundsObject.GetComponents<AudioSource>();
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
                    //GameObject.Find("Water splash").GetComponent<AudioSource>().Play();
                    sounds[2].Play();

                    break;
                case deathType.Spikes:
                    //GameObject.Find("Player flesh").GetComponent<AudioSource>().Play();
                    sounds[0].Play();
                    break;
                case deathType.Fall:
                    //GameObject.Find("Fall").GetComponent<AudioSource>().Play();
                    sounds[1].Play();
                    break;
            }


            other.GetComponent<playerDeath>().killPlayer(0.1f);

        }

        //try
        //{
        //    if (other.CompareTag("Torch"))
        //    {
        //        other.gameObject.GetComponent<PickUp>().ResetPosition();
        //        Debug.Log("Torch position reset!");
        //    }

        //    if (other.CompareTag("Place"))
        //    {
        //        other.gameObject.transform.parent.GetComponent<PickUp>().ResetPosition();
        //        Debug.Log("Place position reset!");

        //    }

        //}
        //catch
        //{
        //    Debug.Log("Object " + other.gameObject.name +" couldn't be respawned");
        //}

    }
}
