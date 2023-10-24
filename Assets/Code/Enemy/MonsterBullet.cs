using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
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
                    playerHealth.DamagePlayer(30);
                }
            }
        }

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Boundary") || collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
