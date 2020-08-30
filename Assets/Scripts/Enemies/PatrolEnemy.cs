using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.AI;
using InControl;

public class PatrolEnemy : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Skull;
    [SerializeField] GameObject headPosition;
    [SerializeField] PatrolEnemy _this;
    [SerializeField] GameObject skullModel;
    [SerializeField] NavMeshAgent Agent;
    [SerializeField] float viewingAngle;
    [SerializeField] bool spawnSkull = true;
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

            if (Vector3.Distance(transform.position, Player.transform.position) < 2.5)
            {
                kill.killPlayer(0f);
                ResetEnemy();
            }
        }
        else if (trueDeath)
        {
            if (Skull.activeInHierarchy)
                if (Vector3.Distance(Skull.transform.position, Player.transform.position) < 2.5 && InputManager.ActiveDevice.Action3.WasPressed)
                {
                    agent.enabled = false;
                    _this.enabled = false;
                }
        }
    }

    public void Die()
    {
        Logros.numberOfEnemiesKilled++;
        PlayerPrefs.SetInt("NumberOfEnemiesKilled", Logros.numberOfEnemiesKilled);
        if (Logros.numberOfEnemiesKilled==3)
        {
            Logros.CallAchievement(12);
        }

        trueDeath = true;
        animController.SetBool("dead", true);
        agent.SetDestination(agent.transform.position);
        transform.tag = "Untagged";
        whipScript.Died(this.gameObject);
        StartCoroutine(fallToTheGround(2));
    }

    public void ResetEnemy()
    {
        transform.position = startPosition;
        index = 1;
    }

    IEnumerator fallToTheGround(float _s)
    {
        yield return new WaitForSeconds(_s);
        if(spawnSkull)
        {
            Skull.SetActive(true);
            Skull.transform.position = headPosition.transform.position;
            headPosition.transform.position = new Vector3(headPosition.transform.position.x, headPosition.transform.position.y - 20, headPosition.transform.position.z);
        }

    }
}