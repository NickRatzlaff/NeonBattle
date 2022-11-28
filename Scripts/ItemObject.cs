using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    Item.ItemType itemType;

    void Start()
    {
        if (gameObject.name.Contains("Snowflake"))
        {
            itemType = Item.ItemType.Snowflake;
        }
        else if (gameObject.name.Contains("Mushroom"))
        {
            itemType = Item.ItemType.Mushroom;
        }
        else if (gameObject.name.Contains("Stopwatch"))
        {
            itemType = Item.ItemType.Stopwatch;
        }

        GameManager.OnGameStateChanged += ItemObjectOnGameStateChanged;
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= ItemObjectOnGameStateChanged;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name.Contains("Player"))
        {
            Destroy(gameObject);
        }       
    }

    public Item.ItemType GetItemType()
    {
        return itemType;
    }

    private void ItemObjectOnGameStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.RoundStart)
        {
            Destroy(gameObject);
        }
    }
}
