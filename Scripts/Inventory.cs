using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> itemList;
    private const int MAX_ITEMS = 2;

    public Inventory()
    {
        itemList = new List<Item>();
    }

    public void AddItem(Item.ItemType t)
    {
        if (itemList.Count < MAX_ITEMS)
        {
            itemList.Add(new Item(t));
        }     
    }

    public void RemoveItem()
    {
        if (itemList.Count > 0)
        {
            itemList.RemoveAt(itemList.Count - 1);
        }
    }

    public void ListItems()
    {
        foreach (var item in itemList)
        {
            Debug.Log(item.GetItemType());
        }
    }

    public void UseItem(string player)
    {
        if (itemList.Count > 0)
        {
            Item.ItemType itemType = itemList[itemList.Count - 1].GetItemType();

            GameManager.Instance.UseItem(itemType, player);

            RemoveItem();
        }
        
    }

    public void ClearItems()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            RemoveItem();
        }
    }
}
