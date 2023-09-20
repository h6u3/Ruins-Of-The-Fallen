using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private float lookRadius = 5f;
    private float attackCooldown = 3.0f;
    private float EnemyHealth;
    private float MaxEnemyHealth;
    private float Attack;
    private string threatLevel;
    private float enemyID;
    private GameObject enemyObject;
    private bool canAttack = true;
    Transform target;
    NavMeshAgent agent;
    [SerializeField]private EnemySpawner spawner;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        spawner = FindObjectOfType<EnemySpawner>();
    }
    
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius ) {

            agent.SetDestination(target.position);
            

            if (distance <= agent.stoppingDistance) {
                if (canAttack) {
                AttackTarget();
                StartCoroutine(AttackCooldown());
                }

                checkEnemyHealth();
            }
        }
    }
    
    //Runs the takeDamage function in PlayerManager causing the player to take damage equal to attackValue
    public void AttackTarget() {
        PlayerManager.instance.takeDamage(Attack);
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

    private  void checkEnemyHealth() {
        if (EnemyHealth <= 0) {
            Die();
        }
    }

    private void Die() {
        Destroy(enemyObject);
        //Play death animation, drop loot\
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
}
