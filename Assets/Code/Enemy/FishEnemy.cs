using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishEnemy : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        // Reload scene only when colliding with player
        if (other.gameObject.GetComponent<SubController>())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

