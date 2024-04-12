using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public int damage;
    public Player player;
    public PlayerMovement playerMovement;
    private Animator anim;

    int XPAmount = 50;

    [SerializeField] FloatingHealthBar healthBar;

    //LootTable
    [Header("Loot")]
    public List<LootItem> lootTable = new List<LootItem>(); 

    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Start()
    {
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
        anim = GetComponent<Animator>();
    }

    public  void TakeDamage(int  damage)
    {
        anim.SetTrigger("takeHit");
        health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth);

        if (health <= 0)
        {
            
            Die();
        }
    }

    void Die()
    {
        //Spawn Item
        foreach(LootItem lootItem in lootTable)
        {
            if (Random.Range(0f, 100f) <= lootItem.dropChance)
            {
                InstantiateLootItem(lootItem.itemPrefab);
            }
            break;
        }
        XPManager.instance.AddXP(XPAmount);
        Destroy(gameObject);    
    }

    void InstantiateLootItem(GameObject lootItem)
    {
        if (lootItem)
        {
            GameObject droppedLootItem = Instantiate(lootItem, transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !playerMovement.isDashing)
        {
            anim.SetTrigger("attack");
            playerMovement.KBCounter = playerMovement.KBTotalTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                playerMovement.KnockFromRight = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                playerMovement.KnockFromRight = false;
            }
            player.TakeDamage(damage);
        }
    }
}
