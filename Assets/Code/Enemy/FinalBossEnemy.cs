using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class FinalBossEnemy : MonoBehaviour
{
    public FinalBossEnemy instance;

    public GameObject bulletPrefab; // The prefab for the bullets
    public float teleportTime = 5.0f; // Time interval for teleportation
    public float shootDelay = 0.5f; // Delay before shooting bullets
    public float bulletSpeed = 10f; // Speed of the bullets
    public int bossHealth = 100; // Health of the boss
    public int maxBossHealth = 100;
    private Vector2 screenBounds;
    private float lastTeleportTime = 0;
    public GameObject smallEnemyPrefab; // The prefab for the small enemies
    public float enemySpawnInterval = 3.0f; // Interval for spawning small enemies
    private float lastEnemySpawnTime = 0;
    private bool isSpawningEnemies = false;

    public GameObject exit;
    public Transform exitSpawn;

    //Outlets
    public Transform[] ammoSpawns;
    public Transform shieldSpawn;
    public Transform powerSpawn;


    public GameObject ammo;
    public GameObject shield;
    public GameObject power;

    public int healthThreshold;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        bossHealth = maxBossHealth;
        // Get the screen bounds for random teleportation
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
        // Check if boss health is half or below and start spawning enemies
        if (bossHealth <= maxBossHealth / 2 && !isSpawningEnemies)
        {
            isSpawningEnemies = true;
            StartCoroutine(SpawnSmallEnemies());
        }
    }

    IEnumerator SpawnSmallEnemies()
    {
        while (isSpawningEnemies)
        {
            // Wait for the specified interval
            yield return new WaitForSeconds(enemySpawnInterval);

            // Spawn a small enemy at a random position
            Vector2 spawnPosition = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), Random.Range(-screenBounds.y, screenBounds.y));
            Instantiate(smallEnemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    void Teleport()
    {
        // Choose a new position randomly within the screen bounds
        Vector2 newPosition = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), Random.Range(-screenBounds.y, screenBounds.y));
        transform.position = newPosition;

        // Start shooting bullets after a short delay
        StartCoroutine(ShootBulletsInAllDirections());
    }

    IEnumerator ShootBulletsInAllDirections()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(shootDelay);

        // Shoot bullets in 8 directions
        for (int i = 0; i < 8; i++)
        {
            float angle = i * 45; // There are 8 directions, each 45 degrees apart
            Vector2 spawnOffset = Quaternion.Euler(0, 0, angle) * Vector2.up * 0.5f; // Adjust the offset as needed
            Vector2 spawnPosition = (Vector2)transform.position + spawnOffset;
            GameObject bulletInstance = Instantiate(bulletPrefab, spawnPosition, Quaternion.Euler(0, 0, angle));

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
        // If the boss collides with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Reduce the player's health
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth)
            {
                playerHealth.DamagePlayer(50);
            }
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(30);
        }
        
    }

    public void TakeDamage(int damage)
    {
        bossHealth -= damage;
        
        if (bossHealth <= 0)
        {
            // Stop spawning enemies if the boss is defeated
            isSpawningEnemies = false;
            // Boss is defeated
            Destroy(gameObject);
            Instantiate(exit, exitSpawn.position, Quaternion.identity);
        }
        else if (bossHealth <= healthThreshold)
        {
            spawnResources();
            healthThreshold = 0;
        }
    }
    void spawnResources()
    {

        foreach (Transform t in ammoSpawns)
        {
            Instantiate(ammo, t.position, Quaternion.identity);
        }

        Instantiate(shield, shieldSpawn.position, Quaternion.identity);
        Instantiate(power, powerSpawn.position, Quaternion.identity);

    }
}
