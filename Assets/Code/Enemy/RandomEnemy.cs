using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomEnemy : MonoBehaviour
{
    public float pushBackForce = 10f;
    public int damage = 20;
    private SubController player;
    private Rigidbody2D playerRigidbody;

    private void Start()
    {
        player = FindObjectOfType<SubController>();
        gameObject.layer = LayerMask.NameToLayer("RandomEnemy");

        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingLayerName = "Above"; // Use the correct Sorting Layer name
            spriteRenderer.sortingOrder = 1; // Use an appropriate sorting order value to ensure it renders above the wall
        }

        Debug.Log("RandomEnemy started at position: " + transform.position);
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