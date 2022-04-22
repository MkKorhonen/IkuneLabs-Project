using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();

        AddItem(new Item { itemType = Item.ItemType.Capsule, amount = 10 });
        AddItem(new Item { itemType = Item.ItemType.Glue, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Armor, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Clock, amount = 1 });
    }

    public void AddItem(Item item)
    {
        bool itemAlreadyInInventory = false;
        foreach (Item inventoryItem in itemList)
        {
            if (inventoryItem.itemType == item.itemType)
            {
                inventoryItem.amount += item.amount;
                itemAlreadyInInventory = true;
            }
        }
        if (!itemAlreadyInInventory)
            itemList.Add(item);
    }

    public void RemoveItem(Item item)
    {
        foreach (Item inventoryItem in itemList)
        {
            if (inventoryItem.itemType == item.itemType)
            {
                if (item.amount > 0)
                    inventoryItem.amount -= item.amount;
                else
                    itemList.Remove(item);
            }
        }
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
