using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class InventoryManager : MonoBehaviour, DataInterface
{
    public static InventoryManager Instance;
    private List<Item> Items = new List<Item>();

    [SerializeField] private Transform itemContentTransform;
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private ItemPopUp itemPopUp;

    private void Awake()
    {
        if (Instance != null)
        {
            // Another instance already exists, so destroy this one to ensure it's a singleton.
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    /// Add an item to the inventory and update the UI and also show which item was picked up
    public void AddItem(Item item)
    {
        if (item != null)
        {
            Items.Add(item);
            if (itemPopUp != null)
            {
                itemPopUp.ShowPopUp(item.itemName);
            }
            UpdateUI();
        }
    }

    /// Remove an item from the inventory.
    public void RemoveItem(Item item)
    {
        if (item != null)
        {
            Items.Remove(item);
        }
    }

    /// Clear the UI and list items in the inventory.
    public void UpdateUI()
    {
        if (itemContentTransform != null && inventoryItemPrefab != null)
        {
            foreach (Transform item in itemContentTransform)
            {
                Destroy(item.gameObject);
            }

            foreach (var item in Items)
            {
                GameObject obj = Instantiate(inventoryItemPrefab, itemContentTransform);

                var itemNameText = obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                var itemIconImage = obj.transform.GetChild(1).GetComponent<Image>();

                if (itemNameText != null && itemIconImage != null)
                {
                    itemNameText.text = item.itemName;
                    itemIconImage.sprite = item.icon;

                    var itemController = obj.GetComponent<ItemController>();
                    if (itemController != null)
                    {
                        itemController.item = item;
                    }
                }
            }
        }
    }

    /// Sort items in ascending order based on item name.
    public List<Item> SortItemsAlphaAsc()
    {
        return Items.OrderBy(item => item.itemName).ToList();
    }

    /// Sort items in descending order based on item name.
    public List<Item> SortItemsAlphaDesc()
    {
        return Items.OrderByDescending(item => item.itemName).ToList();
    }

    /// Sort items in ascending order based on item ID.
    public List<Item> SortItemsIDAsc()
    {
        return Items.OrderBy(item => item.id).ToList();
    }

    /// Sort items in descending order based on item ID.
    public List<Item> SortItemsIDDesc()
    {
        return Items.OrderByDescending(item => item.id).ToList();
    }

    public void LoadData(GameData gameData)
    {
        this.Items = gameData.Items;
        UpdateUI();
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.Items = this.Items;
    }

    public void SortItemsAlphaAscending()
    {
        Items = SortItemsAlphaAsc();
        UpdateUI();
    }

    public void SortItemsAlphaDescending()
    {
        Items = SortItemsAlphaDesc();
        UpdateUI();
    }

    public void SortItemsIDAscending()
    {
        Items = SortItemsIDAsc();
        UpdateUI();
    }

    public void SortItemsIDDescending()
    {
        Items = SortItemsIDDesc();
        UpdateUI();
    }
}