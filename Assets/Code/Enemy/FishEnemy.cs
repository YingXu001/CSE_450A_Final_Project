using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishEnemy : MonoBehaviour
{
    public float moveSpeed = 3f;  // Fish's movement speed
    public float detectionDistance = 2f;

    private Vector2 moveDirection;
    private float changeDirectionDelay = 0.5f; // Duration for which the enemy keeps moving in the current direction after a collision
    private float timeSinceLastDirectionChange;

    private void Start()
    {
        PickRandomDirection();
        timeSinceLastDirectionChange = Time.time;
    }

    void Update()
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
                Vector2 awayFromWall = (Vector2)transform.position - hit.point;
                PickDirection(awayFromWall);
                timeSinceLastDirectionChange = Time.time;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.time - timeSinceLastDirectionChange < changeDirectionDelay)
            return;

        // Check for player collision
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            SubController player = collision.gameObject.GetComponent<SubController>();
            if (player.HasSheild())
            {
                player.DeactivateShield();
            }
            else
            {
                if (playerHealth != null)
                {
                    playerHealth.DamagePlayer(10);
                }
            }
            Destroy(gameObject);
        }
        // Check for bullet collision
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
        // If it collides with a wall or boundary, pick a direction away from the collision
        else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Boundary"))
        {
            Vector2 awayFromCollision = -collision.contacts[0].normal;
            PickDirection(awayFromCollision);
            timeSinceLastDirectionChange = Time.time;
        }
    }

    void PickRandomDirection()
    {
        moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void PickDirection(Vector2 preferredDirection)
    {
        moveDirection = preferredDirection + new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
        moveDirection.Normalize();
    }
}
