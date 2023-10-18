using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;

    public int Health;
    public int Hydration;
    public int Hunger;

    public List<Item> Items = new List<Item>();

    public GameData()
    {
        this.playerPosition = new Vector3(-575, 30, 165);

        this.Health = 50;
        this.Hydration = 50;
        this.Hunger = 50;

        this.Items = new List<Item>();
    }
}
