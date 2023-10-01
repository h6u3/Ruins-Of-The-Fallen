using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Create more items
[CreateAssetMenu(fileName = "Hotbar Item", menuName = "Hotbar Item/Create Item")]
public class ItemAsset : ScriptableObject
{
    public itemType item_type; //Item type
    public Sprite item_sprite; //For UI element
}

//Enum of the items available. 
public enum itemType { Sword, Pickaxe, Axe };

