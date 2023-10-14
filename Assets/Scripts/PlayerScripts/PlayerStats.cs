using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public static PlayerStats instance;
    private PlayerStatsUI player;
    private float Health = 50f;
    private float Attack = 5f;

    private void Awake() {
        player = GetComponent<PlayerStatsUI>();
        instance = this;
    }

    private void Update()
    {
        //Health = (float)player.Health;
    }

    public void changeHealth(float damage) {
        player.DecreaseHealth((int)damage);
        Health -= (int) damage;
    }

    public float getHealth() {
        return Health;
    }

    public float getAttack() {
        return Attack;
    }
}
