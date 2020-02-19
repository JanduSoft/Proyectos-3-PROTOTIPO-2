using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.AI;

public class PatrolEnemy : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] NavMeshAgent Agent;
    [SerializeField] float viewingAngle;
    [SerializeField] Animator animController;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float viewingDistance;
    [SerializeField] List<GameObject> pathPoints;
    [SerializeField] playerDeath kill;
    float angleBetweenEnemyandPlayer = 0;
    bool trueDeath = false;
    [SerializeField] int pathNumer = 1;
    // Start is called before the first frame update
    void Start()
    {
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
                Agent.SetDestination(pathPoints[pathNumer].transform.position);
                if (Vector3.Distance(transform.position, pathPoints[pathNumer].transform.position) < 3)
                {
                    if (pathNumer < pathPoints.Count - 1) pathNumer++;
                    else pathNumer = 0;
                }
            }

            if (Vector3.Distance(transform.position, Player.transform.position) < 2.5) kill.killPlayer(0f);
        }
    }

   public void Die()
   {
        trueDeath = true;
        animController.SetBool("dead", true);
        agent.enabled = false;
        StartCoroutine(returnToTheLiving(2));
   }

    IEnumerator returnToTheLiving(float _s)
    {
        yield return new WaitForSeconds(6);
        animController.SetBool("dead", false);
        yield return new WaitForSeconds(2);
        agent.enabled = true;
        trueDeath = false;
    }
}
