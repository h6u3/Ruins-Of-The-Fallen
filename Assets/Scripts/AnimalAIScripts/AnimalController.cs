using System.Collections;
using System.Collections.Generic;
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
    private GameObject animalObject;


    void Start() {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }


    void Update() {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius) {
            Vector3 fleeDistance = -target.position*2;
            agent.SetDestination(fleeDistance);

            checkAnimalHealth();
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

    private  void checkAnimalHealth() {
        if (AnimalHealth <= 0) {
            Die();
        }
    }

    private void Die() {
        Destroy(animalObject);
        //Play death animation, drop loot
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
}
