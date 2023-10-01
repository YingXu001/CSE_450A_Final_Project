using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEnemy : MonoBehaviour
{
    public float pushBackForce = 10f;  // Force with which the submarine is pushed back, adjust as needed
    private int timesHit = 0;     // Counter to keep track of the number of times the monster has been attacked
    public float moveSpeed = 2f;
    public float detectionDistance = 2f;

    public Vector2 moveDirection;
    private float randomMoveTime;

    private void Start()
    {
        PickRandomDirection();
    }

    private void Update()
    {
        Move();
        DetectObstacle();
    }

    void Move()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    void DetectObstacle()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, detectionDistance);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Boundary"))
            {
                PickRandomDirection();
            }
        }
    }

    private void PickRandomDirection()
    {
        moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) {
            timesHit++;

            if (timesHit >= 3)
            {
                Destroy(gameObject);
            }

            Destroy(collision.gameObject);
        }

        // Check if the colliding object is the submarine (tagged as Player)
        else if (collision.gameObject.CompareTag("Player"))
        {
            // Retrieve the Health component of the submarine and decrease its health
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.DamagePlayer(15);
            }

            // Push the submarine back in the opposite direction of its movement
            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                Vector2 pushDirection = -playerRigidbody.velocity.normalized;
                playerRigidbody.AddForce(pushDirection * pushBackForce, ForceMode2D.Impulse);
            }
        }
    }
}
