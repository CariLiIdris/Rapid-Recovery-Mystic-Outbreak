using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootChest : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    public List<Loot> lootList = new List<Loot>();

    Loot GetDroppedItem()
    {
        int randomNum = Random.Range(1, 101);
        List <Loot> possibleItems = new List<Loot>();
        foreach (Loot item in lootList)
        {
            if(randomNum <= item.dropChance)
            {
                possibleItems.Add(item);
            }
        }
        if (possibleItems.Count > 0)
        {
            Loot droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return droppedItem;
        }
        Debug.Log("No loot dropped");
        return null;
    }

    public void InstantiateLoot(Vector3 spawnPos)
    {
        Loot droppedItem = GetDroppedItem();
        if (droppedItem != null)
        {
            GameObject lootGameObj = Instantiate(droppedItemPrefab, spawnPos, Quaternion.identity);
            lootGameObj.GetComponent<SpriteRenderer>().sprite = droppedItem.lootSprite;
        }
    }
}
