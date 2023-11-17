using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultimate : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shootDuration = 5f;
    public float timeBetweenBullets = 0.5f;
    private SubController player; // get the submarine object
    private bool isShooting = false;
    public AudioClip laser_shot_sound;

    void Start()
    {
        player = FindObjectOfType<SubController>();
    }

    void Update()
    {
        if(player.mechaMode == true)
        {
            if (Input.GetKeyDown(KeyCode.R) && !isShooting)
            {
                isShooting = true;
                StartCoroutine(ShootBullets());
                player.DecreaseEnergy(50);
            }
        }
        
    }

    IEnumerator ShootBullets()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shootDuration)
        {
            ShootMultipleBullets();
            yield return new WaitForSeconds(0.5f); // Adjust this delay as needed

            elapsedTime += 0.5f; // Increment elapsed time
        }

        isShooting = false;
    }

    void ShootMultipleBullets()
    {
        for (int i = 0; i < 30; i++)
        {
            AudioSource.PlayClipAtPoint(laser_shot_sound, transform.position);
            float angle = i * 12f;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 bulletDirection = Quaternion.Euler(0f, 0f, angle) * transform.right;

            rb.velocity = bulletDirection * 10f; // Adjust speed as needed

            // Set the rotation of the bullet to face its direction
            float bulletAngle = Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, bulletAngle);

            Destroy(bullet, 3f); // Destroy the bullet after 3 seconds (adjust as need
        }
    }
}
