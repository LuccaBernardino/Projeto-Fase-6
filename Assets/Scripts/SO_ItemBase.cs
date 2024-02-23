using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    quest,
    weapon,
    armor,
    food
}

public class SO_ItemBase : ScriptableObject
{
    public string ItemName;
    public string description;
    public float value;
    public ItemType type;
    public GameObject icon;
}
