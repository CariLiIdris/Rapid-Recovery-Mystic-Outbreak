using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Player player;
    private Animator anim;
    SpriteRenderer spriteRenderer;
    public Sprite passive, active;

    AudioManager audioManager;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioManager.PlaySFX(audioManager.checkpoint);
            player.UpdateCheckpoint(transform.position);
            spriteRenderer.sprite = active;
        }
    }
}
