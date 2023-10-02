using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEnemy : MonoBehaviour
{
    public float pushBackForce = 10f;  // Force with which the submarine is pushed back, adjust as needed
    private int timesHit = 0;     // Counter to keep track of the number of times the monster has been attacked
    public float moveSpeed = 2f;
    public float detectionDistance = 2f;
    private float changeDirectionDelay = 0.5f; // Duration for which the enemy keeps moving in the current direction after a collision
    private float timeSinceLastDirectionChange;

    public Vector2 moveDirection;
    private float randomMoveTime;

    private void Start()
    {
        PickRandomDirection();
        timeSinceLastDirectionChange = Time.time;
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
        if (Time.time - timeSinceLastDirectionChange < changeDirectionDelay)
            return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, detectionDistance);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Boundary"))
            {
                Vector2 awayFromWall = transform.position - (Vector3)hit.point;
                PickRandomDirectionAwayFromCollision(awayFromWall);
                timeSinceLastDirectionChange = Time.time;
            }
        }
    }


    private void PickRandomDirection()
    {
        const int maxAttempts = 5;
        int attempts = 0;

        do
        {
            moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, detectionDistance);

            if (hit.collider == null || (!hit.collider.CompareTag("Wall") && !hit.collider.CompareTag("Boundary")))
            {
                break;
            }

            attempts++;
        }
        while (attempts < maxAttempts);
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.time - timeSinceLastDirectionChange < changeDirectionDelay)
            return;

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
        else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Boundary"))
        {
            Vector2 awayFromCollision = -collision.contacts[0].normal;
            PickRandomDirectionAwayFromCollision(awayFromCollision);
            timeSinceLastDirectionChange = Time.time;
        }
    }
    private void PickRandomDirectionAwayFromCollision(Vector2 normal)
    {
        // Move in the direction opposite to the collision normal with a larger random offset
        moveDirection = -normal + new Vector2(Random.Range(-0.7f, 0.7f), Random.Range(-0.7f, 0.7f));
        moveDirection.Normalize();
    }

}
