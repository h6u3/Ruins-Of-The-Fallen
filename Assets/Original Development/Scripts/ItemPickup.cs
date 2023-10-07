using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;

    // On trigger code (making contact with items to pick them up)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Pickup();
        }
    }

    public void Pickup()
    {
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
        ItemNameDisplayScript._instance.HideToolTip();
    }
}
