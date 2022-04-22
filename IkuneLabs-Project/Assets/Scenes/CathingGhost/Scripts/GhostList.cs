using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GhostList : MonoBehaviour
{
    TextRay textRay;
    private List<GameObject> ghostCol = new List<GameObject>();
    List<int> ghostColor = new List<int>(4);
    public List<string> items;
    Inventory inventory;
    private List<Item> itemList;
    int first = 0;

    public void CollectedGhost(GameObject ghost)
    {
        ghostCol.Add(ghost);
    }

    public void GetGhostColor(int col)
    {
        ghostColor.Add(col);
    }

    public List<int> GiveGhostColor()
    {
        return ghostColor;  
    }
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
    }

    public void GetInventory()
    {
        itemList = inventory.GetItemList();
    }

    public void GiveItemList()
    {
        if (itemList == null)
            return;
        else
            foreach (Item item in itemList)
                inventory.AddItem(item);
    }

    public int FistTime()
    {
        first++;
        return first;
    }
}
