using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public static PlayerStats instance;
    public PlayerStatsUI player;
    private float Attack = 5f;

    private void Awake() {
        player = GetComponent<PlayerStatsUI>();
        instance = this;
    }

    public void changeHealth(int damage) {
        player.DecreaseHealth(damage);
    }

    public void changeHunger(int value)
    {
        player.DecreaseHunger(value);
    }

    public void changeHydration(int value)
    {
        player.DecreaseHydration(value);
    }

    public float getHealth() {
        return player.Health;
    }

    public int getHunger()
    {
        return player.Hunger;
    }

    public int getHydration()
    {
        return player.Hydration;
    }

    public float getAttack() {
        return Attack;
    }

}
