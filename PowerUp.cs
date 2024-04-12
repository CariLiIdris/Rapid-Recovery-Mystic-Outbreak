using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public GameObject pickUpEffect;
    public int CS;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup(other);
        }
    }

    void Pickup(Collider2D player)
    {
        Instantiate(pickUpEffect, transform.position, transform.rotation);

        Player stats = player.GetComponent<Player>();
        stats.CS += 1;

        Destroy(gameObject);
    }
}
