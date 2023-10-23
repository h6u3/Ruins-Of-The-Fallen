using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningToolTip : MonoBehaviour
{
    public Item item;
    private float range = 8f;
    private StoneManager stoneManager;

    private void Start()
    {
        stoneManager = FindObjectOfType<StoneManager>();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range, stoneManager.mineableObject))
        {
            ItemNameDisplayScript._instance.SetAndShowTextBox("Use Pickaxe to Mine \n" + item.itemName);
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