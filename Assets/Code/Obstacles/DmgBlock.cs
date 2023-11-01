using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DmgBlock : MonoBehaviour
{
    public float pushBackForce = 10f;
    private SubController player;
    void OnCollisionEnter2D(Collision2D collision)
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
            playerHealth?.DamagePlayer(20);
        }

        Rigidbody2D playerRigidbody = playerObj.GetComponent<Rigidbody2D>();
        if (playerRigidbody != null)
        {
            Vector2 pushDirection = -playerRigidbody.velocity.normalized;
            playerRigidbody.AddForce(pushDirection * pushBackForce, ForceMode2D.Impulse);
        }

       
    }
}
