using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHealth : MonoBehaviour
{
    public int health = 15;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Power-Cell";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Retrieve the Health component of the submarine and increase its health
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.GetHealth(health);
            }
            Destroy(gameObject);
        }
    }
}
