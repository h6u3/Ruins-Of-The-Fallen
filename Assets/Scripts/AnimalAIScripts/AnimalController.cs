using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalController : MonoBehaviour {
    public float lookRadius = 10f;

    Transform target;
    NavMeshAgent agent;
    private bool isWalking;

    void Start() {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }


    void Update() {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius) {
            isWalking = true;
            Vector3 fleeDistance = -target.position*2;
            agent.SetDestination(fleeDistance);


        if (distance <= agent.stoppingDistance) {
            isWalking = false;
        }
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public bool IsWalking() {
        return isWalking;
    }
}
