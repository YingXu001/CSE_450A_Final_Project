using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemy : MonoBehaviour
{
    public float pushBackForce = 10f;
    public int damage = 20;
    private SubController player;
    private Rigidbody2D playerRigidbody;

    private void Start()
    {
        player = FindObjectOfType<SubController>();
        if (player != null)
        {
            // Set the enemy's position next to the player
            transform.position = player.transform.position + Vector3.right; // Adjust as necessary
            // Set layer above the wall
            gameObject.layer = LayerMask.NameToLayer("AboveWall"); // Replace "AboveWall" with your layer name
        }
    }

    private void Update()
    {
        // Optionally, you can add code here to make the enemy follow the player or perform other actions.
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            HandleCollisionWithPlayer();
        }
    }

    private void HandleCollisionWithPlayer()
    {
        if (player.HasSheild())
        {
            player.DeactivateShield();
        }
        else
        {
            player.playerHealth.DamagePlayer(damage);
        }

        playerRigidbody = player.GetComponent<Rigidbody2D>();
        if (playerRigidbody != null)
        {
            Vector2 pushDirection = (player.transform.position - transform.position).normalized;
            playerRigidbody.AddForce(pushDirection * pushBackForce, ForceMode2D.Impulse);
        }
    }
}