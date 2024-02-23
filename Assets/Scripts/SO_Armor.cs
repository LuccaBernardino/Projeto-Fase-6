using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Armor")]

public class SO_Armor : SO_ItemBase
{
    public float armorBonus;
    public float durability;

    private void Awake()
    {
        type = ItemType.armor;
    }
}
