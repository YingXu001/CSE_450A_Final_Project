using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishEnemy : MonoBehaviour
{
    public float moveSpeed = 3f;  // Fish's movement speed
    public float detectionDistance = 2f;
    private Vector2 currentDirection; // The current direction the fish is moving
    
    private Vector2 moveDirection;

    private void Start()
    {
        PickRandomDirection();
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, detectionDistance);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Boundary"))
            {
                PickRandomDirection();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check for player collision
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.DamagePlayer(10);
            }
            Destroy(gameObject);
        }
        // Check for bullet collision
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
        // If it collides with anything else, pick a new direction (e.g. wall, obstacle)
        else
        {
            PickRandomDirection();
        }
    }

    void PickRandomDirection()
    {
        moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
