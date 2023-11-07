using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossEnemy : MonoBehaviour
{
    public GameObject bulletPrefab; // The bullet prefab to shoot
    public float teleportTime = 5.0f; // Time in seconds between each teleport
    public float shootDelay = 0.5f; // Delay in seconds before shooting after teleporting
    private int hitsToDefeat = 8; // Number of hits required to defeat the boss
    private int currentHits = 0; // Current number of hits taken by the boss
    private Vector2 screenBounds;
    private float lastTeleportTime = 0;

    void Start()
    {
        // Get the screen bounds for teleportation
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Teleport();
    }

    void Update()
    {
        // Check if it's time to teleport again
        if (Time.time - lastTeleportTime > teleportTime)
        {
            Teleport();
            lastTeleportTime = Time.time;
        }
    }

    void Teleport()
    {
        // Randomly choose a new position within the screen bounds
        Vector2 newPosition = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), Random.Range(-screenBounds.y, screenBounds.y));
        transform.position = newPosition;

        // Start coroutine to shoot bullets after a short delay
        StartCoroutine(ShootBulletsInAllDirections());
    }

    IEnumerator ShootBulletsInAllDirections()
    {
        // Wait for the specified delay before shooting
        yield return new WaitForSeconds(shootDelay);

        // Shoot bullets in 8 directions
        for (int i = 0; i < 8; i++)
        {
            float angle = i * 45; // There are 8 directions, each 45 degrees apart
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Instantiate(bulletPrefab, transform.position, rotation);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the boss collides with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Reduce the player's health by 50
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth)
            {
                playerHealth.DamagePlayer(50);
            }
        }
    }

    public void HitByPlayer()
    {
        currentHits++;
        if (currentHits >= hitsToDefeat)
        {
            // The boss is defeated
            Destroy(gameObject);
        }
    }
}

