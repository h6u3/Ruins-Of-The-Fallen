using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public int HealthValue;
    public int HydrationValue;
    public int HungerValue;
    public Sprite icon;
    public bool isConsumable;
}