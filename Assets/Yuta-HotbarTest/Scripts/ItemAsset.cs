using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hotbar Item", menuName = "Hotbar Item/Create Item")]
public class ItemAsset : ScriptableObject
{
    public itemType item_type;
    public Sprite item_sprite;
}

public enum itemType { Sword, Pickaxe, Axe };

