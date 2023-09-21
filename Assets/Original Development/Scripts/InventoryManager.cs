using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;
using System.Linq;

public class InventoryManager : MonoBehaviour, DataInterface
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        Items.Add(item);
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);

            var itemName = obj.transform.GetChild(0);
            var itemIcon = obj.transform.GetChild(1);

            itemName.GetComponent<TextMeshProUGUI>().text = item.itemName;
            itemIcon.GetComponent<Image>().sprite = item.icon;

            obj.GetComponent<ItemController>().item = item;

        }
    }

    public List<Item> SortAlphaAsc()
    {
        return Items.OrderBy(Item => Item.itemName).ToList();
    }

    public List<Item> SortAlphaDesc()
    {
        return Items.OrderBy(Item => Item.itemName).Reverse().ToList();
    }

    public List<Item> SortIDAsc()
    {
        return Items.OrderBy(Item => Item.id).ToList();
    }

    public List<Item> SortIDDesc()
    {
        return Items.OrderBy(Item => Item.id).Reverse().ToList();
    }

    public void LoadData(GameData gameData)
    {
        this.Items = gameData.Items;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.Items = this.Items;
    }
}
