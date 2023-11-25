using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomEnemy : MonoBehaviour
{
    public float pushBackForce = 10f;
    public int damage = 20;
    private SubController player;
    private Rigidbody2D playerRigidbody;
    public bool canMove = true; // Assuming you want to control movement

    private void Start()
    {
        player = FindObjectOfType<SubController>();
        if (player == null)
        {
            Debug.LogError("Player not found in the scene.");
            return;
        }
        
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
        if (collision.gameObject.CompareTag("Player"))
        {
            HandleCollisionWithPlayer(collision.gameObject); // Pass collision.gameObject as the argument
        }
        else if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Freeze-Bullet"))
        {
            if (collision.gameObject.CompareTag("Freeze-Bullet"))
            {
                canMove = false; // Disable movement if hit by a freeze bullet
                print("freeze!");
            }
            // Since this enemy will be destroyed, no need to check for player null here
            player.IncreaseEnergy(30);
            Destroy(gameObject);
        }
    }


    private void HandleCollisionWithPlayer(GameObject playerObj)
    {
        SubController playerController = playerObj.GetComponent<SubController>();

        if (playerController != null)
        {
            if (playerController.HasSheild()) // Correct the method name to match SubController
            {
                playerController.DeactivateShield();
            }
            else
            {
                playerController.playerHealth.DamagePlayer(damage);
            }

            Rigidbody2D playerRigidbody = playerObj.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                Vector2 pushDirection = (playerObj.transform.position - transform.position).normalized;
                playerRigidbody.AddForce(pushDirection * pushBackForce, ForceMode2D.Impulse);
            }
        }
        else
        {
            Debug.LogError("SubController component not found on playerObj.");
        }
    }
}