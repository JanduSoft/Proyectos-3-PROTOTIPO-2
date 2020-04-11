using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSink : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] float goingDownSpeed = 2f;
    [SerializeField] Vector3 maxGoingDownPos;
    [SerializeField] float timeBeforeGoingUpAgain = 1f;
    GameObject player;

    bool startGoingDown = false;
    bool startGoingUp = false;
    Vector3 initialPosition;
    void Start()
    {
        //get the initial position to go back to it
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if the platform has been touched, it starts going down
        if (startGoingDown)
        {
            transform.Translate(Vector3.down * Time.deltaTime * goingDownSpeed);
        }
        //when the platform reaches its destination, it'll start going up again
        else if (startGoingUp)
        {
            transform.Translate(Vector3.up * Time.deltaTime * goingDownSpeed);
        }

        //check if the platform has reached the position
        if (transform.position==maxGoingDownPos || Vector3.Distance(transform.position,maxGoingDownPos)<0.5)
        {
            startGoingDown = false;
            StartCoroutine(WaitAndGoUp());  //used to wait some seconds before the platform comes back up
        }
        else if (transform.position == initialPosition || Vector3.Distance(transform.position, initialPosition) < 0.5)
        {
            startGoingUp = false;
        }

        if (player!=null &&  Vector3.Distance(transform.position,player.transform.position) > 3.0f)
        {
            player.transform.SetParent(null);
            player = null;
        }
    }
    
    IEnumerator WaitAndGoUp()
    {
        yield return new WaitForSeconds(timeBeforeGoingUpAgain);
        startGoingUp = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !startGoingDown)
        {
            //when you touch the platform, start going down
            other.gameObject.transform.SetParent(transform);
            player = other.gameObject;
            startGoingDown = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !startGoingDown)
        {
            //when you touch the platform, start going down
            other.gameObject.transform.SetParent(null);
        }
    }
}
