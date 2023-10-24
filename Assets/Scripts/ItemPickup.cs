using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;

    public void Pickup()
    {
        InventoryManager.Instance.AddItem(Item);
        Destroy(gameObject);
        ItemNameDisplayScript._instance.HideToolTip();
    }
}