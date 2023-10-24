using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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