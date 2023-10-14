using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEnemy2 : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Vector2 moveDirection = Vector2.up;
    public float maxY = 3f;
    public float minY = -3f;
    private int timesHit = 0;
    public int hitsRequiredToDestroy = 2;
    public int damageAmount = 15; // Amount of damage to the player on collision

    private SubController player;

    void Start()
    {
        player = FindObjectOfType<SubController>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (moveDirection == Vector2.up && transform.position.y >= maxY)
        {
            moveDirection = Vector2.down;
        }
        else if (moveDirection == Vector2.down && transform.position.y <= minY)
        {
            moveDirection = Vector2.up;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();

            if (playerHealth)
            {
                playerHealth.DamagePlayer(damageAmount);
                player.IncreaseEnergy(damageAmount);
                Destroy(gameObject);
            }
        }

        else if (collision.gameObject.CompareTag("Bullet"))
        {
            timesHit++;

            if (timesHit >= hitsRequiredToDestroy)
            {
                Destroy(gameObject);
                player.IncreaseEnergy(damageAmount); // Increase player energy by the same amount of damage
            }

            Destroy(collision.gameObject);
        }
    }
}