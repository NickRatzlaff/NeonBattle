using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    [SerializeField] private GameObject snowflake;
    [SerializeField] private GameObject stopwatch;
    [SerializeField] private GameObject mushroom;

    private List<GameObject> itemList;

    private float delayTime = 6.0f;

    // Start is called before the first frame update
    void Start()
    {
        itemList = new List<GameObject>();
        itemList.Add(snowflake);
        itemList.Add(stopwatch);
        itemList.Add(mushroom);
        StartCoroutine(GenerateItems());
    }

    IEnumerator GenerateItems()
    {
        int index = 0;
        while (true)
        {
            yield return new WaitForSeconds(delayTime);
            Instantiate(itemList[index], transform.position, Quaternion.identity);
            if (index < itemList.Count - 1)
            {
                index++;
            }
            else 
            {
                index = 0;
            }
        }
    }
}
