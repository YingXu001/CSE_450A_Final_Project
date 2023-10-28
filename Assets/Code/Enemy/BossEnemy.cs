using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public GameObject bulletPrefab; // Bullet prefab reference
    public float spawnAreaSize = 5f; // Size of the area where the Boss can randomly spawn
    public float shootInterval = 2f; // Interval time between bullet shoots

    // Start is called before the first frame update
    void Start()
    {
        // Position the Boss randomly within the specified range
        Vector2 randomPosition = new Vector2(
            Random.Range(-spawnAreaSize / 2, spawnAreaSize / 2),
            Random.Range(-spawnAreaSize / 2, spawnAreaSize / 2)
        );
        transform.position = randomPosition;

        // Start the bullet shooting coroutine
        StartCoroutine(ShootBullets());
    }

    // This coroutine shoots bullets at regular intervals
    IEnumerator ShootBullets()
    {
        while (true)
        {
            for (int i = 0; i < 8; i++) // There are 8 directions, so loop 8 times
            {
                float angle = i * 45f; // 360 / 8 = 45, increment by 45 degrees each time
                FireBullet(angle);
            }
            yield return new WaitForSeconds(shootInterval); // Wait for the specified time before shooting the next round of bullets
        }
    }

    // Shoot a bullet in the given angle direction
    void FireBullet(float angle)
    {
        Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up; // Get the direction by rotating
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, angle));
        bullet.GetComponent<Rigidbody2D>().velocity = direction * 5f; // Here, 5f is the bullet speed. Adjust as needed.
    }
}
