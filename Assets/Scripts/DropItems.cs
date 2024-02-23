using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class DropItems : NetworkBehaviour
{
   public List<ItemChance> dropList;
   public void Drop()
   {
    foreach (ItemChance item in dropList)
    {
        float chance = UnityEngine.Random.Range(0, 100);

        if(chance <= item.dropChance)
        {
            Vector2 dropPosition = new Vector2
            (UnityEngine.Random.Range(transform.position.x-1, transform.position.x+1), UnityEngine.Random.Range(transform.position.y-1, transform.position.y+1));

            GameObject new_item = Instantiate (item.item, dropPosition, transform.rotation);

            NetworkServer.Spawn(new_item);
        }
    }
   }
}

[Serializable]
public class ItemChance
{
    public GameObject item;
    public float dropChance;
}
