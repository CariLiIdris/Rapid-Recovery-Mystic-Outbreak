using Cainos.PixelArtPlatformer_VillageProps;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteract : MonoBehaviour
{
    public GameObject Player { get; set; }
    public bool CanInteract { get; set; }

    private Chest chest;
    public GameObject chestPos;

    public bool Looted = false;

    //LootTable
    [Header("Loot")]
    public List<LootItem> lootTable = new List<LootItem>();

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        chest = GetComponent<Chest>();
    }

    private void Update()
    {
        if (CanInteract)
        {
            if (Input.GetButtonDown("Interact") & !Looted)
            {
                chest.Open();
                Looted = true;
                foreach (LootItem lootItem in lootTable)
                {
                    if (Random.Range(0f, 100f) <= lootItem.dropChance)
                    {
                        InstantiateLootItem(lootItem.itemPrefab);
                    }
                    break;
                }
            }
        }
    }

    void InstantiateLootItem(GameObject lootItem)
    {
        if (lootItem)
        {
            GameObject droppedLootItem = Instantiate(lootItem, chestPos.transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Player)
        {
            CanInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Player)
        {
            CanInteract = false;
        }
    }

}
