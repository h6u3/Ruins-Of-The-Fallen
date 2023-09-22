using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager instance;
    private PlayerStats playerStats;

    private void Awake() {
        instance = this;
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
