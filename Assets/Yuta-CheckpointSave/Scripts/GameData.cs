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
    public List<Item> Items;

    public GameData()
    {
        this.playerPosition = Vector3.zero;
        this.Health = 0;
        this.Hydration = 0;
        this.Hunger = 0;
        this.Items = new List<Item>();
    }
}
