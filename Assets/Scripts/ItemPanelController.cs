using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanelController : MonoBehaviour
{
    public CanvasRenderer ItemPanel;

    public Text itemNameText;
    public Text itemHealthText;
    public Text itemHydrationText;
    public Text itemHungerText;
    public Image itemIconImage;

    public void DisplaySelectedItem(Item item)
    {
        itemNameText.text = item.itemName;
        itemHealthText.text = "Health: " + item.HealthValue.ToString();
        itemHydrationText.text = "Hydration: " + item.HydrationValue.ToString();
        itemHungerText.text = "Hunger: " + item.HungerValue.ToString();
        itemIconImage.sprite = item.icon;
        gameObject.SetActive(true);
    }

    public void ClosePanel()
    {
        ItemPanel.gameObject.SetActive(false);
    }

    public void UseSelectedItem()
    {
        
    }
}