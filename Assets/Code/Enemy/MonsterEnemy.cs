using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEnemy : MonoBehaviour
{
    public float pushBackForce = .2f;  // Force with which the submarine is pushed back, adjust as needed

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object is the submarine (tagged as Player)
        if (collision.gameObject.CompareTag("Player"))
        {
            // Retrieve the Health component of the submarine and decrease its health
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.DamagePlayer(10);
            }

            // Push the submarine back in the opposite direction of its movement
            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                Vector2 pushDirection = -playerRigidbody.velocity.normalized;
                playerRigidbody.AddForce(pushDirection * pushBackForce, ForceMode2D.Impulse);
            }
        }
    }
}
