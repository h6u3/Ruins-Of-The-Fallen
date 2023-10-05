using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager instance;
    private PlayerStats playerStats;

    private void Awake() {
        instance = this;
    }

    private void Update()
    {
        float rate = 5 * Time.deltaTime;
        float randomNumber = Random.Range(0, 10);
        if ((playerStats.getHunger()>0) && (randomNumber >= 8.25))
        {
            playerStats.changeHunger((int)rate);
        }
        if ((playerStats.getHydration() > 0) && (randomNumber <= 1.5))
        {
            playerStats.changeHydration((int)rate);
        }
        if (playerStats.getHunger() <= 0)
        {
            takeDamage(rate);
        }
        if (playerStats.getHydration() <= 0)
        {
            takeDamage(rate);
        }
    }

    private void Start() {
        playerStats = PlayerStats.instance;
    }
    public GameObject player;
    bool playerLives = true;

    public void takeDamage(float damageAmount) {
        if (playerLives) {
            playerStats.changeHealth(damageAmount);
        }
        checkAlive();
    }

    private void checkAlive() {
        if (playerStats.getHealth() <= 0) {
            Die();
        }
    }

    public void Die() {
        playerLives = false;
        playerStats.changeHealth(0f); //updates health bar i think
        Debug.Log("Player Died");
    }
}
