using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanWhip : MonoBehaviour
{
    public PlayerController player;
    public Transform whipPosition;
    public Animator whipAnimation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.onWhip == true)
        {
            player.transform.position = whipPosition.position;
        }
        else
        {
            whipAnimation.SetBool("Whip", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            player.onWhip = false;
            whipAnimation.SetBool("Whip", false);
            player.Jump();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.onWhip = true;

            whipAnimation.SetBool("Whip" ,true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.onWhip = false;

            whipAnimation.SetBool("Whip", false);
        }
    }
}
