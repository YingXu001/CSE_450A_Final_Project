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

    // For shooting bullets
    public GameObject monsterBulletPrefab;
    public float shootInterval = 2f;
    public float bulletSpeed = 10f;
    private float lastShotTime;

    private SubController player;

    void Start()
    {
        player = FindObjectOfType<SubController>();
        lastShotTime = Time.time;
    }

    void Update()
    {
        Move();
        HandleShooting();
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

    private void HandleShooting()
    {
        if (Time.time - lastShotTime >= shootInterval)
        {
            ShootBullet();
            lastShotTime = Time.time;
        }
    }

    private void ShootBullet()
    {
        GameObject bulletInstance = Instantiate(monsterBulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bulletInstance.GetComponent<Rigidbody2D>();

        if (bulletRb)
        {
            Vector2 shootDirection = (player.transform.position - transform.position).normalized;
            bulletRb.velocity = shootDirection * bulletSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            SubController playerController = collision.gameObject.GetComponent<SubController>();

            if (playerHealth)
            {
                if (playerController.HasSheild())
                {
                    playerController.DeactivateShield();
                }
                else
                {
                    playerHealth.DamagePlayer(damageAmount);
                    player.IncreaseEnergy(damageAmount);
                    Destroy(gameObject);
                }
            }
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            timesHit++;

            if (timesHit >= hitsRequiredToDestroy)
            {
                Destroy(gameObject);
                player.IncreaseEnergy(damageAmount);
            }

            Destroy(collision.gameObject);
        }
    }
}