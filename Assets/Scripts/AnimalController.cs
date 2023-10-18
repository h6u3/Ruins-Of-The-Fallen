using System;
using System.Collections;
using System.Collections.Generic;
//using Gaia;
using UnityEngine;
using UnityEngine.AI;

public class AnimalController : MonoBehaviour {
    private float lookRadius = 10f;

    Transform target;
    NavMeshAgent agent;
    private float AnimalHealth;
    private float MaxAnimalHealth;
    private string threatLevel;
    private float animalID;
    private AnimalSpawner spawner;
    private GameObject animalObject;
    private bool active;


    void Start() {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }


    void Update() {
        active = spawner.getPlayerInsideArea();
        if (active)
        {
            checkHealth();
            if (agent.isOnNavMesh){
                float distance = Vector3.Distance(target.position, transform.position);

                if (distance <= lookRadius)
                {
                    Vector3 fleeDistance = -target.position * 2;
                    agent.SetDestination(fleeDistance);

                }
            }
        }
        else
        {
            Destroy(animalObject);
            spawner.animalDied();
        }
    }

    public void takeDamage(float damage) {
        AnimalHealth -= damage;
    }

    public void setAnimalHealth (float AnimalHealthValue) {
        AnimalHealth = AnimalHealthValue;
    }

    public void setMaxAnimalHealth (float AnimalHealthValue) {
        MaxAnimalHealth = AnimalHealthValue;
    }

    public float getAnimalHealth() {
        return AnimalHealth;
    }

    public float getMaxAnimalHealth() {
        return MaxAnimalHealth;
    }

    public void setanimalID(int ID) {
        animalID = ID;
    }

    public void checkHealth() {
        if (AnimalHealth <= 0) {
            Die();
        }
    }

    private void Die() {
        Destroy(animalObject);
        //Play death animation, drop loot
        spawner.animalDied();
    }

    public void setGameObject(GameObject animal) {
        animalObject = animal;
    }

     public string GetThreatLevel() {
        return threatLevel;
    }

    public void setThreatLevel(int threatLevelValue) {
        switch(threatLevelValue) {
            case 1:
                threatLevel = "Low";
                break;
            case 2:
                threatLevel = "Medium";
                break;
            case 3:
                threatLevel = "High";
                break;
        }
    }

    internal void setSpawnerParent(AnimalSpawner animalSpawner)
    {
        spawner = animalSpawner;
    }
}
