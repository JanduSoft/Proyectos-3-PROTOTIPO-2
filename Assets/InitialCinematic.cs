using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class InitialCinematic : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] float timeCinematic = 15;
    void Start()
    {
        player.StopMovement(true);
        Invoke("DestroyCinematic", timeCinematic);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || InputManager.ActiveDevice.Action1.WasPressed)
        {
            DestroyCinematic();
        }
    }

    void DestroyCinematic()
    {
        player.StopMovement(false);
        Destroy(this.gameObject);
    }
}
