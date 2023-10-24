using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingToolTip : MonoBehaviour
{
    public Item item;
    private float range = 8f;
    private WoodManager woodManager;

    private void Start()
    {
        woodManager = FindObjectOfType<WoodManager>();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range, woodManager.mineableObject))
        {
            ItemNameDisplayScript._instance.SetAndShowTextBox("Use Axe to Cut \n" + item.itemName);
        }
    }

    public void SetItem(Item newItem)
    {
        item = newItem;
    }

    private void OnMouseExit()
    {
        ItemNameDisplayScript._instance.HideToolTip();
    }
}