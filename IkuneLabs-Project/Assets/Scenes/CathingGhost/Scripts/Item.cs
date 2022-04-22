using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Capsule,
        Glue,
        Clock,
        Armor,
    }

    public ItemType itemType;
    public int amount;
}
