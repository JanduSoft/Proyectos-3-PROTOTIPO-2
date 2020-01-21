using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolEnemy : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] NavMeshAgent Agent;
    [SerializeField] float viewingAngle;
    [SerializeField] float viewingDistance;
    [SerializeField] List<GameObject> pathPoints;
    float angleBetweenEnemyandPlayer = 0;
    [SerializeField] int pathNumer = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        angleBetweenEnemyandPlayer = Vector3.Angle(transform.forward, new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z) - transform.position);
        if (angleBetweenEnemyandPlayer < viewingAngle && Vector3.Distance(transform.position , Player.transform.position) < viewingDistance)
        {
            Agent.SetDestination(Player.transform.position);
        }
        else
        {
            Agent.SetDestination(pathPoints[pathNumer].transform.position);
            if(Vector3.Distance(transform.position, pathPoints[pathNumer].transform.position) < 3)
            {
                if (pathNumer < pathPoints.Count - 1) pathNumer++;
                else pathNumer = 0;
            }
        }
    }

   
}
