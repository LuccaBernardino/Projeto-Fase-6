using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/Weapon")]

public class SO_Weapons : SO_ItemBase
{
    public float attackBonus;
    public float durability;

    private void Awake()
    {
        type = ItemType.weapon;
    }
}
