using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private float lookRadius = 15f;
    private float attackCooldown = 3.0f;
    [SerializeField]private float EnemyHealth;
    private float MaxEnemyHealth;
    private float Attack;
    private string threatLevel;
    private float enemyID;
    private GameObject enemyObject;
    private bool canAttack = true;
    Transform target;
    NavMeshAgent agent;
    [SerializeField]private EnemySpawner spawner;
    private WanderAI_NavMeshRefactor wanderAi;
    public Animation anim;
    private float targetDistance;
    private bool alive = true;
    private bool active;
    [SerializeField] private PlayerManager playerManager;

    void Start()
    {
        anim = GetComponent<Animation>();
        anim.Play("run");
        playerManager = FindObjectOfType<PlayerManager>();
        target = playerManager.player.transform;
        agent = GetComponent<NavMeshAgent>();
        wanderAi = GetComponent<WanderAI_NavMeshRefactor>();
    }
    
    void FixedUpdate()
    {
        active = spawner.getPlayerInsideArea();
        if (active)
        {
            targetDistance = Vector3.Distance(target.position, this.transform.position);

            if (targetDistance <= lookRadius)
            {
                if (alive)
                {
                    agent.SetDestination(target.position); 
                    if (targetDistance <= agent.stoppingDistance)
                    {
                        if (canAttack)
                        {
                            StartCoroutine(AttackTarget());
                            StartCoroutine(AttackCooldown());
                        }
                    }
                }
            }
        }
        else
        {
            Destroy(enemyObject);
            spawner.enemyDied();
        }

    }

    //Runs the takeDamage function in PlayerManager causing the player to take damage equal to attackValue
    IEnumerator AttackTarget() {
        anim.Play("attack1");
        yield return new WaitForSeconds(1);
        if (targetDistance <= agent.stoppingDistance)
        {
            PlayerManager.instance.takeDamage(Attack);
        }
        anim.Play("run");
    }

    // Prevents further attacks for the specified cooldown period (attackCooldown)
    IEnumerator AttackCooldown() {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public void takeDamage(float damage) {
        EnemyHealth -= damage;
    }

    public void setAttack (float attackValue) {
        Attack = attackValue;
    }

    public void setEnemyHealth (float EnemyHealthValue) {
        EnemyHealth = EnemyHealthValue;
        
    }

    public void setMaxEnemyHealth(float EnemyHealthValue) {
        MaxEnemyHealth = EnemyHealthValue;
    }

    public float getEnemyHealth() {
        return EnemyHealth;
    }

    public float getMaxEnemyHealth() {
        return MaxEnemyHealth;
    }

    public void setEnemyID(int ID) {
        enemyID = ID;
    }
    
    public string GetThreatLevel() {
        return threatLevel;
    }

    public void checkHealth()
    {
        if (EnemyHealth <= 0) {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        //Play death animation, drop loot\
        anim.Play("death1");
        alive = false;
        wanderAi.setAlive(false);
        yield return new WaitForSecondsRealtime(1);
        Destroy(enemyObject);
        spawner.enemyDied();
    }

    public void setGameObject(GameObject enemy) {
        enemyObject = enemy;
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

    internal void setSpawnerParent(EnemySpawner eSpawner)
    {
        spawner = eSpawner;
    }
}
