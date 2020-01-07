using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipJump : MonoBehaviour
{
    [SerializeField] Transform destinationGoingForwardForJump;
    [SerializeField] Transform destinationGoingBackwardsForJump;
    [SerializeField] GameObject player;
    int goingForward = 1;
    Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerTransform(Transform transform)
    {
        playerTransform = transform;
        if(goingForward == 1) player.SendMessage("setDestinationTransform", destinationGoingForwardForJump);
        else player.SendMessage("setDestinationTransform", destinationGoingBackwardsForJump);
        player.SendMessage("setWhipableJumpObjectTransform", this.transform);
        goingForward = -goingForward;
    }

    public void ChangeState()
    {
        goingForward = -goingForward;
    }
}
