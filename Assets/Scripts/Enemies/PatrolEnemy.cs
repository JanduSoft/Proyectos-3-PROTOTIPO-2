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
    [SerializeField] GameObject Skull;
    [SerializeField] PatrolEnemy _this;
    [SerializeField] NavMeshAgent Agent;
    [SerializeField] float viewingAngle;
    [SerializeField] Animator animController;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float viewingDistance;
    [SerializeField] List<GameObject> pathPoints;
    [SerializeField] playerDeath kill;
    float angleBetweenEnemyandPlayer = 0;
    bool trueDeath = false;
    [SerializeField] int index = 1;
    // Start is called before the first frame update
    void Start()
    {
        Skull.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!trueDeath)
        {
            Skull.SetActive(false);
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
        else if(trueDeath)
        {
            Skull.SetActive(true);
            Skull.transform.position = gameObject.transform.position;
            Skull.transform.position += new Vector3(0, 0, 1);
            Debug.Log(this.name);
            if (Vector3.Distance(Skull.transform.position, Player.transform.position) < 2.5 && Input.GetButtonDown("Interact"))
            {
                agent.enabled = false;
                _this.StopAllCoroutines();
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

    IEnumerator returnToTheLiving(float _s)
    {
        yield return new WaitForSeconds(6);
        animController.SetBool("dead", false);
        yield return new WaitForSeconds(2);
        agent.SetDestination(pathPoints[index].transform.position);
        trueDeath = false;
    }
}
