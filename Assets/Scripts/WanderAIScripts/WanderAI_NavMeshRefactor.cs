using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderAI_NavMeshRefactor : MonoBehaviour {
    NavMeshAgent agent;

    private bool isWandering = false;
    public float wanderRadius = 10f;
    private float wanderTimer = 5f;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        if (!isWandering) {
            StartCoroutine(Wander());
        }
    }

    IEnumerator Wander() {
        isWandering = true;

        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, wanderRadius, -1);

        agent.SetDestination(navHit.position);

        yield return new WaitForSeconds(wanderTimer);

        isWandering = false;
    }
}
