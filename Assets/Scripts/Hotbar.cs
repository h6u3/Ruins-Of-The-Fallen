using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    //List to store items in the hotbar
    public List<itemType> hotbarList = new List<itemType>();

    //Serialized game objects for different item types
    [SerializeField] GameObject sword;
    [SerializeField] GameObject pickaxe;
    [SerializeField] GameObject axe;

    [SerializeField] Image[] hotbarImages = new Image[3];
    [SerializeField] Image[] backgroundImages = new Image[3];

    [SerializeField] Sprite swordSprite;
    [SerializeField] Sprite pickaxeSprite;
    [SerializeField] Sprite axeSprite;

    //Index of the current selected item
    public int selectedItem;

    // Dictionary to map item types to their respective GameObjects
    private Dictionary<itemType, GameObject> setActive = new Dictionary<itemType, GameObject>() { };

    private void Start()
    {
        //Initialize Dictionary by mapping item types to the game objects
        setActive.Add(itemType.Sword, sword);
        setActive.Add(itemType.Pickaxe, pickaxe);
        setActive.Add(itemType.Axe, axe);

        //Call the method to set the initial selected item
        NewItemSelected();
        backgroundImages[0].color = Color.white;
        backgroundImages[1].color = Color.grey;
        backgroundImages[2].color = Color.grey;

        hotbarImages[0].sprite = swordSprite;
        hotbarImages[1].sprite = pickaxeSprite;
        hotbarImages[2].sprite = axeSprite;
    }

    private void Update()
    {
        //Check for keypress. Currently for 123, call the method to update the selected item's visibility
        //in the game
        if (Input.GetKeyDown(KeyCode.Alpha1) && hotbarList.Count > 0) {
            selectedItem = 0;
            NewItemSelected();
            backgroundImages[0].color = Color.white;
            backgroundImages[1].color = Color.grey;
            backgroundImages[2].color = Color.grey;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && hotbarList.Count > 0)
        {
            selectedItem = 1;
            NewItemSelected();
            backgroundImages[0].color = Color.grey;
            backgroundImages[1].color = Color.white;
            backgroundImages[2].color = Color.grey;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && hotbarList.Count > 0)
        {
            selectedItem = 2;
            NewItemSelected();
            backgroundImages[0].color = Color.grey;
            backgroundImages[1].color = Color.grey;
            backgroundImages[2].color = Color.white;
        }
    }

    private void NewItemSelected()
    {
        //Deactivate all gameobject items by default
        sword.SetActive(false);
        pickaxe.SetActive(false);
        axe.SetActive(false);

        //Activate the selected item from the hotbar list at corresponding index
        GameObject Selected = setActive[hotbarList[selectedItem]];
        Selected.SetActive(true);
    }
}
