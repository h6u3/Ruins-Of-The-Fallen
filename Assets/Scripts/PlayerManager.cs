using System.Collections;
using System.Collections.Generic;
using System.Threading;
using StarterAssets;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance;
    public PlayerStats playerStats;
    public GameObject player;
    public Canvas GameUI;
    public Canvas DeathUI;
    public int rate = 1;
    private bool playerLives = true;
    private int timing = 0;
    private int hungerTimer = 0;
    private int hydrationTimer = 0;

    private void Awake()
    {
        instance = this;
        rate = 1;
    }

    public void setLiving(bool living)
    {
        playerLives = living;
    }

    public void FixedUpdate()
    {
        timing += 1;
        timing %= 200;
        hungerTimer += 1;
        hungerTimer %= 500;
        hydrationTimer += 1;
        hydrationTimer %= 400;
        //constant decrease of hydration and hunger. health decreases when either or both have depleted fully.
        if (playerStats.getHunger() > 0 && hungerTimer == 0)
        {
            playerStats.changeHunger(rate);
        }
        if (playerStats.getHydration() > 0 && hydrationTimer == 0)
        {
            playerStats.changeHydration(rate);
        }
        if (playerStats.getHunger() <= 0 && timing == 0)
        {
            takeDamage(rate);
        }
        if (playerStats.getHydration() <= 0 && timing == 0)
        {
            takeDamage(rate);
        }
    }

    private void Start()
    {
        playerStats = PlayerStats.instance;
        rate = 1;
    }

    public void takeDamage(int damageAmount)
    {
        if (playerLives)
        {
            playerStats.changeHealth(damageAmount);
        }
        checkAlive();
    }

    private void checkAlive()
    {
        if (playerStats.getHealth() <= 0)
        {
            Die();
        }
        else
        {
            playerLives = true;
        }
    }

    public void Die()
    {
        if (playerLives == true)
        {
            playerLives = false;
            playerStats.changeHealth(0); //updates health bar i think
            player.GetComponent<ThirdPersonController>().PlayerDies();
            if (GameUI.gameObject.activeSelf == true)
            {
                GameUI.gameObject.SetActive(!GameUI.gameObject.activeSelf);
            }
            if (DeathUI.gameObject.activeSelf == false)
            {
                DeathUI.gameObject.SetActive(!DeathUI.gameObject.activeSelf);
                player.GetComponent<StarterAssetsInputs>().SetCursorState(false);
                player.GetComponent<PlayerCombat>().setPlayerDead(true);
            }
            Debug.Log("Player Died");
        }
    }

}
