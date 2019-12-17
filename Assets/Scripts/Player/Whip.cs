using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Whip : MonoBehaviour
{
    [SerializeField] LineRenderer whip;
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject spriteIndicateObject;
    [SerializeField] GameObject playerGO;
    Vector3 newPlayerPos;
    Transform whipableObjectTransform;
    Transform destinationTrasnform;
    [SerializeField] float lineDrawSpeed;
    float distToWhipable;
    float distToDestination;
    float counter = 0;
    float curveCounter = 0;
    float time = 0;
    bool inputDown = false;
    [SerializeField] bool ableToWhipJump = false;
    bool ableToWhipObject = false;
     bool whippin = false;
    // Start is called before the first frame update
    void Start()
    {
        whip.SetPosition(0, playerTransform.position);
        whip.SetPosition(1, playerTransform.position);
        whip.startColor = Color.black;
        whip.endColor = Color.black;
        whip.startWidth = (0.2f);
        whip.endWidth = (0.2f);
    }

    // Update is called once per frame
    void Update()
    {

        #region WHIP UPDATE
        whip.SetPosition(0, playerTransform.position);
        whip.SetPosition(1, playerTransform.position);
        if (counter < distToWhipable && inputDown && (ableToWhipJump || ableToWhipObject))
        {
            time += Time.deltaTime;
            counter += .1f / lineDrawSpeed;
            float x = Mathf.Lerp(0, distToWhipable, counter);
            Vector3 pA = playerTransform.position;
            Vector3 pB = whipableObjectTransform.position;
            Vector3 pointBetweenAandB = x * Vector3.Normalize(pB - pA) + pA;
            whip.SetPosition(1, pointBetweenAandB);
        }
        #endregion

        #region PLAYER WHIPJUMP & WHIPOBJECT
        if (time >= lineDrawSpeed / 4 && ableToWhipJump )
        {
            whip.SetPosition(1, whipableObjectTransform.position);
            playerTransform.DOMove(destinationTrasnform.position, 1f);
            
            /*whip.SetPosition(1, whipableObjectTransform.position);
            float x = Mathf.Lerp(0, distToDestination, Time.deltaTime);
            Vector3 pA = playerTransform.position;
            Vector3 pB = destinationTrasnform.position;
            newPlayerPos = x * Vector3.Normalize(pB - pA) + pA;*/
            whippin = true;
        }
        else if(time >= lineDrawSpeed / 4 && ableToWhipObject)
        {
            whipableObjectTransform.SendMessage("ChangeState");
        }

        if(ableToWhipJump)
            if(Vector3.Distance(playerTransform.position, destinationTrasnform.position) < 1)
            {
                whippin = false;
            }
        
        #endregion

        #region INPUT CONTROL
        if (Input.GetKeyDown(KeyCode.C) && ableToWhipJump)
        {
            whipableObjectTransform.SendMessage("ChangeState");
            inputDown = true;
        }
        if (Input.GetKeyUp(KeyCode.C) )
        {
            whippin = false;
            inputDown = false;
            whip.SetPosition(1, playerTransform.position);
            counter = 0;
            time = 0;
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
            distToWhipable = Vector3.Distance(playerTransform.position, whipableObjectTransform.position);
            distToDestination = Vector3.Distance(playerTransform.position, destinationTrasnform.position);
            spriteIndicateObject.SetActive(true);
            spriteIndicateObject.transform.position = whipableObjectTransform.position;
        }
        else if (other.tag == "WhipObject")
        {
            other.SendMessage("SetPlayerTransform", playerTransform);
            ableToWhipObject = true;
            distToWhipable = Vector3.Distance(playerTransform.position, whipableObjectTransform.position);
            spriteIndicateObject.SetActive(true);
            spriteIndicateObject.transform.position = whipableObjectTransform.position;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "WhipJump")
        {
            ableToWhipJump = false;
            inputDown = false;
            whippin = false;
            spriteIndicateObject.SetActive(false);
            whipableObjectTransform = null;
        }
        else if (other.tag == "WhipObject")
        {
            ableToWhipObject = false;
            whipableObjectTransform = null;
            spriteIndicateObject.SetActive(false);
        }
    }

    public void setDestinationTransform(Transform transform)
    {
        destinationTrasnform = transform;
    }

    public void setWhipableJumpObjectTransform(Transform transform)
    {
        whipableObjectTransform = transform;
    }

    Vector3 calculateBezierCurve(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 newP = uu * p0;
        newP += 2 * u * t * p1;
        newP += tt * p2;
        return newP;

        //return p1 + Mathf.Sqrt(1 - t) * (p0 - p1) + Mathf.Sqrt(t) * (p2 - p1);
    }
}