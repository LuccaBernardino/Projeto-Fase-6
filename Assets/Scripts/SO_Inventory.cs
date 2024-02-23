using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Inventory")]
public class SO_Inventory : ScriptableObject
{
    public List <ItemSlot> itemList = new List <ItemSlot> ();

    public float coins;

    public void SpendCoins(float value)
    {
        coins -= value;
        coins = Mathf.Clamp(coins, 0, 99999);
    }

    public void AddCoins(float value)
    {
        coins += value;
        coins = Mathf.Clamp(coins, 0, 99999);
    }


    public void AddItem(SO_ItemBase item, int amount)
    {
        bool itemExists = false;

        foreach(ItemSlot slot in itemList)
        {
            if(slot.item == item)
            {
                slot.AddAmount(amount);
                itemExists = true;
                break;
            }
        }
        if (itemExists == false)
        {
            ItemSlot slot = new ItemSlot(item, amount);
            itemList.Add(slot);
        }
    }
    public void SwapItems(ItemSlot itemClicked, ItemSlot itemHovered)
    {
        if(itemList.Contains(itemClicked) && itemList.Contains(itemHovered))
        {
            int i1 = itemList.IndexOf(itemClicked);
            int i2 = itemList.IndexOf(itemHovered);

            itemList[i1] = itemHovered;
            itemList[i2] = itemClicked;
        }
    }

    public void RemoverItem(SO_ItemBase item, int amount)
    {
        foreach (ItemSlot slot in itemList)
        {
            if(slot.item == item)
            {
                if(slot.amount > 1)
                {
                    slot.RemoveAmount(amount);
                }
                else
                {
                    itemList.Remove(slot);
                }

                break;
            }
        }
    }
}

[System.Serializable]
public class ItemSlot
{
    public SO_ItemBase item;
    public int amount;

    public ItemSlot(SO_ItemBase new_item, int new_amount)
    {
        item = new_item;
        amount = new_amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }

    public void RemoveAmount(int value)
    {
        amount -= value;
    }
}






