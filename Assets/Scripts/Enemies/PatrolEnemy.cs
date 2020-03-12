﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.AI;

public class PatrolEnemy : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Skull;
    [SerializeField] GameObject headPosition;
    [SerializeField] PatrolEnemy _this;
    [SerializeField] GameObject skullModel;
    [SerializeField] NavMeshAgent Agent;
    [SerializeField] float viewingAngle;
    [SerializeField] Animator animController;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float viewingDistance;
    [SerializeField] Whip whipScript;
    [SerializeField] List<GameObject> pathPoints;
    [SerializeField] playerDeath kill;
    float angleBetweenEnemyandPlayer = 0;
    bool trueDeath = false;
    [SerializeField] int index = 1;
    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        Skull.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!trueDeath)
        {
            angleBetweenEnemyandPlayer = Vector3.Angle(transform.forward, new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z) - transform.position);
            if (angleBetweenEnemyandPlayer < viewingAngle && Vector3.Distance(transform.position, Player.transform.position) < viewingDistance)
            {
                Agent.SetDestination(Player.transform.position);
            }
            else if (Vector3.Distance(transform.position, Player.transform.position) < viewingDistance / 2)
            {
                Agent.SetDestination(Player.transform.position);
            }
            else
            {
                Agent.SetDestination(pathPoints[index].transform.position);
                if (Vector3.Distance(transform.position, pathPoints[index].transform.position) < 3)
                {
                    if (index < pathPoints.Count - 1) index++;
                    else index = 0;
                }
            }

            if (Vector3.Distance(transform.position, Player.transform.position) < 2.5) kill.killPlayer(0f);
        }
        else if (trueDeath)
        {
            if (Skull.activeInHierarchy)
                if (Vector3.Distance(Skull.transform.position, Player.transform.position) < 2.5 && Input.GetButtonDown("Interact"))
                {
                    _this.StopAllCoroutines();
                    agent.enabled = false;
                    skullModel.SetActive(false);
                    _this.enabled = false;

                }
        }
    }

    public void Die()
    {
        trueDeath = true;
        animController.SetBool("dead", true);
        agent.SetDestination(agent.transform.position);
        StartCoroutine(returnToTheLiving(2));
    }

    public void ResetEnemy()
    {
        transform.position = startPosition;
        StartCoroutine(returnToTheLiving(0));

    }

    IEnumerator returnToTheLiving(float _s)
    {
        yield return new WaitForSeconds(2);
        Skull.SetActive(true);
        Skull.transform.position = headPosition.transform.position;
        yield return new WaitForSeconds(4);
        Skull.SetActive(false);
        animController.SetBool("dead", false);
        yield return new WaitForSeconds(2);
        agent.SetDestination(pathPoints[index].transform.position);
        trueDeath = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) whipScript.enabled = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) whipScript.enabled = false;
    }
}