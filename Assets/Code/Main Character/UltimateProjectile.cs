using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateProjectile : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject); // Destroy the bullet on collision
    }
}
