using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    public CoinCounter CoinCounter;
    public int coins;

    public int deaths;

    public int maxHealth;
    public int health;

    [Header("Player XP")]
    [SerializeField] public int XP, maxXP, level;

    [Header("Player CS")]
    public int maxCS, CS, CSLevel = 1;

    [Header("CheckpointPOS")]
    public Vector2 checkpointPos;

    [Header("Slider Bars")]
    public PlayerHealthBar healthBar;
    public PlayerXPBar XPBar;
    public PlayerCSBar CSBar;

    public List<string> items;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CoinCounter.currentCoins = coins;
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        CSBar.SetMaxCS(maxCS);
        XPBar.SetXP(XP);
        checkpointPos = transform.position;
        items = new List<string>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        if (health <= 0) 
        {
            Die();
        }
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos;
    }

    public void Die()
    {
        audioManager.PlaySFX(audioManager.death);
        coins = 0;
        deaths += 1;
        XPBar.SetXP(0);
        DeathCounter.instance.IncreaseDeaths(1);
        health = 0;
        healthBar.SetHealth(health);
        StartCoroutine(Respawn(0.5f));
    }

    public void Heal(int HP)
    {
        health += HP;
        if (health > maxHealth) 
        {
        health = maxHealth;
        }
        healthBar.SetHealth(health);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Heal")
        {
            Heal(25);
            Destroy(other.gameObject);
        }
    }


    public IEnumerator Respawn(float duration)
    {
        yield return new WaitForSeconds(duration);
        health = maxHealth;
        healthBar.SetHealth(maxHealth);
        transform.position = checkpointPos;
    }

    private void OnEnable()
    {
        XPManager.instance.OnXPChange += HandleXPChange;
    }

    private void OnDisable()
    {
        XPManager.instance.OnXPChange -= HandleXPChange;
    }

    private void HandleXPChange(int newXP)
    {
        XP += newXP;
        XPBar.UpdatePlayerXPBar(newXP, maxXP);
        if (XP >= maxXP)
        {
            LevelUp();
        }
    }

    private void Update()
    {
        CoinCounter.currentCoins = coins;
        XPBar.UpdatePlayerXPBar(XP, maxXP);
        CSBar.UpdatePlayerCSBar(CS, maxCS);

        if (CS >= maxCS) 
        {
            ConjureLevelUp();
        }

        CSBar.SetCS(CS);
    }

    private void LevelUp()
    {
        maxHealth += 25;
        health = maxHealth;
        healthBar.SetHealth(maxHealth);

        level++;

        XP = 0;
        XPBar.SetXP(XP);
        maxXP += 25;
    }

    private void ConjureLevelUp()
    {
        health = maxHealth;
        healthBar.SetHealth(health);

        CSLevel++;
        CS = 0;

        
        maxCS *= 2 + CS;
        CSBar.SetCS(CS);
        CSBar.SetMaxCS(maxCS);
        CSBar.UpdatePlayerCSBar(CS, maxCS);
    }
}
