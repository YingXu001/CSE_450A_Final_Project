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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.GetHealth(health);
            }
            Destroy(gameObject);
        }
    }
}
