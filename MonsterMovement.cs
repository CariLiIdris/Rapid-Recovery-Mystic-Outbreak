using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float movementSpeed;
    public int patrolDestination;

    public Transform playerTransform;
    public bool isChasing;
    public float chaseDistance;
    public Animator anim;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isChasing)
        {
            anim.SetBool("isRunning", true);

            if (transform.position.x > playerTransform.position.x) 
            {
                transform.localScale = new Vector3(-1, 1, 1);
                transform.position += Vector3.left * movementSpeed * Time.deltaTime;
            }
            if (transform.position.x < playerTransform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                transform.position += Vector3.right * movementSpeed * Time.deltaTime;
            }

        }
        else
        {
            anim.SetBool("isRunning", true);

            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance) 
            {
                isChasing = true;
            }

            if (patrolDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, movementSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoints[0].position) < .2f)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    patrolDestination = 1;
                }
            }

            if (patrolDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, movementSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoints[1].position) < .2f)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    patrolDestination = 0;
                }
            }
        }
    }
}
