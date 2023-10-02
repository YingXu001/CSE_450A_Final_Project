using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAmmo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Ammo";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SubController player = collision.gameObject.GetComponent<SubController>();
            player.numAmmo++;
            Destroy(gameObject);
        }
    }
}
