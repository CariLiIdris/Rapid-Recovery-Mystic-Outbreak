using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public Player player;
    public PlayerMovement playerMovement;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        Debug.Log("Here");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.Die();
        }
    }
}
