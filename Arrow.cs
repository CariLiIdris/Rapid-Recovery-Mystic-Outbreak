using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public float speed = 20;
    public Rigidbody2D rb;
    public int damage = 25;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null) 
        {
            enemy.TakeDamage(damage);
        }

        GameObject effect = Instantiate(impactEffect, transform.position, transform.rotation);

        Destroy(gameObject);
        Destroy(effect);
    }

}
