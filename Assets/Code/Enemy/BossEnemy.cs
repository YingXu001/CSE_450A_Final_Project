using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public float pushBackForce = 10f;
    public GameObject bossBulletPrefab;
    public float shootInterval = 2f;
    public float bulletSpeed = 10f;

    private float lastShotTime;

    private SubController player; // Get the submarine object

    private void Start()
    {
        lastShotTime = Time.time;

        player = FindObjectOfType<SubController>();

        // Ignore collisions between the boss and its bullets
        Collider2D bossCollider = GetComponent<Collider2D>();
        foreach (var bullet in GameObject.FindGameObjectsWithTag("BossBullet"))
        {
            Collider2D bulletCollider = bullet.GetComponent<Collider2D>();
            if (bulletCollider)
            {
                Physics2D.IgnoreCollision(bossCollider, bulletCollider);
            }
        }
    }

    private void Update()
    {
        HandleShooting();
    }

    private void HandleShooting()
    {
        if (Time.time - lastShotTime >= shootInterval)
        {
            ShootBulletsInAllDirections();
            lastShotTime = Time.time;
        }
    }

    private void ShootBulletsInAllDirections()
    {
        for (int i = 0; i < 8; i++) // 8 directions
        {
            float angle = i * 45f;
            Vector2 spawnOffset = Quaternion.Euler(0, 0, angle) * Vector2.up * 0.5f; // Adjust the offset as needed
            Vector2 spawnPosition = (Vector2)transform.position + spawnOffset;
            GameObject bulletInstance = Instantiate(bossBulletPrefab, spawnPosition, Quaternion.Euler(0, 0, angle));

            bulletInstance.tag = "BossBullet";  // Set the tag to "BossBullet"
            Rigidbody2D bulletRb = bulletInstance.GetComponent<Rigidbody2D>();
            if (bulletRb)
            {
                Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up;
                bulletRb.velocity = direction * bulletSpeed;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HandleCollisionWithPlayer(collision.gameObject);
        }
    }

    private void HandleCollisionWithPlayer(GameObject playerObj)
    {
        Health playerHealth = playerObj.GetComponent<Health>();
        SubController playerController = playerObj.GetComponent<SubController>();

        if (playerController.HasSheild())
        {
            playerController.DeactivateShield();
        }
        else
        {
            playerHealth?.DamagePlayer(50); // Player takes 50 damage upon collision
        }

        Rigidbody2D playerRigidbody = playerObj.GetComponent<Rigidbody2D>();
        if (playerRigidbody != null)
        {
            Vector2 pushDirection = -playerRigidbody.velocity.normalized;
            playerRigidbody.AddForce(pushDirection * pushBackForce, ForceMode2D.Impulse);
        }
    }
}