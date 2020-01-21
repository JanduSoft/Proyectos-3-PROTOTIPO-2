using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanWhip : MonoBehaviour
{
    public PlayerController player;
    public Transform whipPosition;
    public Animator whipAnimation;
    public int whipSide;
    private bool isOnMyWhip = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.onWhip == true && isOnMyWhip)
        {
            player.transform.position = whipPosition.position;
        }
        else
        {
            if (whipSide == 1)
            {
                whipAnimation.SetBool("Whip", false);
            }
            else
            {
                whipAnimation.SetBool("WhipR", false);
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            isOnMyWhip = false;
            player.onWhip = false;
            if (whipSide == 1)
            {
                whipAnimation.SetBool("Whip", false);
            }
            else
            {
                whipAnimation.SetBool("WhipR", false);
            }            
           //player.Jump();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (player.onWhip == true)
            {

            }
            else
            {
                isOnMyWhip = true;
                player.onWhip = true;

                if (whipSide == 1)
                {
                    whipAnimation.SetBool("Whip", true);
                }
                else
                {
                    whipAnimation.SetBool("WhipR", true);
                }
            }
           
        }
    }

   /* private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.onWhip = false;

            whipAnimation.SetBool("Whip", false);
        }
    }*/
}
