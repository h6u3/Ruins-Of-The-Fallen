using UnityEngine;

public class ItemController : MonoBehaviour
{
    public Item item;

    public void RemoveItem()
    {
        InventoryManager.Instance.RemoveItem(item);
        Destroy(gameObject);
    }

    public void UseItem()
    {
        if (item.isConsumable == true)
        {
            PlayerStatsUI.Instance.IncreaseHealth(item.HealthValue);
            PlayerStatsUI.Instance.IncreaseHydration(item.HydrationValue);
            PlayerStatsUI.Instance.IncreaseHunger(item.HungerValue);
            RemoveItem();
        }
    }
}