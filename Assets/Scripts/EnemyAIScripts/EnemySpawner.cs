using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    enum ThreatLevel {
        Low = 1,
        Medium = 2,
        High = 3
    }
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    private float Health;
    private float Attack;
    private ThreatLevel threatLevel;
    private int CoolDown;
    private int eId;
    private int concurrentEnemies;
    private bool playerInsideArea;

    private void Start() {
        CoolDown = 0;
        eId = 0;
        concurrentEnemies = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        //If the collider detectes a player, set true
        if (other.CompareTag("Player"))
        {
            int temp = eId + 10;
            for (int i = eId; i < temp; i++)
            {
                SpawnEnemy(i);
                eId++;
                concurrentEnemies++;
            }
            playerInsideArea = true;
            Debug.Log("Player entered the enemy area."); //Logs to check functionality
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //If the collider no longer detectes a player, set false
        if (other.CompareTag("Player"))
        {
            playerInsideArea = false;
            Debug.Log("Player exited the enemy area."); //Logs to check functionality
        }
    }


    private void FixedUpdate()
    {
        if (playerInsideArea)
        {
            CoolDown += 1;
            CoolDown %= 20;
            if (CoolDown == 0)
            {
                int ranNum = UnityEngine.Random.Range(1, 5);
                if (ranNum == 1 && concurrentEnemies < 10)
                {
                    SpawnEnemy(eId);
                    eId++;
                    concurrentEnemies++;
                }
            }
        }
    }

    public bool getPlayerInsideArea()
    {
        return playerInsideArea;
    }

    private void SpawnEnemy(int enemyID) {
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        newEnemy.name = "Enemy";
        
        EnemyController enemyController = newEnemy.GetComponent<EnemyController>();

        threatLevel = GetThreatLevel();
        setStats(threatLevel);

        if (enemyController != null) {
            enemyController.setEnemyHealth(Health);
            enemyController.setMaxEnemyHealth(Health);
            enemyController.setAttack(Attack);
            enemyController.setEnemyID(enemyID);
            enemyController.setGameObject(newEnemy);
            enemyController.setThreatLevel((int)threatLevel);
        }
    }

    private ThreatLevel GetThreatLevel() {
        int ranNum = UnityEngine.Random.Range(1, 4);

        ThreatLevel threatLevel = (ThreatLevel)ranNum;
        return threatLevel;
    }

    private void setStats(ThreatLevel threatLevel) {
        
        switch (threatLevel) {
            case ThreatLevel.Low:
                Attack = 2;
                Health = 25;
                break;

            case ThreatLevel.Medium:
                Attack = 3;
                Health = 40;
                break;

            case ThreatLevel.High:
                Attack = 5;
                Health = 55;
                break;
        }
    }

    public void enemyDied()
    {
        concurrentEnemies--;
    }
}
