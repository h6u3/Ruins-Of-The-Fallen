using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemController : MonoBehaviour
{
    public Item item;

    public Button RemoveButton;

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
        PlayerStatsUI.Instance.IncreaseHealth(item.HealthValue);
        PlayerStatsUI.Instance.IncreaseHydration(item.HydrationValue);
        PlayerStatsUI.Instance.IncreaseHunger(item.HungerValue);

        RemoveItem();
    }
}
