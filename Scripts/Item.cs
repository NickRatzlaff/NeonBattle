using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Snowflake,
        Mushroom,
        Stopwatch
    }

    private Rigidbody2D body;
    private ItemType type;

    
    public Item(ItemType t)
    {
        type = t;
    }

    public ItemType GetItemType()
    {
        return type;
    }
}
