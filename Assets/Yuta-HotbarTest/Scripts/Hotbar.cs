using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    public List<itemType> hotbarList = new List<itemType>();
    [SerializeField] GameObject sword;
    [SerializeField] GameObject pickaxe;
    [SerializeField] GameObject axe;
    public int selectedItem;

    private Dictionary<itemType, GameObject> setActive = new Dictionary<itemType, GameObject>() { };

    private void Start()
    {
        setActive.Add(itemType.Sword, sword);
        setActive.Add(itemType.Pickaxe, pickaxe);
        setActive.Add(itemType.Axe, axe);

        NewItemSelected();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && hotbarList.Count > 0) {
            selectedItem = 0;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && hotbarList.Count > 0)
        {
            selectedItem = 1;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && hotbarList.Count > 0)
        {
            selectedItem = 2;
            NewItemSelected();
        }
    }
    private void NewItemSelected()
    {
        sword.SetActive(false);
        pickaxe.SetActive(false);
        axe.SetActive(false);

        GameObject GOSelected = setActive[hotbarList[selectedItem]];
        GOSelected.SetActive(true);
    }
}
