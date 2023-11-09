using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : MonoBehaviour
{
    // Outlets
    Rigidbody2D _rb;

    // State Tracking
    Transform target;
    public float torpedoSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float acceleration = torpedoSpeed / 2f;
        float maxSpeed = torpedoSpeed;

        // Home in on target
        ChooseNearestTarget();
        if (target != null)
        {
            //Rotate towards target
            Vector2 directionToTarget = target.position - transform.position;
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

            _rb.MoveRotation(angle);
        }

        // Accelerate forward
        _rb.AddForce(transform.right * acceleration);

        // Cap max speed
        _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, maxSpeed);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Only explode on Asteroids
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    void ChooseNearestTarget()
    {
        // High default means first asteroid will always count as closest
        float closestDistance = 9999f;

        // Check each enemy type to see if it's the closest
        FindAndTrackNearestEnemyType<MonsterEnemy1>(ref closestDistance);
        FindAndTrackNearestEnemyType<MonsterEnemy2>(ref closestDistance);
        FindAndTrackNearestEnemyType<FinalBossEnemy>(ref closestDistance);
    }

    void FindAndTrackNearestEnemyType<T>(ref float closestDistance) where T : MonoBehaviour
    {
        T[] enemies = FindObjectsOfType<T>();
        for (int i = 0; i < enemies.Length; i++)
        {
            T enemy = enemies[i] as T;

            if (enemy == null)
                continue;

            Vector2 directionToTarget = enemy.transform.position - transform.position;

            // Filter for the closest target we've seen so far
            if (directionToTarget.sqrMagnitude < closestDistance)
            {
                // Update closest distance for future comparisons
                closestDistance = directionToTarget.sqrMagnitude;

                // Track this enemy as the current closest target
                target = enemy.transform;
            }
        }
    }
}