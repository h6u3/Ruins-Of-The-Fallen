using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager instance;
    private PlayerStats playerStats;
    public GameObject player;
    public CanvasRenderer GameUI;
    public CanvasRenderer DeathUI;
    bool playerLives = true;

    private void Awake() {
        instance = this;
    }

    private void Update()
    {
        //constant decrease of hydration and hunger. health decreases when either or both have depleted fully.
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
        player.GetComponent<ThirdPersonController>().PlayerDies();
        Debug.Log("Player Died");
    }
}
