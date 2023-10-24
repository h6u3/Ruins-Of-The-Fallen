using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class InventoryManagerTest
{
    private InventoryManager inventoryManager;

    [SetUp]
    public void SetUp()
    {
        inventoryManager = new GameObject().AddComponent<InventoryManager>();
        inventoryManager.GetItems().Clear(); // Clear Items list.
        inventoryManager.SetItemContentTransform(new GameObject().transform); // Set itemContentTransform.
        inventoryManager.SetInventoryItemPrefab(new GameObject()); // Set inventoryItemPrefab.
    }

    [Test]
    public void InventoryManager_AddItem_AddsItemToList()
    {
        // Arrange
        var inventoryManager = new GameObject().AddComponent<InventoryManager>();
        var item = CreateMockItem();

        // Act
        inventoryManager.AddItem(item);

        // Assert
        Assert.Contains(item, inventoryManager.GetItems());
    }

    [Test]
    public void InventoryManager_RemoveItem_RemovesItemFromList()
    {
        // Arrange
        var inventoryManager = new GameObject().AddComponent<InventoryManager>();
        var item = CreateMockItem();
        inventoryManager.AddItem(item);

        // Act
        inventoryManager.RemoveItem(item);

        // Assert
        Assert.IsFalse(inventoryManager.GetItems().Contains(item));
    }


    [Test]
    public void InventoryManager_RemoveItem_HandlesItemNotInList()
    {
        // Arrange
        var inventoryManager = new GameObject().AddComponent<InventoryManager>();
        var item = CreateMockItem();

        // Act
        inventoryManager.RemoveItem(item);

        // Assert: Make sure it doesn't throw an error.
    }

    private Item CreateMockItem()
    {
        Item item = ScriptableObject.CreateInstance<Item>();
        item.id = 1;
        item.itemName = "Mock Item";
        item.HealthValue = 10;
        item.HydrationValue = 5;
        item.HungerValue = 8;
        item.icon = null;
        item.isConsumable = true;

        return item;
    }
}