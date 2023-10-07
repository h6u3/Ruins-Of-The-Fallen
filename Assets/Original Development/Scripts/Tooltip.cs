using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public Item item;

    public void SetItem(Item newItem)
    {
        item = newItem;
    }

    private void OnMouseEnter()
    {
        ItemNameDisplayScript._instance.SetAndShowTextBox(item.itemName);
    }

    private void OnMouseExit()
    {
        ItemNameDisplayScript._instance.HideToolTip();
    }
}