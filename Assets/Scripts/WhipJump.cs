using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipJump : MonoBehaviour
{
    [SerializeField] Transform destinationForJump;
    [SerializeField] GameObject player;
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
        player.SendMessage("setDestinationTransform", destinationForJump);
        player.SendMessage("setWhipableJumpObjectTransform", this.transform);
    }
}
