using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whip : MonoBehaviour
{
    [SerializeField] LineRenderer whip;
    [SerializeField] Transform playerTransform;
    Vector3 newPlayerPos;
    Transform whipableJumpObjectTransform;
    Transform destinationTrasnform;
    [SerializeField] float lineDrawSpeed;
    float distToWhipable;
    [SerializeField] float distToDestination;
    float counter = 0;
    float time = 0;
    bool inputDown = false;
    bool ableToWhipJump = false;
     bool whippin = false;
    // Start is called before the first frame update
    void Start()
    {
        whip.SetPosition(0, playerTransform.position);
        whip.SetPosition(1, playerTransform.position);
        whip.startWidth = (0.2f);
        whip.endWidth = (0.2f);
    }

    // Update is called once per frame
    void Update()
    {

        #region WHIP UPDATE
        whip.SetPosition(0, playerTransform.position);
        whip.SetPosition(1, playerTransform.position);
        if (counter < distToWhipable && inputDown)
        {
            time += Time.deltaTime;
            counter += .1f / lineDrawSpeed;
            float x = Mathf.Lerp(0, distToWhipable, counter);
            Vector3 pA = playerTransform.position;
            Vector3 pB = whipableJumpObjectTransform.position;

            Vector3 pointBetweenAandB = x * Vector3.Normalize(pB - pA) + pA;
            whip.SetPosition(1, pointBetweenAandB);
        }
        #endregion

        #region PLAYER WHIPJUMP
        if (time >= lineDrawSpeed / 4)
        {
            whip.SetPosition(1, whipableJumpObjectTransform.position);
            float x = Mathf.Lerp(0, distToDestination, Time.deltaTime);
            Vector3 pA = playerTransform.position;
            Vector3 pB = destinationTrasnform.position;
            newPlayerPos = x * Vector3.Normalize(pB - pA) + pA;
            whippin = true;
            if (x == distToDestination)
            {
                whippin = false;
                time = 0;
            }
        }
        #endregion

        #region INPUT CONTROL
        if ( Input.GetKeyDown(KeyCode.C) && ableToWhipJump) inputDown = true;
        if (Input.GetKeyUp(KeyCode.C) && ableToWhipJump)
        {
            inputDown = false;
            whip.SetPosition(1, playerTransform.position);
            counter = 0;
            time = 0;
            distToWhipable = Vector3.Distance(playerTransform.position, destinationTrasnform.position);
        }
        #endregion

    }

    private void FixedUpdate()
    {
        if (whippin)
        {
            playerTransform.position = newPlayerPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "WhipJump")
        {
            other.SendMessage("SetPlayerTransform", playerTransform);
            ableToWhipJump = true;
            distToWhipable = Vector3.Distance(playerTransform.position, whipableJumpObjectTransform.position);
            distToDestination = Vector3.Distance(playerTransform.position, destinationTrasnform.position);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "WhipJump")
        {
            ableToWhipJump = false;
            whipableJumpObjectTransform = null;
        }
    }

    public void setDestinationTransform(Transform transform)
    {
        destinationTrasnform = transform;
    }

    public void setWhipableJumpObjectTransform(Transform transform)
    {
        whipableJumpObjectTransform = transform;
    }
}