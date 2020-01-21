using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.AI;

public class backwardsTimeline : MonoBehaviour
{
    [SerializeField] PlayableDirector director;
    [SerializeField] Animator walkinAnimator;
    [SerializeField] PatrolEnemy agentControler;
    [SerializeField] NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        //walkinAnimator.enabled = false;
        agentControler.enabled = false;
        agent.enabled = false;
        director.enabled = true;
        director.Stop();
        director.time = director.playableAsset.duration - 0.01;
        director.Evaluate();
    }

    // Update is called once per frame
    void Update()
    {
        double t = director.time - Time.deltaTime;
        if (t < 0)
            t = 0;

        director.time = t;
        director.Evaluate();

        if (t == 0)
        {
            director.Stop();
            walkinAnimator.enabled = true;
            agentControler.enabled = true;
            agent.enabled = true;
            director.enabled = false;
            enabled = false;
        }
    }
}
