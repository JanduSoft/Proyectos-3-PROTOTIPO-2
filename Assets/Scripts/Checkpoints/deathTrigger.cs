using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public enum deathType { WaterSplash, Spikes, Fall}
    public deathType DeathType;
    void Start()
    {
        transform.tag = "Death";
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
                    GameObject.Find("Player flesh").GetComponent<AudioSource>().Play();
                    break;
                case deathType.Fall:
                    GameObject.Find("Fall").GetComponent<AudioSource>().Play();
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
