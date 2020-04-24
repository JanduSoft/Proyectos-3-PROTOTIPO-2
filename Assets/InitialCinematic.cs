using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialCinematic : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] float timeCinematic = 15;
    void Start()
    {
        player.StopMovement(true);
        Invoke("DestroyCinematic", timeCinematic);
    }

    void DestroyCinematic()
    {
        player.StopMovement(false);
        Destroy(this.gameObject);
    }
}
