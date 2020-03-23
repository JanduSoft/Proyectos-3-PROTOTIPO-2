using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Whip : MonoBehaviour
{
    [SerializeField] LineRenderer whip;
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject spriteIndicateObject;
    [SerializeField] Camera mainCamera;
    [SerializeField] float lineDrawSpeed;
    float timePlus = 0.15f;
    float timeMinus = 0.15f;
    Transform whipableObjectTransform;
    [SerializeField] List<Transform> enemyList = new List<Transform>();
    float distToWhipable;
    float counter = 0;
    float time = 0;
    float oldFOV = 0;
    [SerializeField] bool inputDown = false;
    [SerializeField] bool ableToAttack = false;
    [SerializeField] public bool attackMode = false;
    bool ableToWhipObject = false;
    bool resetWhip = false;
    bool whippin = false;
    [SerializeField] int index = 0;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        #region WHIP UPDATE
        
        if (enemyList.Count == 0)
        {
            index = 0;
            ableToAttack = false;
            attackMode = false;
        }
        else
        {
            ableToAttack = true;
        }

        if (attackMode)
        {
            whipableObjectTransform = enemyList[index];
            spriteIndicateObject.SetActive(true);
            spriteIndicateObject.transform.position = whipableObjectTransform.position + new Vector3(0, 5f, 0);
        }
        else
        {
            spriteIndicateObject.SetActive(false);
        }
        #endregion

        #region INPUT CONTROL
        if (Input.GetAxis("RightJoystickHorizontal") == 1f && attackMode)
        {
            if (timePlus == 0.15f)
            {
                if (index < enemyList.Count - 1) index++;
                else index = 0;
            }
            timePlus -= Time.deltaTime;
            if (timePlus < 0) timePlus = 0.15f;
        }
        else if (Input.GetAxis("RightJoystickHorizontal") == -1f && attackMode)
        {
            if (timeMinus == 0.15f)
            {
                if (index > 0) index--;
                else index = enemyList.Count - 1;
            }
            timeMinus -= Time.deltaTime;
            if (timeMinus < 0) timeMinus = 0.15f;
        }
        #endregion

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WhipEnemy")
        {
            for(int i = 0; i< enemyList.Count; i++)
            {
                if(enemyList[i] != other.transform)
                {
                    enemyList.Add(other.gameObject.transform);
                    i = enemyList.Count;
                }
            }
            if(enemyList.Count == 0)
                enemyList.Add(other.gameObject.transform);

            attackMode = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "WhipEnemy")
        {
            enemyList.Remove(other.transform);
            spriteIndicateObject.SetActive(false);
            whipableObjectTransform = null;
            attackMode = false;
        }
    }

    public void setWhipableJumpObjectTransform(Transform transform)
    {
        whipableObjectTransform = transform;
    }
    public Transform getEnemy()
    {
        return enemyList[0];
    }

}