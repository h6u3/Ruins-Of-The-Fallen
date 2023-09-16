using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public static PlayerStats instance;
    private float Health = 30f;
    private float Attack = 5f;

    private void Awake() {
        instance = this;
    }

    public void changeHealth(float damage) {
        Health -= damage;
    }

    public float getHealth() {
        return Health;
    }

    public float getAttack() {
        return Attack;
    }
}
