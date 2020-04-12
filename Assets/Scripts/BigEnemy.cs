using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class BigEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] NavMeshAgent thisAiController;
    [SerializeField] Animator bigEnemyAnimator;
    [SerializeField] GameObject player;
    bool attack = false;
    void Start()
    {
        StartCoroutine(Persecution());
    }

    // Update is called once per frame
    void Update()
    {
        if(attack)   
             thisAiController.SetDestination(player.transform.position);
        if(Vector3.Distance(transform.position, player.transform.position) < 5)
        {
            thisAiController.isStopped = true;
            bigEnemyAnimator.SetBool("Attack", true);
            StartCoroutine(ReturntoWalking());
        }
    }

    IEnumerator Persecution()
    {
        yield return new WaitForSeconds(6f);
        attack = true;
    }
    IEnumerator ReturntoWalking()
    {
        yield return new WaitForSeconds(3.15f);
        thisAiController.isStopped = false;
        bigEnemyAnimator.SetBool("Attack", false);
    }
}
