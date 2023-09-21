using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderAI_NavMeshRefactor : MonoBehaviour {
    NavMeshAgent agent;
    public Animation animation;

    private bool isWandering = false;
    public float wanderRadius = 10f;
    private float wanderTimer = 5f;
    public float rDistance;
    public float allowDistance = 1.5f;
    private bool alive = true;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        animation = GetComponent<Animation>();
    }

    private void Update() {
        if (!isWandering && alive) {
            StartCoroutine(Wander());
        }
    }

    IEnumerator Wander() {
        isWandering = true;

        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, wanderRadius, -1);

        animation.Play("run");
        agent.SetDestination(navHit.position);

        if (agent.remainingDistance <= allowDistance)
        {
            animation.Play("idle");
        }

        yield return new WaitForSeconds(wanderTimer);

        isWandering = false;

    }

    public void setAlive(bool live)
    {
        alive = live;
    }
}
