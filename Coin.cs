using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private int XPAmount = 2; 
    public int value;
    Player player;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        ;
    }

    // Update is called once per frame
    void Update()
    {
        ;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioManager.PlaySFX(audioManager.coin);
            XPManager.instance.AddXP(XPAmount);
            Destroy(gameObject);
            CoinCounter.instance.IncreaseCoins(value);
        }
    }
}
