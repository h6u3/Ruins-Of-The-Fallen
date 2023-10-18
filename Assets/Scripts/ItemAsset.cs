using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Create more items
[CreateAssetMenu(fileName = "Hotbar Item", menuName = "Hotbar Item/Create Item")]
public class ItemAsset : ScriptableObject
{
    public itemType item_type; //Item type
}

//Enum of the items available. 
public enum itemType { Sword, Pickaxe, Axe };

