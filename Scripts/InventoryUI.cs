using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Sprite snowflake;
    [SerializeField] private Sprite stopwatch;
    [SerializeField] private Sprite mushroom;

    private Image icon1;
    private Image icon2;

    void Start()
    {
        icon1 = gameObject.transform.GetChild(0).GetComponent<Image>();
        icon2 = gameObject.transform.GetChild(1).GetComponent<Image>();
    }

    public void AddItem(Item.ItemType itemType)
    {
        if (!icon1.gameObject.activeInHierarchy)
        {
            SetSprite1(itemType);
        }
        else if (!icon2.gameObject.activeInHierarchy)
        {
            SetSprite2(itemType);
        }
    }
    private void SetSprite1(Item.ItemType itemType)
    {
        switch (itemType)
        {
            case Item.ItemType.Snowflake:
            {
                icon1.sprite = snowflake;
                icon1.gameObject.SetActive(true);
                break;
            }
            case Item.ItemType.Stopwatch:
            {
                icon1.sprite = stopwatch;
                icon1.gameObject.SetActive(true);
                break;
            }
            case Item.ItemType.Mushroom:
            {
                icon1.sprite = mushroom;
                icon1.gameObject.SetActive(true);
                break;
            }
        }
    }

    private void SetSprite2(Item.ItemType itemType)
    {
        switch (itemType)
        {
            case Item.ItemType.Snowflake:
            {
                icon2.sprite = snowflake;
                icon2.gameObject.SetActive(true);
                break;
            }
            case Item.ItemType.Stopwatch:
            {
                icon2.sprite = stopwatch;
                icon2.gameObject.SetActive(true);
                break;
            }
            case Item.ItemType.Mushroom:
            {
                icon2.sprite = mushroom;
                icon2.gameObject.SetActive(true);
                break;
            }
        }
    }

    public void RemoveItem()
    {
        if (icon2.gameObject.activeInHierarchy)
        {  
            RemoveSprite2();
        }
        else if (icon1.gameObject.activeInHierarchy)
        {
            RemoveSprite1();
        }
    }

    public void ClearItems()
    {
        for (int i = 0; i < 3; i++)
        {
            RemoveItem();
        }
    }

    private void RemoveSprite1()
    {
        icon1.gameObject.SetActive(false);
    }

    private void RemoveSprite2()
    {
        icon2.gameObject.SetActive(false);
    }
}
