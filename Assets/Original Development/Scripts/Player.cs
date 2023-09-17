using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, DataInterface
{
    public static Player Instance;

    public int Health;
    public int Hydration;
    public int Hunger;

    public Text HealthText;
    public Text HydrationText;
    public Text HungerText;

    private void Awake()
    {
        Instance = this;
    }

    public void IncreaseHealth(int value)
    {
        Health += value;
        if (Health > 100)
            Health = 100;
        HealthText.text = $"HP: {Health}";
    }

    public void IncreaseHydration(int value)
    {
        Hydration += value;
        if (Hydration > 100)
            Hydration = 100;
        HydrationText.text = $"Hydration: {Hydration}%";
    }

    public void IncreaseHunger(int value)
    {
        Hunger += value;
        if (Hunger > 100)
            Hunger = 100;
        HungerText.text = $"Hunger: {Hunger}%";
    }

    public void LoadData(GameData gameData)
    {
        this.Health = gameData.Health;
        this.Hydration = gameData.Hydration;
        this.Hunger = gameData.Hunger;

        HealthText.text = $"HP: {Health}";
        HydrationText.text = $"Hydration: {Hydration}%";
        HungerText.text = $"Hunger: {Hunger}%";
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.Health = this.Health;
        gameData.Hydration = this.Hydration;
        gameData.Hunger = this.Hunger;
    }
}
