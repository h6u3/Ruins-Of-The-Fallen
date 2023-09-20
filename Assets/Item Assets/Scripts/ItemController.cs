using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemController : MonoBehaviour
{
    public Item item;

    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);

        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }

    public void UseItem()
    {
        Player.Instance.IncreaseHealth(item.HealthValue);
        Player.Instance.IncreaseHydration(item.HydrationValue);
        Player.Instance.IncreaseHunger(item.HungerValue);

        RemoveItem();
    }
}
