using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloraToolTip : MonoBehaviour
{
    public Item item;
    public FloraManager floraManager;

    private void Start()
    {
        floraManager = FindObjectOfType<FloraManager>();
    }

    public void SetItem(Item newItem)
    {
        item = newItem;
    }

    private void Update()
    {
        if (floraManager.harvested)
        {
            ItemNameDisplayScript._instance.HideToolTip();
        }
    }

    private void OnMouseEnter()
    {
        if (!floraManager.harvested)
        {
            ItemNameDisplayScript._instance.SetAndShowTextBox("Left Click to Harvest \n" + item.itemName);
        }
    }

    private void OnMouseExit()
    {
        ItemNameDisplayScript._instance.HideToolTip();
    }
}